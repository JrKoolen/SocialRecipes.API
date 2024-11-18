// testData.js
export const formFields = {
    username: 'input#username',
    email: 'input#email',
    password: 'input#password',
    submitButton: 'button[type="submit"]',
  };
  
  export const loginFields = {
    username: 'input[name="username"]',
    password: 'input[name="password"]',
    submitButton: 'button[type="submit"]',
  };
  
  export const testData = {
    emptyFields: {
      description: 'should show required field errors on empty form submission',
      data: { username: '', email: '', password: '' },
      expectValid: false,
    },
    invalidEmail: {
      description: 'should show an error for invalid email format',
      data: { username: 'testuser', email: 'invalid-email', password: 'Password123' },
      expectValid: false,
    },
    validSubmission: {
      description: 'should successfully submit the form with valid inputs',
      data: {
        username: `testuser_${Date.now()}`,
        email: `testuser_${Date.now()}@example.com`,
        password: 'Password123',
      },
      expectValid: true,
    },
  };
  