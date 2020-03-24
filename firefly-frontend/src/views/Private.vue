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
      <template #header-right>
        <div id="user-menu-container">
          <el-dropdown
            class="navbar-right"
            trigger="click"
            placement="bottom"
            @command="userMenuAction"
          >
            <div id="user-menu">
              <span class="mdi mdi-account" />
            </div>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item
                icon="el-icon-user-solid"
                command="account"
              >{{$t('general.account')}}</el-dropdown-item>
              <el-dropdown-item
                icon="el-icon-* mdi mdi-logout"
                command="logout"
              >{{$t('general.logout')}}</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </div>
      </template>
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
  data() {
    return {
      userName: localStorage.getItem('name'),
    };
  },
  computed: {
    uiCollapsed() {
      return this.$store.state.ui.uiCollapsed;
    },
  },
  methods: {
    toggleUiState: function() {
      this.$store.commit('toggleUiState');
    },
    fitToScreen: function() {
      if (window.innerWidth <= 800) {
        this.$store.commit('collapseUi');
      } else {
        this.$store.commit('expandUi');
      }
    },
    toggleSettings: function() {
      this.$store.commit('toggleSettings');
    },
    userMenuAction: function(command) {
      switch (command) {
        case 'account':
          this.$router.push({ path: '/user' });
          break;
        case 'logout':
          this.$store.commit('logout');
          this.$router.push({ path: '/login' }, () => {});
          break;
        default:
          console.error(`Invalid user menu action '${command}' was triggered.`);
          break;
      }
    },
  },
  created: function() {
    window.addEventListener('resize', this.fitToScreen);
    this.fitToScreen();
  },
  destroyed() {
    window.removeEventListener('resize', this.fitToScreen);
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

#header-title {
  float: left;
  font-family: 'Lobster', cursive;
  padding: 8px 16px;
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

#user-menu-container {
  float: right;
}

#user-menu {
  color: var($--theme-primary-contrast);
  width: 32px;
  height: 32px;
  padding: 12px;
  font-size: 28px;
  &:hover {
    background-color: $primary-dark;
    color: $light;
    cursor: pointer;
  }
}

#content {
  margin-left: 200px;
  &.expanded {
    margin-left: 52px;
  }
  transition: all 0.5s ease-in-out;
}
</style>