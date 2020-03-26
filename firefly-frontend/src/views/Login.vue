<template>
  <div id="view-login">
    <!-- Page header -->
    <Header>
      <div id="header-title">Firefly</div>
    </Header>

    <!-- Login page -->
    <div id="login-content">
      <Box :title="$t('login.welcome')">
        <!-- Feedback -->
        <el-collapse-transition>
          <Alert
            type="error"
            closeable
            show-icon
            v-if="badLogin"
            v-on:close="badLogin = false"
          >{{$t('login.badLogin')}}</Alert>
        </el-collapse-transition>

        <!-- Login form -->
        <div class="row">
          <el-form :model="loginForm" :rules="validationRules" ref="loginForm">
            <el-form-item prop="id">
              <el-input
                v-model="loginForm.id"
                :placeholder="$t('user.id')"
                prefix-icon="el-icon-user-solid"
                @keyup.enter.native="login"
                autofocus
              ></el-input>
            </el-form-item>
            <el-form-item prop="password">
              <el-input
                v-model="loginForm.password"
                :placeholder="$t('user.password')"
                prefix-icon="el-icon-lock"
                @keyup.enter.native="login"
                show-password
              ></el-input>
            </el-form-item>
            <div id="login-button-container">
              <el-button
                type="primary"
                icon="el-icon-* mdi mdi-login"
                @click="login"
              >{{ $t('login.login') }}</el-button>
            </div>
          </el-form>
        </div>
      </Box>

      <!-- Page footer -->
      <div id="footer-text">
        <a href="https://www.github.com/alex-c/firefly">
          <span class="mdi mdi-github"></span>
          {{ $t('login.footer') }}
        </a>
      </div>
    </div>
    <Settings />
  </div>
</template>

<script>
import ApiErrorHandlingMixin from '@/mixins/ApiErrorHandlingMixin.js';
import Header from '@/components/Header.vue';
import Settings from '@/views/Settings.vue';
import Box from '@/components/Box.vue';
import Alert from '@/components/Alert.vue';

export default {
  name: 'login',
  mixins: [ApiErrorHandlingMixin],
  components: { Header, Settings, Box, Alert },
  data() {
    return {
      loginForm: {
        id: '',
        password: '',
      },
      badLogin: false,
    };
  },
  computed: {
    uiCollapsed() {
      return this.$store.state.ui.uiCollapsed;
    },
    validationRules() {
      return {
        id: { required: true, message: this.$t('login.validation.id'), trigger: 'blur' },
        password: { required: true, message: this.$t('login.validation.password'), trigger: 'blur' },
      };
    },
  },
  methods: {
    login: function() {
      this.badLogin = false;
      this.$refs['loginForm'].validate(valid => {
        if (valid) {
          this.$api
            .login(this.loginForm.id, this.loginForm.password)
            .then(response => {
              const token = response.body.token;
              this.$store.commit('login', token);
              this.$router.push('/dashboard');
            })
            .catch(error => {
              if (error.status === 401) {
                this.badLogin = true;
              } else {
                this.handleApiError(error);
              }
            });
        } else {
          return false;
        }
      });
    },
  },
};
</script>

<style lang="scss" scoped>
@import '@/style/colors.scss';

#header-title {
  float: left;
  font-family: 'Lobster', cursive;
  padding: 8px 16px;
  font-size: 32px;
  width: 168px;
  text-align: center;
}

#login-content {
  width: 360px;
  padding: 32px;
  margin: auto;
  margin-top: 32px;
}

#login-button-container {
  text-align: center;
}

#footer-text {
  margin-top: 16px;
  font-size: 14px;
  a:any-link {
    color: $secondary-lighter;
  }
  a:hover {
    text-decoration: underline;
  }
  a:active {
    color: var($--theme-text);
  }
}
</style>
