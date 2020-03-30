<template>
  <div class="menu-button" :class="{active: currentRoute == route}" @click="navigate">
    <div>
      <span class="menu-button-icon mdi" :class="this.icon" />
      <div>{{$t(`general.${name}`)}}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'menu-button',
  props: ['name', 'routePrefix', 'currentRoute', 'icon'],
  data() {
    return {
      route: this.routePrefix ? `${this.routePrefix}/${this.name}` : `/${this.name}`,
    };
  },
  methods: {
    navigate: function() {
      this.$router.push({ path: this.route });
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

.menu-button {
  & > div {
    margin: 16px;
    height: 20px;
    overflow: hidden;
    & > div {
      float: left;
      max-width: 20px;
    }
  }
  overflow: auto;
  &.active {
    background-color: var($--theme-background);
    border-left: 5px solid $primary;
  }
  &:hover {
    background-color: var($--theme-background);
    border-left: 5px solid $primary-light;
    cursor: pointer;
  }
}

.menu-button-icon {
  float: left;
  margin-right: 8px;
}
</style>