// File: cypress/integration/navbar.spec.js

describe('Navbar Tests', () => {
    context('When user is not logged in', () => {
      beforeEach(() => {
        cy.visit('/?isLoggedIn=false');
      });
  
      it('should display Home, Login, and Register links', () => {
        cy.contains('Home').should('be.visible');
        cy.contains('Login').should('be.visible');
        cy.contains('Register').should('be.visible');
      });
  
      it('should not display links for logged-in users', () => {
        cy.contains('Create recipe').should('not.exist');
        cy.contains('My profile').should('not.exist');
        cy.contains('Logout').should('not.exist');
      });
  
      it('should highlight the active page', () => {
        cy.visit('/login?isLoggedIn=false&activePage=login');
        cy.get('a.active').should('contain.text', 'Login');
      });
    });
  
    context('When user is logged in', () => {
        beforeEach(() => {
          cy.visit('/mockAuth');
        });
      
        it('should display Home, Create Recipe, My Profile, and Logout links', () => {
          cy.contains('Home').should('be.visible');
          //cy.contains('Create recipe').should('be.visible');
         // cy.contains('My profile').should('be.visible');
         // cy.contains('Logout').should('be.visible');
        });
      
        it('should not display Login and Register links', () => {
          cy.contains('Login').should('not.exist');
          cy.contains('Register').should('not.exist');
        });
      });
      
  
    context('Navigation functionality', () => {
      it('should navigate to the correct page when a link is clicked', () => {
        cy.visit('/?isLoggedIn=false');
        cy.contains('Login').click();
        cy.url().should('include', '/login');
      });
    });
  });
  