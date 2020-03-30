<template>
  <div class="box">
    <div class="box-header" v-if="hasHeader">
      <div class="box-title left">{{title}}</div>
      <div class="right">
        <div
          class="box-toggle right"
          v-if="toggleable !== undefined"
          @click="isToggled = !isToggled"
        >
          <span class="mdi mdi-chevron-down-circle" v-if="isToggled" />
          <span class="mdi mdi-chevron-up-circle" v-else />
        </div>
        <div class="header-buttons right">
          <slot name="header-buttons" />
        </div>
      </div>
    </div>
    <div class="box-content" v-show="!isToggled">
      <slot />
    </div>
  </div>
</template>

<script>
export default {
  name: 'box',
  props: ['title', 'toggleable', 'toggled'],
  data() {
    return {
      isToggled: this.toggled !== undefined,
    };
  },
  computed: {
    hasHeader() {
      return this.title !== undefined || this.$slots['header-buttons'] !== undefined;
    },
  },
};
</script>

<style lang="scss">
@import '@/style/colors.scss';

.box {
  background-color: var($--theme-foreground);
  border-radius: 2px;
  text-align: left;
}

.box-header {
  padding: 0px 16px;
  height: 48px;
  border-bottom: 1px solid var($--theme-primary);
  .box-title {
    padding: 16px 0px;
    font-weight: bold;
  }
  .mdi {
    margin-left: 16px;
    font-size: 18px;
    position: relative;
    top: -2px;
  }
  .header-buttons {
    padding: 10px 0px;
    button {
      margin-left: 16px;
    }
  }
  .box-toggle {
    height: 16px;
    padding: 16px 0px;
    &:hover {
      cursor: pointer;
    }
  }
}

.box-content {
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