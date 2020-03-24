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
  height: 56px;
}

#header-buttons {
  float: right;
}

.header-button {
  width: 32px;
  height: 32px;
  padding: 12px;
  font-size: 28px;
  text-align: center;
  &:hover {
    background-color: $primary-dark;
    color: $light;
    cursor: pointer;
  }
}
</style>