/* describe('Acceptance Test: As a user, I want to be able to navigate while logged in', () => {
    const baseUrl = `http://localhost:${Cypress.env('PORT') || 3001}/`;

    it('should allow navigation as a logged-in user (mock session)', () => {
        cy.visit(`${baseUrl}user-page`); 
        cy.url().should('include', '/user-page'); 
        cy.contains('Welcome').should('be.visible');
    });

    it('should logout successfully', () => {
        cy.visit(`${baseUrl}logout`);
        cy.contains('Login').should('be.visible'); 
    });
});
 */