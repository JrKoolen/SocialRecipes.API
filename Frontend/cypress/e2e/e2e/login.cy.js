/* describe('Login Page', () => {
    const baseUrl = `http://localhost:${Cypress.env('PORT') || 3001}/login`;

    const formFields = {
        username: 'input#username',
        password: 'input#password',
        submitButton: 'button[type="submit"]',
    };

    const testData = {
        emptyFields: {
            description: 'should show required field errors on empty form submission',
            data: { username: '', password: '' },
            expectValid: false,
        },
        invalidCredentials: {
            description: 'should show an error for invalid credentials',
            data: { username: 'invaliduser', password: 'wrongpassword' },
            expectValid: false,
        },
        validLogin: {
            description: 'should successfully log in with valid credentials',
            data: { username: 'jan', password: 'jan' },
            expectValid: true,
        },
    };

    function fillForm({ username, password }) {
        if (username) cy.get(formFields.username).type(username);
        if (password) cy.get(formFields.password).type(password);
    }

    it('should load the login form', () => {
        cy.visit(baseUrl);
        cy.get(formFields.username).should('be.visible');
        cy.get(formFields.password).should('be.visible');
        cy.get(formFields.submitButton).should('be.visible');
    });

    Cypress._.each(testData, (test) => {
        it(test.description, () => {
            cy.visit(baseUrl);
            fillForm(test.data);
            cy.get(formFields.submitButton).click();

            if (test.expectValid) {
                cy.url().should('include', '/user-page');
                cy.contains('Welcome').should('be.visible');
            } else {
                if (!test.data.username) {
                    cy.get(formFields.username).then(($input) => {
                        expect($input[0].checkValidity()).to.be.false;
                    });
                }
                if (!test.data.password) {
                    cy.get(formFields.password).then(($input) => {
                        expect($input[0].checkValidity()).to.be.false;
                    });
                }
                if (test.data.username && test.data.password) {
                    cy.contains('error').should('be.visible');
                }
            }
        });
    });
});
 */