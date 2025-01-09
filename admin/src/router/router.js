import { createRouter, createWebHistory } from 'vue-router';
import DashboardPage from '../views/DashboardPage.vue';
import AnalyticsPage from '../views/AnalyticsPage.vue';

const routes = [
  { path: '/dashboard', name: 'DashboardPage', component: DashboardPage },
  { path: '/analytics', name: 'AnalyticsPage', component: AnalyticsPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
