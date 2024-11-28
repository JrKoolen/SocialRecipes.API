describe('Acceptance Test: As a user i want to be able to create a recipe', () => {
    const baseUrl = 'http://localhost:3000/';
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
    });
  
    it('should be able to create a recipe after login', () => {

        cy.visit(`${baseUrl}login`);
        cy.get('input[name="username"]').type(username); 
        cy.get('input[name="password"]').type(password);
        cy.get('button[type="submit"]').click();

        cy.url().should('include', '/user-page'); 
        cy.contains('Welcome').should('be.visible'); 

        cy.visit(`${baseUrl}create-recipe`);
        cy.get('input#title').type('My Delicious Recipe');
        cy.get('textarea#description').type('This is a short description of the recipe.');
        cy.get('textarea#body').type('Here is the detailed recipe body with instructions.');
        cy.get('input#image').selectFile('cypress/fixtures/test.jpg'); 
        cy.get('button[type="submit"]').click();
    

        //cy.url().should('include', '/recipes'); 
       //cy.contains('Recipe created successfully').should('be.visible'); 
        });
    
});

  