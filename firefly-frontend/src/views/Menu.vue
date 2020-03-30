<template>
  <aside id="menu" :class="{collapsed}">
    <MenuButton name="dashboard" icon="mdi-view-dashboard" :current-route="currentRoute" />
    <MenuHeader name="finances" icon="mdi-home-currency-usd" :collapsed="collapsed" />
    <MenuButton name="accounts" icon="mdi-bank" :current-route="currentRoute" />
    <MenuButton name="transactions" icon="mdi-bank-transfer" :current-route="currentRoute" />
    <MenuButton name="trends" icon="mdi-finance" :current-route="currentRoute" />
    <MenuHeader name="admin" icon="mdi-shield-key" :collapsed="collapsed" v-if="userIsAdmin" />
    <MenuButton
      name="users"
      routePrefix="/admin"
      icon="mdi-account-group"
      :current-route="currentRoute"
      v-if="userIsAdmin"
    />
    <MenuButton
      name="accounts"
      routePrefix="/admin"
      icon="mdi-bank"
      :current-route="currentRoute"
      v-if="userIsAdmin"
    />
  </aside>
</template>

<script>
import MenuButton from '@/components/MenuButton.vue';
import MenuHeader from '@/components/MenuHeader.vue';

export default {
  name: 'menu',
  components: { MenuButton, MenuHeader },
  props: ['collapsed'],
  data() {
    return {
      userIsAdmin: this.$store.state.role === 'Administrator',
    };
  },
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
  top: 56px;
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