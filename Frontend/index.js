console.log('Current directory:', __dirname);
const express = require('express');
const session = require('express-session');
const multer = require('multer');
const constants = require('./config/constants');
const setLocals = require('./middleware/local');
const upload = multer({ dest: 'uploads/' });
const crypto = require('crypto');
const marked = require('marked');
const https = require('https');
const axios = require('axios');
const path = require('path');
const fs = require('fs');
const app = express();


app.use(express.json());
app.use(express.urlencoded({ extended: true })); 

app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

app.use(express.static(path.join(__dirname, 'public')));
app.use('/assets', express.static('assets'));

app.use(session({
  secret: crypto.randomBytes(64).toString('hex'), 
  resave: false,
  saveUninitialized: true,
  cookie: {
    secure: process.env.NODE_ENV === 'production', 
    httpOnly: true, 
    maxAge: 1000 * 60 * 60 * 24 
  }
}));

app.use(setLocals);

function ensureAuthenticated(req, res, next) {
  if (req.session.user && req.session.user.isLoggedIn) {
    return next();
  }
  res.redirect('/login');
}

const httpsAgent = new https.Agent({
  rejectUnauthorized: false, 
});

app.get('/', async (req, res) => {
  try {
    const response = await axios.get(constants.RECIPE.GET_ALL_RECIPES, { httpsAgent });
    const recipes = response.data.recipes || [];

    if (Array.isArray(recipes)) {
      res.render('index', { recipes });
    } else {
      console.error('Error: Expected an array for recipes, received:', recipes);
      res.render('index', { recipes: [] });
    }
  } catch (error) {
    console.error('Error fetching recipes:', error);
    res.render('index', { recipes: [] });
  }
});


app.get('/login', (req, res) => {
  res.render('login', { activePage: 'login', errorMessage: null, isLoggedIn: req.session.user && req.session.user.isLoggedIn  });
});

app.post('/login', async (req, res) => {
  const { username, password } = req.body;

  try {
    const response = await axios.post(
      constants.AUTH.LOGIN,
      { username, password },
      { httpsAgent: new https.Agent({ rejectUnauthorized: false }) }
    );

    const { token } = response.data;

    if (token) {
      req.session.user = {
        username,
        token,
        id : response.data.id,
        isLoggedIn: true 
      };
      console.log(`User ${username} logged in with token.`);
      return res.redirect('/user-page');
    } else {
      res.render('login', { errorMessage: 'Invalid username or password' });
    }
  } catch (error) {
    console.error('Error logging in:', error.message);
    res.render('login', { errorMessage: 'Login failed. Please try again.' });
  }
});

app.get('/logout', (req, res) => {
  req.session.destroy((err) => {
    if (err) {
      return res.redirect('/user-page'); 
    }
    res.clearCookie('connect.sid');
    res.redirect('/login');
  });
});


app.get('/register', (req, res) => {
  res.render('register', { errorMessage: null });
});

app.post('/register', async (req, res) => {
  const { username, email, password } = req.body;

  const payload = {
    name: username,
    email: email,
    password: password
  };

  console.log('Data sent to the API:', payload); 

  try {
    const response = await axios.post(
      constants.AUTH.REGISTER,
      payload,
      { httpsAgent }
    );

    if (response.status === 200) {
      return res.redirect('/login'); 
    } else {
      res.render('register', {
        errorMessage: 'Registration failed. Please try again.'
      });
    }
  } catch (error) {
    console.error('Error registering user:', error.response ? error.response.data : error.message);
    res.render('register', {
      errorMessage: 'An error occurred during registration. Please try again.'
    });
  }
});

app.get('/user-page', async (req, res) => {
  if (!req.session.user || !req.session.user.token) {
    return res.redirect('/login');
  }

  try {
    const response = await axios.get(
      constants.RECIPE.GET_ALL_RECIPES_FROM_USER(req.session.user.id), 
      {
        headers: {
          Authorization: `Bearer ${req.session.user.token}`
        },
        httpsAgent: new https.Agent({ rejectUnauthorized: false })
      }
    );
    
    const recipes = Array.isArray(response.data.recipes) ? response.data.recipes : [];
    console.log('Recipes:', recipes);
    res.render('user-page', { recipes });
  } catch (error) {
    console.error('Error fetching recipes:', error.message);
    res.render('user-page', { errorMessage: 'Failed to load recipes. Please try again.', recipes: [] });
  }
});


app.get('/recipes', async (req, res) => {
  try {
    const response = await axios.get(constants.RECIPE.GET_ALL_RECIPES, { httpsAgent });
    const recipes = Array.isArray(response.data.recipes) ? response.data.recipes : [];
    
    res.render('recipes', { recipes });
  } catch (error) {
    console.error('Error fetching recipes:', error);
    res.render('recipes', { recipes: [] });
  }
});


app.get('/create-recipe', ensureAuthenticated, (req, res) => {
  res.render('create-recipe');
});

app.post('/create-recipe', upload.single('image'), async (req, res) => {
  const { title, body, description } = req.body;
  const userId = req.session.user ? req.session.user.id : null;

  const payload = {
    recipeDto: {
      title,
      body,
      description,
      userId,
      status: "private"
    }
  };

  if (req.file) {
    const fs = require('fs');
    try {
      let imageBase64 = fs.readFileSync(req.file.path, { encoding: 'base64' });
      if (imageBase64.startsWith("data:image")) {
        imageBase64 = imageBase64.split(",")[1];
      }
      payload.recipeDto.image = imageBase64; 
    } catch (error) {
      console.error("Error reading image file:", error.message);
      return res.render('create-recipe', { errorMessage: 'Error processing image. Please try again.' });
    }
  }

  console.log('Data sent to the API:', JSON.stringify(payload, null, 2)); 

  try {
    const agent = new https.Agent({ rejectUnauthorized: false });

    const response = await axios.post(
      constants.RECIPE.CREATE_RECIPE,
      payload,
      {
        headers: {
          Authorization: `Bearer ${req.session.user.token}`,
          'Content-Type': 'application/json'
        },
        httpsAgent: agent 
      }
    );

    if (response.status === 200) {
      return res.redirect('/recipes');
    } else {
      res.render('create-recipe', {
        errorMessage: 'Failed to create recipe. Please try again.'
      });
    }
  } catch (error) {
    console.error('Error creating recipe:', error.response ? error.response.data : error.message);
    res.render('create-recipe', {
      errorMessage: 'An error occurred during recipe creation. Please try again.'
    });
  } finally {
    if (req.file) {
      fs.unlink(req.file.path, (err) => {
        if (err) console.error("Error deleting temp image file:", err.message);
      });
    }
  }
});

app.get('/confirm-account', (req, res) => {
  res.render('confirm-account');
});

app.get('/recipe/:id', async (req, res) => {
  const recipeId = req.params.id;
  try {
    const response = await axios.get(constants.RECIPE.GET_ALL_RECIPES_FROM_USER(req.session.id), { httpsAgent });
    const recipe = response.data.recipe;

    if (recipe) {
      if (recipe.body) {
        recipe.body = marked.parse(recipe.body);
      }
      res.render('recipe', { recipe, activePage: 'recipe', isLoggedIn: req.session.user && req.session.user.isLoggedIn  });
    } else {
      res.status(404).send('Recipe not found');
    }
  } catch (error) {
    console.error('Error fetching recipe:', error);
    res.status(500).send('Failed to fetch recipe.');
  }
});

app.listen(3000, () => {
  console.log('Server running on http://localhost:3000');
});