<template>
  <div id="view-not-found">
    <Box id="not-found-box">
      <template #header>
        <span class="mdi mdi-magnify" />
        <span>{{$t('notFound.title')}}</span>
      </template>
      <span>{{$t('notFound.message')}}</span>
    </Box>
  </div>
</template>

<script>
import Box from '@/components/Box.vue';

export default {
  name: 'not-found',
  components: { Box },
};
</script>

<style lang="scss" scoped>
#view-not-found {
  padding: 16px;
}

#not-found-box {
  width: 400px;
  margin: auto;
}
</style>