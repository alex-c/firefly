import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);

// JWT decoding utility
const jwtDecode = require('jwt-decode');

// Import modules
import ui from './modules/store.ui.js';

// Load user data from local storage
const token = localStorage.getItem('token');
const user = localStorage.getItem('user');
const name = localStorage.getItem('name');

// Set up store
export default new Vuex.Store({
  state: {
    token: token,
    user: user,
    name: name,
  },
  mutations: {
    login(state, token) {
      const decodedToken = jwtDecode(token);
      const user = decodedToken.sub;
      const name = decodedToken.name;
      state.token = token;
      state.user = user;
      state.name = name;
      localStorage.setItem('token', token);
      localStorage.setItem('user', user);
      localStorage.setItem('name', name);
    },
    logout(state) {
      state.token = null;
      state.user = null;
      state.name = null;
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      localStorage.removeItem('name');
    },
  },
  actions: {},
  modules: { ui },
});
