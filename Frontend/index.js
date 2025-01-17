const express = require('express');
const path = require('path');
const fs = require('fs');
const querystring = require('querystring');
const https = require('https');

const session = require('express-session');
const FileStore = require('session-file-store')(session);
const multer = require('multer');
const marked = require('marked');
const axios = require('axios');
const cors = require('cors');
const dotenv = require('dotenv');

const constants = require('./config/constants');
const setLocals = require('./middleware/local');
const { waitForSession } = require('./src/utils/utils'); 
const envFile = process.env.NODE_ENV === 'production' ? '.env.production' : '.env.development';

dotenv.config({ path: envFile });
const PORT = process.env.PORT || 3001; 

// const corsOptions = {
// origin: [process.env.CORS_ORIGIN || 'http://localhost:3000', 'http://localhost:3001', 'http://localhost:8081'],
// methods: ['GET', 'POST', 'PUT', 'DELETE'],
// credentials: true,
// };



module.exports = {
  waitForSession,
};

const app = express();

// app.use(cors(corsOptions));
app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));
app.use(express.static(path.join(__dirname, 'public')));
app.use('/assets', express.static('assets'));

app.use(
  session({
    store: new FileStore({
      path: './sessions',
      retries: 5, 
      retryDelay: 5,
      reapInterval: 3600,
      logFn: console.log, }),
    secret: 'T8x!g5#Lk92z@Q7P$G1%XcMZ5L!7DfNlR',
    resave: false,
    saveUninitialized: false,
    cookie: {
      secure: false,
      maxAge: 1000 * 60 * 60 * 24, 
    },
  })
);

const upload = multer({ dest: 'uploads/' });

app.use(setLocals);

const httpsAgent = new https.Agent({
  rejectUnauthorized: false, 
});

function ensureAuthenticated(req, res, next) {
  if (req.session.user && req.session.user.isLoggedIn) {
    return next();
  }
  res.redirect('/login');
}


app.get('/', (req, res) => {
  res.redirect('/recipes');
});

app.get('/login', (req, res) => {
  res.render('login', { activePage: 'login', errorMessage: null, isLoggedIn: req.session.user && req.session.user.isLoggedIn  });
});

app.post('/login', async (req, res) => {
  const { username, password } = req.body;

  const payload = {
    Name: username,
    Password: password,
  };

  console.log('Data sent to the API:', payload);

  try {
    const response = await axios.post(
      constants.AUTH.LOGIN,
      payload,
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
        },
        httpsAgent: new https.Agent({ rejectUnauthorized: false }),
      }
    );

    if (response.status === 200 && response.data.token) {
      req.session.user = {
        username,
        token: response.data.token,
        id: response.data.userId,
        isLoggedIn: true,
      };
      console.log('Session:', req.session);
      console.log(response.data);
      console.log(`User ${username} logged in successfully. user id: ${req.session.user.id}`);
      req.session.save(async (err) => {
        if (err) {
            console.error('Error saving session:', err);
            return res.status(500).render('login', {
                errorMessage: 'An error occurred while saving your session. Please try again.',
            });
        }
        try {
            await waitForSession(req.sessionID);
            console.log(`Session file is available: ${req.sessionID}`);
            
            return res.redirect('/user-page');
        } catch (sessionError) {
            console.error(`Error waiting for session file: ${sessionError.message}`);
            return res.status(500).render('login', {
                errorMessage: 'Session file creation timed out. Please try again.',
            });
        }
    });
  } else {
    console.log('Invalid username or password');
    return res.status(401).render('login', {
        errorMessage: 'Invalid username or password.',
    });
    }} catch (error) {
    console.error('Error logging in:', error.response ? error.response.data : error.message);
    return res.status(500).render('login', {
    errorMessage: 'An error occurred during login. Please try again.',
    });
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

app.get('/mockAuth', (req, res) => {
  req.session.user = {
    username: 'testUser',
    token: 'mockToken',
    id: 1,
    isLoggedIn: true,
  };
  res.redirect('/user-page');
});

app.get('/register', (req, res) => {
  res.render('register', { errorMessage: null });
});

app.post('/register', async (req, res) => {
  const { username, email, password } = req.body;

  const payload = {
    Name: username,
    Email: email,
    Password: password
  };

  console.log('Data sent to the API:', payload); 

  try {
    const response = await axios.post(
      constants.AUTH.REGISTER,
      payload,
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        },
        httpsAgent
      }
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
    res.render('user-page', { recipes });
  } catch (error) {
    console.error('Error fetching recipes:', error.message);
    res.render('user-page', { errorMessage: 'Failed to load recipes. Please try again.', recipes: [] });
  }
});

app.get('/recipes', async (req, res) => {
  try {
    const response = await axios.get(`${constants.RECIPE.GET_FEATURED_RECIPES}?amount=20`, { httpsAgent });
    const recipes = Array.isArray(response.data) ? response.data : [];
    
    
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

  try {
    let base64Image = null;
    if (req.file) {
      const filePath = path.resolve(req.file.path);
      const fileData = fs.readFileSync(filePath);
      base64Image = fileData.toString('base64');
    }

    const payload = {
      Title: title,
      Body: body,
      Description: description,
      UserId: req.session.user ? req.session.user.id : null,
      Status: 'private',
      Image: base64Image 
    };


    const response = await axios.post(
      constants.RECIPE.CREATE_RECIPE,
      payload,
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
          Authorization: `Bearer ${req.session.user.token}`
      },
        httpsAgent
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
    console.error('Error creating recipe:', error.message);
    res.render('create-recipe', {
      errorMessage: 'An error occurred while creating the recipe. Please try again.'
    });
  } finally {
    if (req.file) {
      fs.unlink(req.file.path, (err) => {
        if (err) console.error('Error deleting temp file:', err.message);
      });
    }
  }
});

app.get('/confirm-account', (req, res) => {
  res.render('confirm-account');
});

app.get('/recipe/:id', async (req, res) => {
  try {

    const response = await axios.get(
      constants.RECIPE.GET_RECIPE_BY_ID(req.params.id),
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded, application/json',
        },
        httpsAgent
      }
    );

    const recipe = response.data.recipe;

    if (recipe && recipe.id === parseInt(req.params.id, 10)) {
      const recipeData = {
        id: recipe.id,
        title: recipe.title,
        description: recipe.description || 'No description provided.',
        body: recipe.body ? marked.parse(recipe.body) : null, 
        imageBase64: recipe.imageBase64 || null, 
      };
      res.render('recipe', {
        recipe: recipeData, 
        activePage: 'recipe',
      });
    } else {
      res.status(404).send('Recipe not found');
    }
  } catch (error) {
    console.error('Error fetching recipe:', error.response?.data || error.message);
    res.status(500).send('Failed to fetch recipe.');
  }
});



app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`);
});

