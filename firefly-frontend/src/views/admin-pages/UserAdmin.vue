<template>
  <div id="view-user-admin">
    <Breadcrumb :name="$t('general.users')" :items="[{name: $t('general.admin')}]" />
    <Box :title="$t('users.title')">
      <!-- Header -->
      <template #header-buttons>
        <div class="right">
          <router-link :to="{path: '/admin/users/create'}">
            <el-button
              type="primary"
              theme="dark"
              size="mini"
              icon="el-icon-plus"
            >{{$t('users.create')}}</el-button>
          </router-link>
        </div>
      </template>

      <!-- Filters -->
      <div class="row" id="search-bar">
        <el-input
          :placeholder="$t('users.filter')"
          prefix-icon="el-icon-search"
          v-model="search"
          size="mini"
          clearable
          @change="setSearch"
        ></el-input>
      </div>

      <!-- User table -->
      <div class="row">
        <el-table
          :data="users"
          size="mini"
          :empty-text="$t('general.noData')"
          highlight-current-row
          @current-change="selectUser"
          ref="userTable"
          row-key="id"
        >
          <el-table-column prop="id" :label="$t('general.id')"></el-table-column>
          <el-table-column prop="name" :label="$t('general.name')"></el-table-column>
          <el-table-column prop="isAdmin" :label="$t('users.role')" :formatter="formatRole"></el-table-column>
        </el-table>
      </div>

      <!-- Pagination & user options -->
      <div class="row">
        <div class="left">
          <el-pagination
            background
            layout="prev, pager, next"
            :total="totalUsers"
            :page-size="query.usersPerPage"
            :current-page.sync="query.page"
            @current-change="changePage"
          ></el-pagination>
        </div>
        <div class="right">
          <router-link :to="{path: '/'}">
            <el-button
              icon="el-icon-edit"
              type="primary"
              size="mini"
              :disabled="selected.id === null"
            >{{$t('general.edit')}}</el-button>
          </router-link>
        </div>
      </div>
    </Box>

    <!-- Password reset links -->
    <Box :title="$t('users.passwordResetLink.titlePlural')" toggleable toggled>
      <div class="row">
        <el-table
          :data="resetLinks"
          size="mini"
          :empty-text="$t('general.noData')"
          highlight-current-row
          @current-change="selectUser"
          ref="userTable"
          row-key="id"
        >
          <el-table-column prop="id" :label="$t('general.id')"></el-table-column>
          <el-table-column prop="user" :label="$t('general.user')"></el-table-column>
          <el-table-column
            prop="expirationTimestamp"
            :label="$t('users.passwordResetLink.expires')"
            :formatter="formatRole"
          ></el-table-column>
        </el-table>
      </div>
    </Box>
  </div>
</template>

<script>
import ApiErrorHandlingMixin from '@/mixins/ApiErrorHandlingMixin.js';
import Breadcrumb from '@/components/Breadcrumb.vue';
import Box from '@/components/Box.vue';

export default {
  name: 'user-admin',
  mixins: [ApiErrorHandlingMixin],
  components: { Breadcrumb, Box },
  data() {
    return {
      search: '',
      query: {
        page: 1,
        usersPerPage: 10,
        search: '',
      },
      users: [],
      totalUsers: 0,
      selected: {
        id: null,
      },
      resetLinks: [],
    };
  },
  methods: {
    getUsers: function() {
      this.resetSelectedUser();
      this.$api.users
        .getUsers(this.query.page, this.query.usersPerPage, this.query.search, this.query.showDisabledUsers)
        .then(response => {
          this.users = response.body.data;
          this.totalUsers = response.body.totalElements;
        })
        .catch(this.handleApiError);
    },
    changePage: function(page) {
      this.query.page = page;
      this.getUsers();
    },
    setSearch: function(value) {
      this.query.search = value;
      this.query.page = 1;
      this.getUsers();
    },
    selectUser: function(user) {
      this.selected = {
        id: user.id,
        name: user.name,
        role: user.role,
        disabled: user.disabled,
      };
    },
    resetSelectedUser: function() {
      this.$refs['userTable'].setCurrentRow(1);
      this.selected.id = null;
      this.selected.name = null;
      this.selected.role = null;
      this.selected.disabled = null;
    },
    formatRole: function(user) {
      return user.isAdmin ? this.$t('users.roles.admin') : this.$t('users.roles.none');
    },
  },
  mounted() {
    this.getUsers();
  },
};
</script>

<style lang="scss" scoped>
#view-user-admin {
  max-width: 800px;
  margin: auto;
  padding: 0px 16px;
  & > div {
    margin-top: 16px;
  }
}
</style>