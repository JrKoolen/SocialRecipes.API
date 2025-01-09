describe('Seting up the e2e tests:', () => {
    const baseUrl = `http://localhost:${Cypress.env('PORT') || 3000}/`;
    const uniqueEmail = `testuser-${Date.now()}@example.com`;
    const username = `test${Date.now()}`;
    const password = `Password123${Date.now()}`;

    const formFields = {
        username: 'input#username',
        email: 'input#email',
        password: 'input#password',
        submitButton: 'button[type="submit"]',
    };

    describe('API /status Endpoint', () => {
        it('should return a successful response', () => {
          cy.request('http://localhost:8080/api/status') 
            .then((response) => {
              expect(response.status).to.eq(200);
            });
        });
        it('should return a successful response', () => {
            cy.request('http://localhost:8080/api/status/database') 
              .then((response) => {
                expect(response.status).to.eq(200);
              });
          });
        it('should return a successful response', () => {
            cy.request('http://localhost:8080/api/User/GetTotalUsers') 
              .then((response) => {
                expect(response.status).to.eq(200);
              });
          });
      });

    it('should create a account with unique credentials', () =>{
        cy.visit(`${baseUrl}register`);
        cy.get(formFields.username).type(username);
        cy.get(formFields.email).type(uniqueEmail);
        cy.get(formFields.password).type(password);
        cy.get(formFields.submitButton).click();

        cy.url().should('include', '/login');
        cy.contains('Login').should('be.visible');
    });

    it('should be able to login after registration', () => {
        cy.visit(`${baseUrl}mockAuth`);
        cy.url().should('include', '/user-page'); 
        cy.contains('Welcome').should('be.visible'); 
    });

    it('should be able to create a recipe after login', () => {

        cy.visit(`${baseUrl}mockAuth`);

        cy.visit(`${baseUrl}create-recipe`);
        cy.get('input#title').type('My Delicious Recipe');
        cy.get('textarea#description').type('This is a short description of the recipe.');
        cy.get('textarea#body').type('Here is the detailed recipe body with instructions.');
        cy.get('input#image').selectFile('cypress/fixtures/test.jpg'); 
        cy.get('button[type="submit"]').click();


        cy.visit(`${baseUrl}user-page`);
        cy.contains('My Delicious Recipe').should('be.visible');
    });


    it('recipes from user should be visible', () => {
        cy.visit(`${baseUrl}mockAuth`);
        cy.visit(`${baseUrl}user-page`);
        cy.contains('My Delicious Recipe').should('be.visible');

        cy.get('.recipe-card').then((cards) => {
            const cardCount = cards.length;
      
            for (let i = 0; i < cardCount; i++) {
              cy.get('.recipe-card')
                .eq(i) 
                .click({ force: true }); 
      
              cy.url().should('include', '/recipe/');
              cy.get('.recipe-title').should('be.visible');
              cy.get('.recipe-description').should('be.visible');
              cy.get('.recipe-body').should('be.visible');
              cy.get('.recipe-image').should('be.visible');
      
              cy.visit(`${baseUrl}recipes`);
              cy.contains('recipes').should('be.visible');
            }
        });
    });

    it('should show recipes on the main page', () => {  
        cy.visit(`${baseUrl}recipes`);
        cy.contains('recipes').should('be.visible');
        cy.get('.recipe-card').then((cards) => {
            const cardCount = cards.length;
      
            for (let i = 0; i < cardCount; i++) {
              cy.get('.recipe-card')
                .eq(i) 
                .click({ force: true }); 
      
              cy.url().should('include', '/recipe/');
              cy.get('.recipe-title').should('be.visible');
              cy.get('.recipe-description').should('be.visible');
              cy.get('.recipe-body').should('be.visible');
              cy.get('.recipe-image').should('be.visible');
      
              cy.visit(`${baseUrl}recipes`);
              cy.contains('recipes').should('be.visible');
            }
        });
    });

    it ('should be able to logout', () => {
        cy.visit(`${baseUrl}logout`);
        cy.contains('Login').should('be.visible');
    });
});