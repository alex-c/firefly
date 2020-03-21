<template>
  <header id="header">
    <slot />
    <div id="header-buttons">
      <div class="header-button" @click="toggleSettings">
        <span class="mdi mdi-cog"></span>
      </div>
    </div>
    <slot name="header-right" />
  </header>
</template>

<script>
export default {
  name: 'header',
  props: ['collapsed'],
  methods: {
    toggleSettings: function() {
      this.$store.commit('toggleSettings');
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

#header {
  background-color: var($--theme-primary);
  color: var($--theme-primary-contrast);
  text-align: left;
  height: 72px;
}

#header-buttons {
  float: right;
}

.header-button {
  width: 40px;
  height: 40px;
  padding: 16px;
  font-size: 36px;
  &:hover {
    background-color: $primary-dark;
    color: $light;
    cursor: pointer;
  }
}
</style>