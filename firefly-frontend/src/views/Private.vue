<template>
  <div id="view-private">
    <Header>
      <div id="header-title" :class="{collapsed: uiCollapsed}">
        <span v-if="!uiCollapsed">Firefly</span>
        <span v-else>F</span>
      </div>
      <div id="header-ui-ctrl" class="header-button" @click="toggleUiState">
        <span class="mdi mdi-menu-open" :class="{ 'mdi-rotate-180': uiCollapsed }" />
      </div>
    </Header>
    <Menu :collapsed="uiCollapsed" />
    <main id="content" :class="{expanded: uiCollapsed}">
      <router-view></router-view>
    </main>
    <Settings />
  </div>
</template>

<script>
import Header from '@/components/Header.vue';
import Settings from '@/views/Settings.vue';
import Menu from '@/views/Menu.vue';

export default {
  name: 'private',
  components: { Header, Settings, Menu },
  computed: {
    uiCollapsed() {
      return this.$store.state.ui.uiCollapsed;
    },
  },
  methods: {
    toggleUiState: function() {
      this.$store.commit('toggleUiState');
    },
    toggleSettings: function() {
      this.$store.commit('toggleSettings');
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

#header-title {
  float: left;
  font-family: 'Lobster', cursive;
  padding: 16px;
  font-size: 32px;
  width: 168px;
  text-align: center;
  background-color: $primary-light;
  transition: all 0.5s ease-in-out;
  &.collapsed {
    width: 20px;
  }
}

#header-ui-ctrl {
  float: left;
}

#content {
  margin-left: 200px;
  &.expanded {
    margin-left: 52px;
  }
  transition: all 0.5s ease-in-out;
}
</style>