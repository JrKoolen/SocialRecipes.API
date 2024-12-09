describe('Acceptance Test: As a user i want to be able to create a account and login', () => {
    const baseUrl = `http://localhost:${Cypress.env('PORT') || 3001}/`;
    const uniqueEmail = `testuser-${Date.now()}@example.com`;
    const username = `testuser-${Date.now()}`;
    const password = `Password123${Date.now()}`;

    const formFields = {
        username: 'input#username',
        email: 'input#email',
        password: 'input#password',
        submitButton: 'button[type="submit"]',
    };

    it('should create a account with unique credentials', () =>{
        cy.visit(`${baseUrl}register`);
        cy.get(formFields.username).type(username);
        cy.get(formFields.email).type(uniqueEmail);
        cy.get(formFields.password).type(password);
        cy.get(formFields.submitButton).click();
        cy.url().should('include', '/login');
        cy.contains('Login').should('be.visible');
        cy.visit(`${baseUrl}logout`);
        cy.contains('Login').should('be.visible');
    
    });
    
    it('should login with with unique credentials', () =>{
        cy.visit(`${baseUrl}login`);
        cy.get('input[name="username"]').type('Jan'); 
        cy.get('input[name="password"]').type('Jan');
        cy.get('button[type="submit"]').click();
        cy.url().should('include', '/user-page'); 
        cy.contains('Welcome').should('be.visible'); 
    });

    it('should be able to logout', () =>{
        cy.visit(`${baseUrl}logout`);
        cy.contains('Login').should('be.visible');
    });
});   