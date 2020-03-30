<template>
  <div id="view-user-creation">
    <Breadcrumb
      :name="$t('users.create')"
      :items="[{name: $t('general.admin')}, {name: $t('general.users'), path: '/admin/users'}]"
      back-to="/admin/users"
    />
    <Box :title="$t('users.create')">
      <!-- Feedback -->
      <transition name="el-fade-in-linear">
        <Alert
          type="error"
          v-if="error !== null"
          v-on:close="error = null"
          closeable
          show-icon
        >{{error}}</Alert>
      </transition>

      <!-- User creation form -->
      <div class="row">
        <el-form
          :model="form"
          :rules="validationRules"
          ref="form"
          label-position="left"
          label-width="120px"
          size="mini"
        >
          <!-- User name -->
          <el-form-item prop="name" :label="$t('general.name')">
            <el-input :placeholder="$t('general.name')" v-model="form.name"></el-input>
          </el-form-item>

          <!-- User ID -->
          <el-form-item prop="id" :label="$t('users.loginId')">
            <el-input :placeholder="$t('general.id')" v-model="form.id">
              <el-button
                slot="append"
                icon="el-icon-refresh-right"
                type="info"
                @click="generateId"
                :title="$t('users.generateId')"
              ></el-button>
            </el-input>
          </el-form-item>

          <!-- User password -->
          <el-form-item prop="password" :label="$t('user.password')">
            <el-input :placeholder="$t('user.password')" v-model="form.password">
              <el-button
                slot="append"
                icon="el-icon-refresh-right"
                class="input-button"
                @click="generatePassword"
                :title="$t('users.generatePassword')"
              ></el-button>
            </el-input>
          </el-form-item>

          <!-- Administrator role -->
          <el-form-item prop="isAdmin" :label="$t('users.roles.admin')">
            <el-switch v-model="form.isAdmin" />
          </el-form-item>
        </el-form>
      </div>

      <!-- Password reset link & save button -->
      <div class="row">
        <div class="left">
          {{$t('users.passwordResetLink.generate')}}
          <el-switch v-model="form.generatePasswordResetLink" />
          <Info :text="$t('users.passwordResetLink.info')" />
        </div>
        <div class="right">
          <el-button
            @click="createUser"
            icon="el-icon-check"
            type="primary"
            size="mini"
          >{{$t('general.save')}}</el-button>
        </div>
      </div>
    </Box>
  </div>
</template>

<script>
import ApiErrorHandlingMixin from '@/mixins/ApiErrorHandlingMixin.js';
import Breadcrumb from '@/components/Breadcrumb.vue';
import Box from '@/components/Box.vue';
import Info from '@/components/Info.vue';
import Alert from '@/components/Alert.vue';

export default {
  name: 'user-creation',
  mixins: [ApiErrorHandlingMixin],
  components: { Breadcrumb, Box, Info, Alert },
  data() {
    return {
      error: null,
      form: {
        name: '',
        id: '',
        password: '',
        isAdmin: false,
        generatePasswordResetLink: false,
      },
    };
  },
  computed: {
    validationRules() {
      return {
        name: { required: true, message: this.$t('users.validation.name'), trigger: 'blur' },
        id: { required: true, message: this.$t('users.validation.id'), trigger: ['change', 'blur'] },
        password: { required: true, message: this.$t('users.validation.password'), trigger: ['change', 'blur'] },
        isAdmin: { required: true },
      };
    },
  },
  methods: {
    generateId: function() {
      this.form.id = this.form.name
        .trim()
        .replace(/ /g, '_')
        .toLowerCase();
    },
    generatePassword: function() {
      this.form.password = Math.random()
        .toString(36)
        .substr(2, 8);
    },
    createUser: function() {
      this.$refs['form'].validate(valid => {
        if (valid) {
          this.$api.users
            .createUser(this.form.id, this.form.name, this.form.password, this.form.isAdmin)
            .then(result => {
              if (this.form.generatePasswordResetLink) {
                this.$alert(this.$t('users.passwordResetLink.message', { link: 'TODO' }), this.$t('users.passwordResetLink.title'), {
                  confirmButtonText: this.$t('general.ok'),
                });
              } else {
                this.$router.push({ path: '/admin/users' });
              }
            })
            .catch(error => {
              if (error.status === 409) {
                this.error = this.$t('users.idTaken', { id: this.form.id }); // TODO
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
#view-user-creation {
  max-width: 800px;
  margin: auto;
  padding: 0px 16px;
  & > div {
    margin-top: 16px;
  }
}
</style>