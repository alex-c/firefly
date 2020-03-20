import Vue from 'vue';
Vue.config.productionTip = false;

// Router & Vuex
import router from './router';
import store from './store/store.js';

// Element UI & theme
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/reset.css';
import './style/themes.scss';
import enLocale from 'element-ui/lib/locale/lang/en';
Vue.use(ElementUI);

// Get theme from local storage
const theme = localStorage.getItem('theme') || 'dark';
store.commit('setTheme', theme);

// Internationalization lib
import VueI18n from 'vue-i18n';
Vue.use(VueI18n);

// Load messages
import enMessages from './i18n/en.json';
const messages = {
  en: Object.assign(enMessages, enLocale),
};

// Configure internationalization
const language = localStorage.getItem('language') || 'en';
const i18n = new VueI18n({
  locale: language,
  //TODO: deactivated for development! Add back in later: fallbackLocale: 'en',
  messages,
});

// Material Design Icons
import '@mdi/font/css/materialdesignicons.min.css';

// Inject API object
import Api from './Api.js';
Vue.use(Api);

// Mount app
import App from './App.vue';
new Vue({
  router,
  store,
  i18n,
  render: h => h(App),
}).$mount('#app');
