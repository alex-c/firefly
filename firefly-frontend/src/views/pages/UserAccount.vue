<template>
  <div id="view-user-account">
    <Breadcrumb :items="[{name: $t('general.account')}]" />

    <!-- User Profile -->
    <Box>
      <template #header>{{$t('user.profile')}}</template>
      <div class="profile-columns">
        <div>
          <div class="row">{{$t('user.email')}}:</div>
          <div class="row">{{$t('user.name')}}:</div>
        </div>
        <div>
          <div class="row">{{email}}</div>
          <div class="row">{{name}}</div>
        </div>
      </div>
    </Box>

    <!-- Change Password -->
    <Box>
      <template #header>{{$t('user.changePassword')}}</template>
      <el-form
        :model="changePasswordForm"
        :rules="validationRules"
        ref="changePasswordForm"
        label-position="left"
        label-width="150px"
        size="mini"
      >
        <el-form-item prop="oldPassword" :label="$t('user.oldPassword')">
          <el-input
            show-password
            :placeholder="$t('user.oldPassword')"
            v-model="changePasswordForm.oldPassword"
          ></el-input>
        </el-form-item>
        <el-form-item prop="newPassword" :label="$t('user.newPassword')">
          <el-input
            show-password
            :placeholder="$t('user.newPassword')"
            v-model="changePasswordForm.newPassword"
          ></el-input>
        </el-form-item>
        <el-form-item prop="newPasswordRepeat" :label="$t('user.repeat')">
          <el-input
            show-password
            :placeholder="$t('user.newPassword')"
            v-model="changePasswordForm.newPasswordRepeat"
          ></el-input>
        </el-form-item>
      </el-form>
      <div class="row">
        <el-button
          type="primary"
          @click="changePassword"
          icon="el-icon-check"
          size="mini"
          class="right"
        >{{$t('general.save')}}</el-button>
      </div>
    </Box>
  </div>
</template>

<script>
import ApiErrorHandlingMixin from '@/mixins/ApiErrorHandlingMixin.js';
import Breadcrumb from '@/components/Breadcrumb.vue';
import Box from '@/components/Box.vue';

export default {
  name: 'user-account',
  mixins: [ApiErrorHandlingMixin],
  components: { Breadcrumb, Box },
  data() {
    return {
      email: this.$store.state.email,
      name: this.$store.state.name,
      changePasswordForm: {
        oldPassword: '',
        newPassword: '',
        newPasswordRepeat: '',
      },
    };
  },
  computed: {
    validationRules() {
      return {
        oldPassword: { required: true, message: this.$t('user.validation.oldPassword'), trigger: 'blur' },
        newPassword: { required: true, message: this.$t('user.validation.newPassword'), trigger: 'blur' },
        newPasswordRepeat: [
          { required: true, message: this.$t('user.validation.newPasswordRepeat'), trigger: 'blur' },
          { validator: this.validatePasswordRepeat, trigger: 'blur' },
        ],
      };
    },
  },
  methods: {
    validatePasswordRepeat: function(_, value, callback) {
      if (value !== this.changePasswordForm.newPassword) {
        callback(new Error(this.$t('user.validation.passwordMatch')));
      } else {
        callback();
      }
    },
    changePassword: function() {
      this.error = null;
      this.passwordChanged = false;
      this.$refs['changePasswordForm'].validate(valid => {
        if (valid) {
          this.$api
            .changePassword(this.changePasswordForm.oldPassword, this.changePasswordForm.newPassword)
            .then(response => {
              this.passwordChanged = true;
              this.$refs['changePasswordForm'].resetFields();
            })
            .catch(error => {
              if (error.status === 400) {
                this.error = 'account.badPassword';
              } else {
                this.handleApiError(error);
              }
            });
        }
      });
    },
  },
};
</script>

<style lang="scss" scoped>
#view-user-account {
  padding: 0px 16px;
  & > div {
    margin-top: 16px;
  }
}

.profile-columns {
  overflow: auto;
  & > div {
    float: left;
    &:first-child {
      margin-right: 8px;
    }
  }
}
</style>