// File: cypress/integration/recipe_cards.spec.js

describe('Recipe Cards Page Tests', () => {
    beforeEach(() => {
      cy.visit('/recipes');
    });

//   it('should create three recipes with different names', () => {
//     const recipes = [
//       { title: 'Spaghetti Bolognese', body: 'Recipe body 1', description: 'A classic Italian dish' },
//       { title: 'Chicken Curry', body: 'Recipe body 2', description: 'A flavorful curry recipe' },
//       { title: 'Vegan Pancakes', body: 'Recipe body 3', description: 'Delicious pancakes for vegans' },
//     ];

//     recipes.forEach((recipe) => {
//       cy.request({
//         method: 'POST',
//         url: '/create-recipe',
//         form: true,
//         body: {
//           title: recipe.title,
//           body: recipe.body,
//           description: recipe.description,
//         },
//       }).then((response) => {
//         expect(response.status).to.eq(200);
//         expect(response.redirects).to.contain('/recipes');
//       });
//     });
//   });
  
    it('should display the recipe search bar', () => {
      cy.get('input#searchInput').should('be.visible').and('have.attr', 'placeholder', 'Search for recipes...');
    });
  
    // it('should display recipe cards', () => {
    //   cy.get('.recipe-grid .recipe-card').should('exist').and('be.visible');
    // });
  
    // it('should filter recipes based on search input', () => {
    //     cy.get('input#searchInput').type('recipe');
      
    //     cy.get('.recipe-card:visible').each(($card) => {
    //       cy.wrap($card)
    //         .find('.recipe-title')
    //         .invoke('text')
    //         .then((text) => {
    //           expect(text.toLowerCase()).to.include('recipe');
    //         });
    //     });
      
    //     cy.get('.recipe-card:visible').should('have.length.greaterThan', 0);
    //   });
      
  
    it('should hide recipes that do not match the search input', () => {
      cy.get('input#searchInput').type('NonExistentRecipe');
      cy.get('.recipe-card:visible').should('have.length', 0);
    });
  
    // it('should navigate to the recipe detail page when a card is clicked', () => {
    //   cy.get('.recipe-card').first().click();
  
    //   cy.url().should('include', '/recipe/');
    // });
});