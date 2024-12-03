const express = require('express');
const session = require('express-session');
const FileStore = require('session-file-store')(session);
const multer = require('multer');
const constants = require('./config/constants');
const setLocals = require('./middleware/local');
const upload = multer({ dest: 'uploads/' });
const crypto = require('crypto');
const marked = require('marked');
const https = require('https');
const axios = require('axios');
const path = require('path');
const dotenv = require('dotenv');
const fs = require('fs');
const app = express();
const querystring = require('querystring');
const cors = require('cors');

const envFile = process.env.NODE_ENV === 'production' ? '.env.production' : '.env.development';
dotenv.config({ path: envFile });

console.log(`Environment: ${process.env.NODE_ENV}`);

const corsOptions = {
  origin: process.env.CORS_ORIGIN || 'http://localhost:3000',
  methods: ['GET', 'POST', 'PUT', 'DELETE'],
  credentials: true,
};
app.use(cors(corsOptions));

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
    secret: 'your-secret-key',
    resave: false,
    saveUninitialized: false,
    cookie: {
      secure: process.env.NODE_ENV === 'production',
      httpOnly: true,
      maxAge: 1000 * 60 * 60 * 24, 
    },
  })
);

console.log('Current directory:', __dirname);


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


function waitForSession(fileName, timeout = 10000, interval = 100) {
  return new Promise((resolve, reject) => {
      const startTime = Date.now();
      const filePath = path.join(process.cwd(), 'sessions', fileName); 

      const checkFile = () => {
          if (fs.existsSync(`${filePath}.json`)) {
              resolve(true); 
          } else if (Date.now() - startTime > timeout) {
              reject(new Error(`Timeout: File ${filePath} was not found within ${timeout}ms`));
          } else {
              setTimeout(checkFile, interval); 
          }
      };

      checkFile();
  });
}

app.post('/login', async (req, res) => {
  const { username, password } = req.body;

  const payload = {
    Username: username,
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
}
} catch (error) {
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
    //console.log('Recipes:', recipes);
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

  try {
    // Read the image file and convert it to Base64
    let base64Image = null;
    if (req.file) {
      const filePath = path.resolve(req.file.path);
      const fileData = fs.readFileSync(filePath);
      base64Image = fileData.toString('base64');
    }

    // Build the payload
    const payload = {
      Title: title,
      Body: body,
      Description: description,
      UserId: req.session.user ? req.session.user.id : null,
      Status: 'private',
      Image: base64Image // Add Base64 image string to the payload
    };

    //console.log('Data sent to the API:', payload);

    // Send the payload to the API
    const response = await axios.post(
      constants.RECIPE.CREATE_RECIPE,
      payload,
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
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
    console.log(`${constants.RECIPE.GET_ALL_RECIPES_FROM_USER(req.session.user.id)}`);

    const response = await axios.get(
      constants.RECIPE.GET_ALL_RECIPES_FROM_USER(req.session.user.id),
      {
        headers: {
          Authorization: `Bearer ${req.session.user.token}`
        },
        httpsAgent: new https.Agent({ rejectUnauthorized: false }) 
      }
    );

    const recipes = response.data.recipes || [];

    const recipe = recipes.find((r) => r.id === parseInt(req.params.id, 10));

    if (recipe) {
      recipe.imageBase64 = recipe.image;

      recipe.description = recipe.description || 'No description provided.';

      if (recipe.body) {
        recipe.body = marked.parse(recipe.body);
      }
      res.render('recipe', { 
        recipe, 
        activePage: 'recipe', 
        isLoggedIn: req.session.user && req.session.user.isLoggedIn 
      });
    } else {
      res.status(404).send('Recipe not found');
    }
  } catch (error) {
    console.error('Error fetching recipe:', error.response?.data || error.message);
    res.status(500).send('Failed to fetch recipe.');
  }
});


const PORT = process.env.PORT || 3001; 

app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`);
});

