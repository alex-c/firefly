import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

// Views
import Login from '@/views/Login.vue';
import Private from '@/views/Private.vue';
import Dashboard from '@/views/pages/Dashboard.vue';
import Accounts from '@/views/pages/Accounts.vue';
import Transactions from '@/views/pages/Transactions.vue';
import Trends from '@/views/pages/Trends.vue';
import NotFound from '@/views/pages/NotFound.vue';

// Define routes
const routes = [
  {
    path: '/login',
    component: Login,
  },
  {
    path: '/dashboard',
    component: Private,
    children: [
      {
        path: '/',
        component: Dashboard,
      },
      {
        path: '/accounts',
        component: Accounts,
      },
      {
        path: '/transactions',
        component: Transactions,
      },
      {
        path: '/trends',
        component: Trends,
      },
      {
        path: '*',
        component: NotFound,
      },
    ],
  },
  {
    path: '*',
    redirect: '/',
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
