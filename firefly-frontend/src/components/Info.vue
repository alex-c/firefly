<template>
  <el-tooltip :content="text" :placement="placement">
    <span class="mdi mdi-information info-icon" />
  </el-tooltip>
</template>

<script>
export default {
  name: 'info',
  props: ['text', 'position'],
  data() {
    return {
      placement: this.position || 'top',
    };
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

.info-icon {
  font-size: 20px;
  margin: 4px;
  position: relative;
  top: 2px;
  color: var($--theme-primary);
}
</style>