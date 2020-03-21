<template>
  <div class="breadcrumb">
    <router-link to="/">Home</router-link>
    <span v-for="(item, i) in items" :key="i">
      <span class="breadcrumb-separator">/</span>
      <router-link v-if="item.path !== undefined" :to="item.path">{{item.name}}</router-link>
      <span v-else>{{item.name}}</span>
    </span>
  </div>
</template>

<script>
export default {
  name: 'breadcrumb',
  props: ['items'],
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

.breadcrumb {
  text-align: left;
  font-size: 12px;
  a:any-link {
    color: $secondary-lighter;
  }
  a:hover {
    text-decoration: underline;
  }
  a:active {
    color: var($--theme-text);
  }
}

.breadcrumb-separator {
  margin: 0px 8px;
}
</style>