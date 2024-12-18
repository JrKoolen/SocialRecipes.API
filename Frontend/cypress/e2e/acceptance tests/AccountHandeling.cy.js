/* describe('Acceptance Test: As a user, I want to be able to create an account and login', () => {
    const baseUrl = `http://localhost:${Cypress.env('PORT') || 3000}/`;
    const uniqueEmail = `testuser-${Date.now()}@example.com`;
    const username = `testuser-${Date.now()}`;
    const password = `Password123${Date.now()}`;

    const formFields = {
        username: 'input#username',
        email: 'input#email',
        password: 'input#password',
        submitButton: 'button[type="submit"]',
    };

    Cypress.Commands.add('fakeLogin', () => {
        cy.request('POST', `${baseUrl}api/mock-login`, {
            username: 'Jan',
            token: 'mockAuthToken',
            id: 1,
            isLoggedIn: true,
        }).then((response) => {
            expect(response.status).to.eq(200);
        });
    });

    it('should create an account with unique credentials', () => {
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

    it('should fake a login and navigate to the user page', () => {
        cy.fakeLogin(); 
        cy.visit(`${baseUrl}user-page`); 
        cy.url().should('include', '/user-page'); 
        cy.contains('Welcome').should('be.visible');
    });

    it('should be able to logout', () => {
        cy.visit(`${baseUrl}logout`);
        cy.contains('Login').should('be.visible');
    });
});
 */