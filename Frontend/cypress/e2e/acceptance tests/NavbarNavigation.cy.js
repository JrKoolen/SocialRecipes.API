describe('Acceptance Test: As a user i want to be able to easily navigate on the site.', () => {
    const baseUrl = `http://localhost:${Cypress.env('PORT') || 3001}/`;
  
    it('should display the sidebar on all pages', () => {
      cy.visit(baseUrl);
      cy.get('.sidebar').should('be.visible');
      cy.get('.logo-img').should('have.attr', 'src').and('include', 'logo.png'); 
    });
  
    it('should navigate to each section when clicking a sidebar item', () => {
      const navItems = [
        { label: 'Home', path: '/', selector: 'Home' },
        { label: 'Login', path: '/login', selector: 'Login' },
        { label: 'Register', path: '/register', selector: 'Register' },
      ];
  
      navItems.forEach((item) => {
        cy.visit(baseUrl);
        cy.get('.sidebar').invoke('addClass', 'force-hover');
        cy.get('.menu-text').contains(item.selector).click();
        cy.url().should('include', item.path);
      });
    });
  
    it('should highlight the active sidebar item', () => {
      cy.visit(`${baseUrl}login`);
      cy.get('a.active').should('contain', 'Login'); 
  
    it('should render login/register options for unauthenticated users and create-recipe/logout for authenticated users', () => {
      cy.visit(baseUrl);
      cy.get('.menu-text').contains('Login').should('be.visible');
      cy.get('.menu-text').contains('Register').should('be.visible');
      cy.get('.menu-text').contains('Create recipe').should('not.exist');
      cy.get('.menu-text').contains('Logout').should('not.exist');
  
      cy.setCookie('auth_token', 'valid-token'); 
      cy.reload();
  
      cy.get('.menu-text').contains('Login').should('not.exist');
      cy.get('.menu-text').contains('Register').should('not.exist');
      cy.get('.menu-text').contains('Create recipe').should('be.visible');
      cy.get('.menu-text').contains('Logout').should('be.visible');
    });
  
    it('should transform the sidebar for smaller screens', () => {
      cy.viewport('iphone-6'); 
      cy.visit(baseUrl);
  
      cy.get('.hamburger-menu').should('be.visible').click(); 
      cy.get('.sidebar').should('be.visible'); 
    });
  
    it('should allow navigation through the sidebar using the keyboard', () => {
      cy.visit(baseUrl);
      cy.get('body').tab(); 

      cy.get('a').first().should('have.focus');
      cy.focused().type('{enter}'); 
      cy.url().should('include', '/');
    });
  });
});