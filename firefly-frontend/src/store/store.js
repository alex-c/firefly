import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);

// JWT decoding utility
const jwtDecode = require('jwt-decode');

// Import modules
import ui from './modules/store.ui.js';

// Load user data from local storage
const token = localStorage.getItem('token');
const id = localStorage.getItem('id');
const name = localStorage.getItem('name');
const role = localStorage.getItem('role');

// Set up store
export default new Vuex.Store({
  state: {
    token: token,
    id: id,
    name: name,
    role: role,
  },
  mutations: {
    login(state, token) {
      const decodedToken = jwtDecode(token);
      const id = decodedToken.sub;
      const name = decodedToken.name;
      const role = decodedToken.role ? decodedToken.role : 'None';
      state.token = token;
      state.id = id;
      state.name = name;
      state.role = role;
      localStorage.setItem('token', token);
      localStorage.setItem('id', id);
      localStorage.setItem('name', name);
      localStorage.setItem('role', role);
    },
    logout(state) {
      state.token = null;
      state.id = null;
      state.name = null;
      state.role = null;
      localStorage.removeItem('token');
      localStorage.removeItem('id');
      localStorage.removeItem('name');
      localStorage.removeItem('role');
    },
  },
  actions: {},
  modules: { ui },
});
