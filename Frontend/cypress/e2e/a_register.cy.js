describe('Create Account Page', () => {
    const baseUrl = 'http://localhost:3000/register';

    const formFields = {
        username: 'input#username',
        email: 'input#email',
        password: 'input#password',
        submitButton: 'button[type="submit"]',
    };

    it('should load the registration form successfully', () => {
        cy.visit(baseUrl);
        cy.get('form#createAccountForm').should('be.visible');
        cy.get(formFields.username).should('be.visible');
        cy.get(formFields.email).should('be.visible');
        cy.get(formFields.password).should('be.visible');
        cy.get(formFields.submitButton).should('be.visible');
    });

    it('should show required field errors on empty form submission', () => {
        cy.visit(baseUrl);
        cy.get(formFields.submitButton).click();

        cy.get(formFields.username).should('have.attr', 'required');
        cy.get(formFields.username).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });

        cy.get(formFields.email).should('have.attr', 'required');
        cy.get(formFields.email).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });

        cy.get(formFields.password).should('have.attr', 'required');
        cy.get(formFields.password).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });
    });

    it('should show an error for invalid email format', () => {
        cy.visit(baseUrl);
        cy.get(formFields.username).type('testuser1');
        cy.get(formFields.email).type('invalid-email');
        cy.get(formFields.password).type('Password123');
        cy.get(formFields.submitButton).click();

        cy.get(formFields.email).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });
    });

    it('should successfully submit the form with valid inputs', () => {
        const uniqueEmail = `testuser-${Date.now()}@example.com`;

        cy.visit(baseUrl);
        cy.get(formFields.username).type('testuser');
        cy.get(formFields.email).type(uniqueEmail);
        cy.get(formFields.password).type('Password123');
        cy.get(formFields.submitButton).click();

        cy.url().should('include', '/login');
        cy.contains('Login').should('be.visible');
    });
});
