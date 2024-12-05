describe('template spec', () => {
  const baseUrl = `http://localhost:${Cypress.env('PORT') || 3001}/login`;
  it('passes', () => {
    cy.visit('https://example.cypress.io')
  })
})