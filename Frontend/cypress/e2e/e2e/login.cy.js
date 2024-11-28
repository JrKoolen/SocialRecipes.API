describe('Login Page Tests', () => {
  it('should load the login page successfully', () => {
    cy.visit('http://localhost:3000/login');
    cy.contains('Login').should('be.visible'); 
  });

  it('should show an error on submitting empty login form', () => {
    cy.visit('http://localhost:3000/login');
    cy.get('button[type="submit"]').click();
  
    cy.get('input[name="username"]').should('have.attr', 'required');
    cy.get('input[name="username"]').then(($input) => {
      expect($input[0].checkValidity()).to.be.false;
    });
  
    cy.get('input[name="password"]').should('have.attr', 'required');
    cy.get('input[name="password"]').then(($input) => {
      expect($input[0].checkValidity()).to.be.false;
    });
  });

  it('should show an error with invalid credentials', () => {
    cy.visit('http://localhost:3000/login');
    cy.get('input[name="username"]').type('invalidUsername1');
    cy.get('input[name="password"]').type('wrongpassword1');
    cy.get('button[type="submit"]').click();

  cy.contains('Please try again.').should('be.visible');
  });
});