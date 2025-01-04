// File: cypress/integration/register.spec.js

describe('Register Page Tests', () => {
    beforeEach(() => {
      cy.visit('/register');
    });
  
    it('should display the registration form', () => {
      cy.get('form#createAccountForm').should('be.visible');
      cy.get('input#username').should('exist');
      cy.get('input#email').should('exist');
      cy.get('input#password').should('exist');
      cy.get('button[type="submit"]').should('exist');
    });
  
    it('should not submit the form with empty fields', () => {
      cy.get('button[type="submit"]').click();
      cy.get('input#username:invalid').should('exist');
      cy.get('input#email:invalid').should('exist');
      cy.get('input#password:invalid').should('exist');
    });
  
    it('should allow entering username, email, and password', () => {
      cy.get('input#username').type('test');
      cy.get('input#email').type('test@test.com');
      cy.get('input#password').type('test');
      cy.get('input#username').should('have.value', 'test');
      cy.get('input#email').should('have.value', 'test@test.com');
      cy.get('input#password').should('have.value', 'test');
    });
  
    it('should validate email format', () => {
      cy.get('input#username').type('test');
      cy.get('input#email').type('invalidemail');
      cy.get('input#password').type('test');
      cy.get('button[type="submit"]').click();
  
      cy.get('input#email:invalid').should('exist');
    });
});
  