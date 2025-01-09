<template>
  <div>
    <h1 class="dashboard-title">Analytics</h1>
    <div class="dashboard-grid">
      <div class="dashboard-box">
        <p>Total users</p>
        <p>{{ totalUsers }}</p>
      </div>
      <div class="dashboard-box">
        <p>Total recipes</p>
        <p>{{ totalRecipes }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import { getTotalRecipes, getTotalUsers } from "../services/api"; 

export default {
  name: "AnalyticsPage",
  data() {
    return {
      totalUsers: 0,
      totalRecipes: 0,
    };
  },
  methods: {
    async fetchAnalyticsData() {
      try {
        const [usersResponse, recipesResponse] = await Promise.all([
          getTotalUsers(),
          getTotalRecipes(),
        ]);
        this.totalUsers = usersResponse.data.total || 0; 
        this.totalRecipes = recipesResponse.data.total || 0;
      } catch (error) {
        console.error("Error fetching analytics data:", error);
      }
    },
  },
  mounted() {
    this.fetchAnalyticsData();
  },
};
</script>

<style scoped>
.dashboard-title {
  text-align: center;
  font-size: 36px;
  font-weight: bold;
  margin-bottom: 20px;
}

.dashboard-grid {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 100px;
  margin-top: 20px;
}

.dashboard-box {
  background: #fff;
  border: 2px solid #007bff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 40px;
  text-align: center;
  font-size: 18px;
  font-weight: bold;
  border-radius: 8px;
  height: 200px;
  width: 90%;
}
</style>
