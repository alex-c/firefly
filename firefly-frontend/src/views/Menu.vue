<template>
  <aside id="menu" :class="{collapsed}">
    <MenuButton name="dashboard" icon="mdi-view-dashboard" :current-route="currentRoute" />
    <MenuButton name="accounts" icon="mdi-bank" :current-route="currentRoute" />
    <MenuButton name="transactions" icon="mdi-bank-transfer" :current-route="currentRoute" />
    <MenuButton name="trends" icon="mdi-finance" :current-route="currentRoute" />
  </aside>
</template>

<script>
import MenuButton from '@/components/MenuButton.vue';

export default {
  name: 'menu',
  components: { MenuButton },
  props: ['collapsed'],
  computed: {
    currentRoute() {
      return this.$route.path;
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

#menu {
  position: fixed;
  top: 72px;
  left: 0px;
  bottom: 0px;
  width: 200px;
  background-color: var($--theme-foreground);
  text-align: left;
  font-size: 16px;
  transition: all 0.5s ease-in-out;
  overflow: hidden;
  &.collapsed {
    width: 52px;
  }
}
</style>