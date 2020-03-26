<template>
  <div class="breadcrumb">
    <router-link to="/">Home</router-link>
    <span v-for="(item, i) in items" :key="i">
      <span class="breadcrumb-separator">/</span>
      <router-link v-if="item.path !== undefined" :to="item.path">{{item.name}}</router-link>
      <span v-else>{{item.name}}</span>
    </span>
    <span v-if="name !== undefined">
      <span class="breadcrumb-separator">/</span>
      <span class="breadcrumb-current-location">{{name}}</span>
    </span>
    <div class="breadcrumb-back right" v-if="backTo !== undefined">
      <router-link :to="{path: backTo}">
        <span class="mdi mdi-chevron-left" />
        {{$t('general.back')}}
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: 'breadcrumb',
  props: ['name', 'items', 'backTo'],
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

.breadcrumb {
  text-align: left;
  font-size: 12px;
  a:any-link {
    color: var($--theme-text);
  }
  a:hover {
    text-decoration: underline;
  }
  a:active {
    color: $primary;
  }
}

.breadcrumb-current-location {
  color: $primary-light;
}

.breadcrumb-separator {
  margin: 0px 8px;
}
</style>