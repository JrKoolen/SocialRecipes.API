const dotenv = require('dotenv');
const envFile = process.env.NODE_ENV === 'production' ? '.env.production' : '.env.development';

dotenv.config({ path: envFile });

module.exports = (req, res, next) => {
  if (process.env.NODE_ENV === 'test' || process.env.NODE_ENV === 'development') {
      req.session = req.session || {};
      req.session.id = '1';
      req.session.user = {
          username: 'testuser',
          isLoggedIn: true,
      };
      console.log(`Mock session set for ${process.env.NODE_ENV} environment`);
  }

  res.locals.isLoggedIn = req.session && req.session.user && req.session.user.isLoggedIn;
  res.locals.activePage = req.path === '/' ? 'home' : req.path.slice(1);
  res.locals.username = req.session.user ? req.session.user.username : null;
  next();
};
