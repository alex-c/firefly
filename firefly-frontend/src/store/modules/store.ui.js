export default {
  state: {
    uiCollapsed: false,
    settingsShown: false,
    theme: 'dark',
  },
  mutations: {
    // UI state: collapsed/enabled
    toggleUiState(state) {
      state.uiCollapsed = !state.uiCollapsed;
    },
    collapseUi(state) {
      state.uiCollapsed = true;
    },
    expandUi(state) {
      state.uiCollapsed = false;
    },
    // Settings state: shown/hidden
    toggleSettings(state) {
      state.settingsShown = !state.settingsShown;
    },
    showSettings(state) {
      state.settingsShown = true;
    },
    hideSettings(state) {
      state.settingsShown = false;
    },
    // Theme
    setTheme(state, theme) {
      state.theme = theme;
    },
  },
  actions: {},
  getters: {},
};
