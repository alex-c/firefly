import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

// Views - general
import Login from '@/views/Login.vue';
import Private from '@/views/Private.vue';
import UserAccount from '@/views/pages/UserAccount.vue';
import NotFound from '@/views/pages/NotFound.vue';

// Views - pages
import Dashboard from '@/views/pages/Dashboard.vue';
import Accounts from '@/views/pages/Accounts.vue';
import Transactions from '@/views/pages/Transactions.vue';
import Trends from '@/views/pages/Trends.vue';

// Views - admin pages
import UserAdmin from '@/views/admin-pages/UserAdmin.vue';
import AccountAdmin from '@/views/admin-pages/AccountAdmin.vue';

// Store
import store from '../store/store.js';

// Admin navigation guard
function userIsAdmin(_to, _from, next) {
  if (store.state.role === 'Administrator') {
    next();
  } else {
    next(_from);
  }
}

// Define routes
const routes = [
  {
    path: '/',
    beforeEnter: (_to, _from, next) => {
      if (store.state.token === null) {
        next({ path: '/login' });
      } else {
        next({ path: '/dashboard' });
      }
    },
  },
  {
    path: '/login',
    component: Login,
  },
  {
    path: '/dashboard',
    component: Private,
    beforeEnter: function(_to, _from, next) {
      if (store.state.token === null) {
        next({ path: '/login' });
      } else {
        next();
      }
    },
    children: [
      {
        path: '/',
        component: Dashboard,
      },
      {
        path: '/user',
        component: UserAccount,
        name: 'User Account',
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
        path: '/user-admin',
        component: UserAdmin,
        beforeEnter: userIsAdmin,
      },
      {
        path: '/account-admin',
        component: AccountAdmin,
        beforeEnter: userIsAdmin,
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
