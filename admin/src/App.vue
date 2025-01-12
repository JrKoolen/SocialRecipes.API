<template>
  <div id="admin-panel">
    <aside id="sidebar">
      <ul>
        <li>
          <router-link to="/dashboard" class="styled-link">Dashboard</router-link>
        </li>
        <li>
          <router-link to="/analytics" class="styled-link">Analytics</router-link>
        </li>
        <li>
          <router-link to="/adminpanel" class="styled-link">adminpanel</router-link>
        </li>
      </ul>
    </aside>
    <main id="main-content">
      <router-view />
    </main>
  </div>
</template>

<script>
export default {
  name: 'AdminPanel',
  methods: {
    navigateTo(route) {
      this.$router.push(route);
    },
  },
};
</script>

<style scoped>
#admin-panel {
  display: flex;
  height: 100vh;
}

#sidebar {
  width: 250px;
  background: #333;
  color: #fff;
  padding: 20px;
  display: flex;
  flex-direction: column;
}

#sidebar ul {
  list-style: none;
  padding: 0;
}

#sidebar ul li {
  margin: 10px 0;
}

.styled-link {
  display: block;
  width: 90%;
  padding: 10px;
  margin-top: 10px;
  background: hsl(155, 100%, 50%);
  color: #fff;
  text-decoration: none;
  text-align: center;
  border-radius: 4px;
  cursor: pointer;
}

.styled-link:hover {
  background: #04eeff;
}

#main-content {
  flex: 1;
  padding: 20px;
  background: #f4f4f4;
  overflow-y: auto;
}
</style>
