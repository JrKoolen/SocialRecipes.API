describe('Create Account Page', () => {

    it('should load the registration form', () => {
      cy.visit('http://localhost:3000/register');
      cy.get('form#createAccountForm').should('be.visible');
      cy.get('input#username').should('be.visible');
      cy.get('input#email').should('be.visible');
      cy.get('input#password').should('be.visible');
      cy.get('button[type="submit"]').should('be.visible');
    });
  
    it('should show required field errors on empty form submission', () => {
      cy.visit('http://localhost:3000/register');
      cy.get('button[type="submit"]').click();
  
      cy.get('input#username').then(($input) => {
        expect($input[0].checkValidity()).to.be.false;
      });
      cy.get('input#email').then(($input) => {
        expect($input[0].checkValidity()).to.be.false;
      });
      cy.get('input#password').then(($input) => {
        expect($input[0].checkValidity()).to.be.false;
      });
    });
  
    it('should show an error for invalid email format', () => {
      cy.visit('http://localhost:3000/register');
      cy.get('input#username').type('testuser');
      cy.get('input#email').type('invalid-email'); 
      cy.get('input#password').type('Password123');
      cy.get('button[type="submit"]').click();
  
      cy.get('input#email').then(($input) => {
        expect($input[0].checkValidity()).to.be.false;
      });
    });
  
    it('should successfully submit the form with valid inputs', () => {
        cy.visit('http://localhost:3000/register');
      
        const uniqueUsername = `testuser_${Date.now()}`;
        const uniqueEmail = `testuser_${Date.now()}@example.com`;
      
        cy.get('input#username').type(uniqueUsername);
        cy.get('input#email').type(uniqueEmail);
        cy.get('input#password').type('Password123');
      
        cy.get('button[type="submit"]').click();
      
        cy.url().should('include', '/login');
        cy.contains('Login').should('be.visible');
      });
      
  
  });
  