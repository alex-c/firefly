import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);

// Import modules
import ui from './modules/store.ui.js';

// Load user data from local storage
const token = localStorage.getItem('token');
const user = localStorage.getItem('user');
const role = localStorage.getItem('role');
const name = localStorage.getItem('name');

export default new Vuex.Store({
  state: {
    token: token,
    user: user,
    role: role,
    name: name,
  },
  mutations: {},
  actions: {},
  modules: { ui },
});
