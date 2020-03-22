<template>
  <div class="alert is-dark" :class="['el-alert--' + type]">
    <span class="icon-left mdi" v-if="showIcon" :class="icons[type]" />
    <span class="icon-right mdi mdi-close" v-if="closeable" @click="close" />
    <div class="alert-title" v-if="title !== undefined">{{title}}</div>
    <slot />
  </div>
</template>

<script>
export default {
  name: 'alert',
  props: {
    type: {
      // Can be any of `info`, `success`, `warning` or `error`
      type: String,
      default: 'info',
    },
    title: String,
    showIcon: Boolean,
    closeable: Boolean,
  },
  data() {
    return {
      icons: {
        info: 'mdi-information',
        success: 'mdi-check',
        warning: 'mdi-alert',
        error: 'mdi-alert-circle',
      },
    };
  },
  methods: {
    close: function() {
      this.$emit('close');
    },
  },
};
</script>

<style lang="scss" scoped>
.alert {
  border-radius: 2px;
  padding: 8px;
  .mdi {
    font-size: 18px;
    position: relative;
    top: -4px;
  }
  .icon-left {
    margin-right: 6px;
    float: left;
  }
  .icon-right {
    float: right;
    &:hover {
      cursor: pointer;
    }
  }
}

.alert-title {
  font-weight: bold;
  margin-bottom: 8px;
}
</style>