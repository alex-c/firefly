<template>
  <div id="view-user-creation">
    <Breadcrumb
      :name="$t('users.create')"
      :items="[{name: $t('general.admin')}, {name: $t('general.users'), path: '/admin/users'}]"
      back-to="/admin/users"
    />
    <Box :title="$t('users.create')">
      <!-- Save button -->
      <div class="row">
        <div class="right">
          <el-button icon="el-icon-check" type="primary" size="mini">{{$t('general.save')}}</el-button>
        </div>
      </div>
    </Box>
  </div>
</template>

<script>
import Breadcrumb from '@/components/Breadcrumb.vue';
import Box from '@/components/Box.vue';

export default {
  name: 'user-creation',
  components: { Breadcrumb, Box },
};
</script>

<style lang="scss" scoped>
#view-user-creation {
  max-width: 800px;
  margin: auto;
  padding: 0px 16px;
  & > div {
    margin-top: 16px;
  }
}
</style>