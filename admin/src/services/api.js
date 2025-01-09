import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:8080/api', 
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
export const delUserFromId = () => api.delete(`/User/DeleteUser/`);
export const delRecipeFromId = () => api.delete(`/Recipe/DeleteRecipeFromId/{}`);
export const GetLatestCommitStatus = () => githubApi.get('/repos/JrKoolen/SocialRecipes.API/actions/workflows/CD%20%26%20CD%20pipeline.yml/runs');


export default api;