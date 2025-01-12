<template>
    <div>
      <h1 class="adminpanel-title">Admin Panel</h1>
      <div class="adminpanel-grid">
        <div class="adminpanel-box">
          <h2>Delete User</h2>
          <input
            type="text"
            v-model="userId"
            placeholder="Enter User ID"
            class="adminpanel-input"
          />
          <button @click="deleteUser" class="adminpanel-button">Delete User</button>
        </div>
  
        <div class="adminpanel-box">
          <h2>Delete Recipe</h2>
          <input
            type="text"
            v-model="recipeId"
            placeholder="Enter Recipe ID"
            class="adminpanel-input"
          />
          <button @click="deleteRecipe" class="adminpanel-button">Delete Recipe</button>
        </div>
      </div>
    </div>
  </template>
  
<script>
import { delUserFromId, delRecipeFromId } from "../services/api"; 

export default {
  data() {
    return {
      userId: "", 
      recipeId: "", 
    };
  },
  methods: {
    async deleteUser() {
      if (!this.userId) {
        alert("Please enter a valid User ID.");
        return;
      }

      try {
        await delUserFromId(this.userId); 
        alert("User deleted successfully.");
        this.userId = "";
      } catch (error) {
        console.error("Error deleting user:", error);
        alert("Failed to delete user. Please try again.");
      }
    },

    async deleteRecipe() {
      if (!this.recipeId) {
        alert("Please enter a valid Recipe ID.");
        return;
      }

      try {
        await delRecipeFromId(this.recipeId); 
        alert("Recipe deleted successfully.");
        this.recipeId = ""; 
      } catch (error) {
        console.error("Error deleting recipe:", error);
        alert("Failed to delete recipe. Please try again.");
      }
    },
  },
};
</script>

<style scoped>
  .adminpanel-title {
    text-align: center;
    font-size: 36px;
    font-weight: bold;
    margin-bottom: 20px;
  }
  
  .adminpanel-grid {
    display: grid;
    grid-template-columns: repeat(1, 1fr);
    gap: 20px;
    margin-top: 20px;
    justify-content: center;
  }
  
  .adminpanel-box {
    background: #fff;
    border: 2px solid #000000;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    padding: 20px;
    text-align: center;
    font-size: 18px;
    font-weight: bold;
    border-radius: 8px;
    transition: background-color 0.3s ease;
    width: 90%;
    max-width: 400px;
  }
  
  .adminpanel-input {
    display: block;
    margin: 10px auto;
    padding: 8px;
    width: 80%;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  
  .adminpanel-button {
    display: inline-block;
    background-color: #ff4d4d;
    color: #fff;
    padding: 10px 20px;
    font-size: 16px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }
  
  .adminpanel-button:hover {
    background-color: #e60000;
  }
</style>
  