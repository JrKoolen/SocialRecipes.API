module.exports = (req, res, next) => {
    res.locals.isLoggedIn = req.session && req.session.user && req.session.user.isLoggedIn;
    res.locals.activePage = req.path === '/' ? 'home' : req.path.slice(1);
    res.locals.username = req.session.user ? req.session.user.username : null;
    next();
  };