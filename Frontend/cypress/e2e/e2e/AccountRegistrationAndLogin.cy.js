describe('API Status Tests', () => {
    const apiStatusEndpoint = 'http://localhost:8080/api/status';

    it('should verify that the API is running', () => {
        cy.request(apiStatusEndpoint).then((response) => {
            expect(response.status).to.eq(200);
        });
    });
});

const uniqueEmail = `testuser-${Date.now()}@example.com`;
const uniqueUsername = `testuser-${Date.now()}`;
const uniquePassword = `Password123${Date.now()}`;

describe('Register Tests', () => {
    const baseUrl = 'http://localhost:3000/';
    const RegisterFields = {
        username: 'input#username',
        email: 'input#email',
        password: 'input#password',
        submitButton: 'button[type="submit"]',
    };

    it('should load the registration form successfully', () => {
        cy.visit(`${baseUrl}register`);
        cy.get('form#createAccountForm').should('be.visible');
        cy.get(RegisterFields.username).should('be.visible');
        cy.get(RegisterFields.email).should('be.visible');
        cy.get(RegisterFields.password).should('be.visible');
        cy.get(RegisterFields.submitButton).should('be.visible');
    });

    it('should show required field errors on empty form submission', () => {
        cy.visit(`${baseUrl}register`);
        cy.get(RegisterFields.submitButton).click();

        cy.get(RegisterFields.username).should('have.attr', 'required');
        cy.get(RegisterFields.username).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });

        cy.get(RegisterFields.email).should('have.attr', 'required');
        cy.get(RegisterFields.email).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });

        cy.get(RegisterFields.password).should('have.attr', 'required');
        cy.get(RegisterFields.password).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });
    });

    it('should show an error for invalid email format', () => {
        cy.visit(`${baseUrl}register`);
        cy.get(RegisterFields.username).type('testuser1');
        cy.get(RegisterFields.email).type('invalid-email');
        cy.get(RegisterFields.password).type('Password123');
        cy.get(RegisterFields.submitButton).click();

        cy.get(RegisterFields.email).then(($input) => {
            expect($input[0].checkValidity()).to.be.false;
        });
    });

    it('should successfully submit the form with valid inputs', () => {
        const uniqueEmail = `testuser-${Date.now()}@example.com`;

        cy.visit(`${baseUrl}register`);
        cy.get(RegisterFields.username).type(uniqueUsername);
        cy.get(RegisterFields.email).type(uniqueEmail);
        cy.get(RegisterFields.password).type(uniquePassword);
        cy.get(RegisterFields.submitButton).click();

        cy.url().should('include', '/login');
        cy.contains('Login').should('be.visible');
    });

    it('should load the login page successfully', () => {
        cy.visit(`${baseUrl}login`);
        cy.contains('Login').should('be.visible'); 
      });
    
      it('should show an error on submitting empty login form', () => {
        cy.visit(`${baseUrl}login`);
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
        cy.visit(`${baseUrl}login`);
        cy.get('input[name="username"]').type('invalidUsername1');
        cy.get('input[name="password"]').type('wrongpassword1');
        cy.get('button[type="submit"]').click();
    
      cy.contains('Please try again.').should('be.visible');
      });

      it('should login with valid credentials', () => {
        cy.visit(`${baseUrl}login`);
        cy.get('input[name="username"]').type('Jan');
        cy.get('input[name="password"]').type('Jan');
        cy.get('button[type="submit"]').click();
    
      cy.url().should('include', '/user-page'); 
      cy.contains('Welcome').should('be.visible'); 
    });
});
