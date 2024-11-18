import { loginFields, testData } from '../support/testData';

describe('Login Page Tests', () => {
  const baseUrl = 'http://localhost:3000/login';

  function fillLoginForm({ username, password }) {
    if (username) cy.get(loginFields.username).type(username);
    if (password) cy.get(loginFields.password).type(password);
  }

  it('should load the login page successfully', () => {
    cy.visit(baseUrl);
    cy.contains('Login').should('be.visible');
  });

  it('should show an error on submitting empty login form', () => {
    cy.visit(baseUrl);
    cy.get(loginFields.submitButton).click();

    cy.get(loginFields.username).should('have.attr', 'required');
    cy.get(loginFields.username).then(($input) => {
      expect($input[0].checkValidity()).to.be.false;
    });

    cy.get(loginFields.password).should('have.attr', 'required');
    cy.get(loginFields.password).then(($input) => {
      expect($input[0].checkValidity()).to.be.false;
    });
  });

  it('should login successfully with valid credentials', () => {
    cy.visit(baseUrl);

    const validData = testData.validSubmission.data; 
    fillLoginForm({ username: validData.username, password: validData.password });

    cy.get(loginFields.submitButton).click();

    cy.url().should('include', '/user-page');
    cy.contains('Welcome').should('be.visible');
  });

  it('should show an error with invalid credentials', () => {
    cy.visit(baseUrl);
    fillLoginForm({ username: 'invalidUsername', password: 'wrongpassword' });
    cy.get(loginFields.submitButton).click();

    cy.contains('Login failed. Please try again.').should('be.visible');
  });
});
