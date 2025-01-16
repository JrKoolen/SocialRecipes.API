const BASE_URL = process.env.API_BASE_URL || (process.env.NODE_ENV === 'production'
  ? 'http://socialrecipes-api-container:8080/api'
  : 'http://socialrecipes-api-container:8080/api');
module.exports = {
  AUTH: {
    LOGIN: `${BASE_URL}/Auth/login`,
    REGISTER: `${BASE_URL}/Auth/register`
  },
  USER: {
    DELETE_USER: (userId) => `${BASE_URL}/User/${userId}`,
    //GET_USER_BY_ID: (userId) => `${BASE_URL}/User/${userId}`
  },
  RECIPE: {
    CREATE_RECIPE: `${BASE_URL}/Recipe/CreateRecipe`,
    UPDATE_RECIPE: `${BASE_URL}/Recipe/UpdateRecipe`,
    GET_RECIPE_BY_ID: (recipeId) => `${BASE_URL}/Recipe/GetRecipeById/${recipeId}`,
    GET_ALL_RECIPES_FROM_USER: (userId) => `${BASE_URL}/Recipe/GetAllRecipesFromUser/${userId}`,
    GET_ALL_RECIPES: `${BASE_URL}/Recipe/GetAllRecipes`,
    GET_ALL_RECIPES_FROM_STATUS: (status) => `${BASE_URL}/Recipe/GetAllRecipesFromStatus/${status}`,
    GET_ALL_RECIPES_FROM_STATUS_AND_USER: (status, userId) => `${BASE_URL}/Recipe/GetAllRecipesFromStatusAndUser/${status}/${userId}`,
    DELETE_RECIPE_BY_ID: (recipeId) => `${BASE_URL}/Recipe/DeleteRecipeFromId?id=${recipeId}` ,
    GET_FEATURED_RECIPES: `${BASE_URL}/Recipe/GetFeaturedRecipes`
  },
  FOLLOW: {
    FOLLOW_USER: (userId) => `${BASE_URL}/Follow/Follow/${userId}`,
    GET_FOLLOWERS: (userId) => `${BASE_URL}/Follow/GetFollowers/${userId}`,
    GET_FOLLOWING: (userId) => `${BASE_URL}/Follow/GetFollowing/${userId}`,
    UNFOLLOW_USER: (userId) => `${BASE_URL}/Follow/Unfollow/${userId}`
  },
  COMMENT: {
    ADD_COMMENT: `${BASE_URL}/Comment/Add`,
    GET_COMMENTS_BY_RECIPE: (recipeId) => `${BASE_URL}/Comment/ByRecipe/${recipeId}`,
    GET_COMMENTS_BY_USER: (userId) => `${BASE_URL}/Comment/ByUser/${userId}`,
    DELETE_COMMENT: (commentId) => `${BASE_URL}/Comment/Delete/${commentId}`
  }
};