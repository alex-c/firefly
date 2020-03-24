<template>
  <div id="view-user-admin">
    <Breadcrumb :items="[{name: 'Administration'}, {name: 'Users'}]" />
    <Box>
      <template #header>Users</template>
      <span>test</span>
    </Box>
  </div>
</template>

<script>
import Breadcrumb from '@/components/Breadcrumb.vue';
import Box from '@/components/Box.vue';

export default {
  name: 'user-admin',
  components: { Breadcrumb, Box },
};
</script>

<style lang="scss" scoped>
#view-user-admin {
  padding: 0px 16px;
  & > div {
    margin-top: 16px;
  }
}
</style>