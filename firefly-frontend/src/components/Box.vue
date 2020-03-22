<template>
  <div class="box">
    <div class="box-header" v-if="hasHeader">
      <slot name="header" />
    </div>
    <div class="box-content">
      <slot />
    </div>
  </div>
</template>

<script>
export default {
  name: 'box',
  computed: {
    hasHeader() {
      return this.$slots.header !== undefined;
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

.box {
  background-color: var($--theme-foreground);
  border-radius: 2px;
  text-align: left;
}

.box-header {
  padding: 16px;
  font-weight: bold;
  .mdi {
    margin-right: 6px;
    font-size: 18px;
    float: left;
    position: relative;
    top: -2px;
  }
}

.box-content {
  border-top: 1px solid var($--theme-primary);
  padding: 16px;
  .row {
    margin-top: 16px;
    overflow: auto;
    &:first-child {
      margin-top: 0px;
    }
  }
  .row-condensed {
    margin-top: 8px;
    overflow: auto;
    &:first-child {
      margin-top: 0px;
    }
  }
  .row-nopad {
    overflow: auto;
  }
}
</style>