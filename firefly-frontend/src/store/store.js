import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);

// JWT decoding utility
const jwtDecode = require('jwt-decode');

// Import modules
import ui from './modules/store.ui.js';

// Load user data from local storage
const token = localStorage.getItem('token');
const email = localStorage.getItem('email');
const name = localStorage.getItem('name');
const admin = localStorage.getItem('admin') === 'true';

// Set up store
export default new Vuex.Store({
  state: {
    token: token,
    email: email,
    name: name,
    admin: admin,
  },
  mutations: {
    login(state, token) {
      const decodedToken = jwtDecode(token);
      const email = decodedToken.sub;
      const name = decodedToken.name;
      const admin = decodedToken.admin === 'true';
      state.token = token;
      state.email = email;
      state.name = name;
      state.admin = admin;
      localStorage.setItem('token', token);
      localStorage.setItem('email', email);
      localStorage.setItem('name', name);
      localStorage.setItem('admin', admin);
    },
    logout(state) {
      state.token = null;
      state.email = null;
      state.name = null;
      state.admin = null;
      localStorage.removeItem('token');
      localStorage.removeItem('email');
      localStorage.removeItem('name');
      localStorage.removeItem('admin');
    },
  },
  actions: {},
  modules: { ui },
});
