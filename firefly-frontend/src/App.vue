<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: 'app',
  computed: {
    theme() {
      return this.$store.state.ui.theme;
    },
  },
  mounted() {
    document.documentElement.classList.add(this.theme);
  },
};
</script>

<style lang="scss">
@import '@/style/colors.scss';

body {
  font-family: 'Raleway', 'Helvetica Neue', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  background-color: var($--theme-background);
  color: var($--theme-text);
  margin: 0;
}

#app {
  text-align: center;
}
</style>
