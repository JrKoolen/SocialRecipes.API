import { formFields, testData } from '../support/testData';

describe('Create Account Page', () => {
  const baseUrl = 'http://localhost:3000/register';

  function fillForm({ username, email, password }) {
    if (username) cy.get(formFields.username).type(username);
    if (email) cy.get(formFields.email).type(email);
    if (password) cy.get(formFields.password).type(password);
  }

  it('should load the registration form', () => {
    cy.visit(baseUrl);
    cy.get('form#createAccountForm').should('be.visible');
    cy.get(formFields.username).should('be.visible');
    cy.get(formFields.email).should('be.visible');
    cy.get(formFields.password).should('be.visible');
    cy.get(formFields.submitButton).should('be.visible');
  });

  Cypress._.each(testData, (test) => {
    it(test.description, () => {
      cy.visit(baseUrl);
      fillForm(test.data);
      cy.get(formFields.submitButton).click();

      if (test.expectValid) {
        cy.url().should('include', '/login');
        cy.contains('Login').should('be.visible');
      } else {
        if (!test.data.username) {
          cy.get(formFields.username).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
          });
        }
        if (!test.data.email || test.data.email.includes('invalid')) {
          cy.get(formFields.email).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
          });
        }
        if (!test.data.password) {
          cy.get(formFields.password).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
          });
        }
      }
    });
  });
});
