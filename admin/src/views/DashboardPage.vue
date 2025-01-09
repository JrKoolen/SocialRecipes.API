<template>
  <div>
    <h1 class="dashboard-title">Dashboard</h1>
    <p v-if="loading">Loading data...</p>
    <div v-else class="dashboard-grid">
      <div 
        class="dashboard-box" 
        :class="{ 'service-up': apiStatus === 'Service is running', 'service-down': apiStatus !== 'Service is running' }"
      >
        <h2>API Status</h2>
        <p>{{ apiStatus || 'Loading...' }}</p>
        <p>Last updated: {{ apiTimestamp || 'N/A' }}</p>
      </div>

      <div 
        class="dashboard-box" 
        :class="{ 'service-up': databaseStatus === 'Database is running', 'service-down': databaseStatus !== 'Database is running' }"
      >
        <h2>Database Status</h2>
        <p>{{ databaseStatus || 'Loading...' }}</p>
        <p>Last updated: {{ dbTimestamp || 'N/A' }}</p>
      </div>

      <div 
        class="dashboard-box" 
        :class="{ 'service-up': commitStatus === 'success', 'service-down': commitStatus !== 'success' }"
      >
        <h2>Latest Commit action Status</h2>
        <p>{{ commitMessage || 'No Commit Data Available' }}</p>
        <p>Status: {{ commitStatus || 'N/A' }}</p>
        <p>Last updated: {{ commitTimestamp || 'N/A' }}</p>
        <p><a :href="commitUrl" target="_blank" v-if="commitUrl">View Commit</a></p>
      </div>

      <div 
        class="dashboard-box" 
        :class="{ 'service-up': localhostStatus === 'Reachable', 'service-down': localhostStatus !== 'Reachable' }"
      >
        <h2>Frontend Status</h2>
        <p>{{ localhostStatus || 'Loading...' }}</p>
        <small>Last checked: {{ localhostTimestamp || 'N/A' }}</small>
      </div>

    </div>
  </div>
</template>



<script>
import { getApiStatus, getDatabaseStatus, GetLatestCommitStatus } from '../services/api'; 
import axios from 'axios';

export default {
  name: 'DashboardPage',
  data() {
    return {
      apiStatus: '', 
      databaseStatus: '', 
      apiTimestamp: '',
      dbTimestamp: '',  
      commitStatus: '', 
      commitMessage: '', 
      commitTimestamp: '', 
      commitUrl: '', 
      loading: true, 
    };
  },
  methods: {
    async fetchData() {
      this.loading = true;

      try {
        const [apiResponse, dbResponse] = await Promise.all([
          getApiStatus(),
          getDatabaseStatus(),
        ]);

        this.apiStatus = apiResponse.data.status || 'Unknown';
        this.apiTimestamp = apiResponse.data.timestamp || 'N/A';

        this.databaseStatus = dbResponse.data.status || 'Unknown';
        this.dbTimestamp = dbResponse.data.timestamp || 'N/A';
      } catch (error) {
        console.error('Error fetching API/Database data:', error);
        this.apiStatus = 'Error';
        this.databaseStatus = 'Error';
        this.apiTimestamp = 'N/A';
        this.dbTimestamp = 'N/A';
      }

      try {
        const commitResponse = await GetLatestCommitStatus();
        console.log('Commit Response:', commitResponse.data);

        if (commitResponse.data.workflow_runs && commitResponse.data.workflow_runs.length > 0) {
          const latestRun = commitResponse.data.workflow_runs[0];
          this.commitStatus = latestRun.conclusion || 'Unknown';
          this.commitMessage = latestRun.name || 'No Message';
          this.commitTimestamp = latestRun.updated_at || 'N/A';
          this.commitUrl = latestRun.html_url || null;
        } else {
          this.commitStatus = 'No Data';
          this.commitMessage = 'No Commit Data Available';
          this.commitTimestamp = 'N/A';
          this.commitUrl = null;
        }
      } catch (error) {
        console.error('Error fetching commit status:', error);
        this.commitStatus = 'Error';
        this.commitMessage = 'Error fetching commit data';
        this.commitTimestamp = 'N/A';
        this.commitUrl = null;
      }

      try {
        const localhostResponse = await axios.get('http://localhost:3000/recipes');
        console.log('Localhost Response:', localhostResponse.status);
        this.localhostStatus = localhostResponse.status === 200 ? 'Reachable' : 'Unreachable';
        this.localhostTimestamp = new Date().toISOString();
      } catch (error) {
        console.error('Error checking localhost:3000:', error.message);
        this.localhostStatus = 'Unreachable';
        this.localhostTimestamp = new Date().toISOString();
      } finally {
        this.loading = false;
      }
    },
  },
  created() {
    this.fetchData();
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
  gap: 20px;
  margin-top: 20px;
}

.dashboard-box {
  background: #fff;
  border: 2px solid #000000;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 40px;
  text-align: center;
  font-size: 18px;
  font-weight: bold;
  border-radius: 8px;
  height: 200px;
  width: 90%;
  transition: background-color 0.3s ease; 
}

.service-up {
  background-color: hsl(134, 61%, 41%); 
  color: white;
}

.service-down {
  background-color: #dc3545; 
  color: white;
}
</style>