import axios from 'axios';

const api = axios.create({
    baseURL: 'https://socialrecipesapi-e4hjhse0f6cxfgen.westeurope-01.azurewebsites.net/api', 
    headers: {
      'Content-Type': 'application/json',
    },
    withCredentials: true, 
  });

  const githubApi = axios.create({
    baseURL: 'https://api.github.com',
    headers: {
      'Content-Type': 'application/json',
    },
    withCredentials: false,
  });


export const getApiStatus= () => api.get('/status'); 
export const getDatabaseStatus = () => api.get('/status/database');
export const getTotalRecipes = () => api.get('/Recipe/GetTotalRecipes');
export const getTotalUsers = () => api.get('/User/GetTotalUsers');
export const delUserFromId = (userId) => 
  api.delete(`/User/DeleteUser/${userId}`); 
export const delRecipeFromId = (recipeId) => 
  api.delete(`/Recipe/DeleteRecipeFromId/${recipeId}`); 
export const GetLatestCommitStatus = () => githubApi.get('/repos/JrKoolen/SocialRecipes.API/actions/workflows/CD%20%26%20CD%20pipeline.yml/runs');


export default api;


