// File: cypress/integration/login.spec.js

describe('Login Page Tests', () => {
    beforeEach(() => {
      cy.visit('/login');
    });
  
    it('should display the login form', () => {
      cy.get('form[action="/login"]').should('be.visible');
      cy.get('input#username').should('exist');
      cy.get('input#password').should('exist');
      cy.get('button[type="submit"]').should('exist');
    });
  
    it('should not submit the form with empty fields', () => {
      cy.get('button[type="submit"]').click();
  
      cy.get('input#username:invalid').should('exist');
      cy.get('input#password:invalid').should('exist');
    });
  
    it('should allow entering username and password', () => {
      cy.get('input#username').type('test');
      cy.get('input#password').type('test');
  
      cy.get('input#username').should('have.value', 'test');
      cy.get('input#password').should('have.value', 'test');
    });
});
  