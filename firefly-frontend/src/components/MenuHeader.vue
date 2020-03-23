<template>
  <div class="menu-header">
    <div>
      <span class="menu-header-icon mdi" :class="this.icon" v-show="collapsed" />
      <div>{{$t(`general.${name}`)}}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'menu-button',
  props: ['name', 'icon', 'collapsed'],
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

.menu-header {
  background-color: $secondary-lighter;
  color: var($--theme-primary-contrast);
  & > div {
    margin: 8px 16px;
    height: 20px;
    overflow: hidden;
    & > div {
      float: left;
      max-width: 20px;
    }
  }
  overflow: auto;
}

.menu-header-icon {
  float: left;
  margin-right: 8px;
}
</style>