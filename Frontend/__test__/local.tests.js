const setLocals = require('../middleware/setLocals'); // Adjust the import path based on your project structure

describe('setLocals middleware', () => {
  let req;
  let res;
  let next;

  beforeEach(() => {
    // Mock the req, res, and next objects
    req = { session: {}, path: '/' }; // Default values
    res = { locals: {} };
    next = jest.fn();
  });

  it('should set res.locals.isLoggedIn to true if user is logged in', () => {
    // Simulate logged-in user
    req.session.user = { isLoggedIn: true, username: 'testUser' };

    setLocals(req, res, next);

    expect(res.locals.isLoggedIn).toBe(true);
    expect(res.locals.username).toBe('testUser');
    expect(next).toHaveBeenCalled(); // Ensure next() was called
  });

  it('should set res.locals.isLoggedIn to false if user is not logged in', () => {
    // Simulate logged-out user
    req.session.user = { isLoggedIn: false };

    setLocals(req, res, next);

    expect(res.locals.isLoggedIn).toBe(false);
    expect(res.locals.username).toBe(null); // Should be null if user is not logged in
    expect(next).toHaveBeenCalled();
  });

  it('should set res.locals.activePage to "home" if path is /', () => {
    req.path = '/';

    setLocals(req, res, next);

    expect(res.locals.activePage).toBe('home');
    expect(next).toHaveBeenCalled();
  });

  it('should set res.locals.activePage to the path name if path is not /', () => {
    req.path = '/about';

    setLocals(req, res, next);

    expect(res.locals.activePage).toBe('about');
    expect(next).toHaveBeenCalled();
  });

  it('should set res.locals.username to null if no user is in session', () => {
    // Simulate no user in session
    req.session.user = null;

    setLocals(req, res, next);

    expect(res.locals.username).toBe(null); // No username if user is not in session
    expect(next).toHaveBeenCalled();
  });
});
