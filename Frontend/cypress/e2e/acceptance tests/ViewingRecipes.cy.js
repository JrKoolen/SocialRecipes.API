/* describe('Acceptance Test: As a user i want to be able view recipes', () => {
  const baseUrl = `http://localhost:${Cypress.env('PORT') || 3001}/`;

    it('should navigate to each recipe detail page when clicking a card', () => {
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
    }); */