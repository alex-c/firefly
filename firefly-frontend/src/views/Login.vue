<template>
  <div id="view-login">
    <Header>
      <div id="header-title">Firefly</div>
    </Header>
    <div id="login-content">
      <Box>
        <template v-slot:header>{{ $t('login.welcome') }}</template>
        <div>
          <el-form :model="loginForm" :rules="validationRules" ref="loginForm">
            <el-form-item prop="email">
              <el-input v-model="loginForm.email" :placeholder="$t('user.email')" prefix-icon="el-icon-user-solid" @keyup.enter.native="login" autofocus></el-input>
            </el-form-item>
            <el-form-item prop="password">
              <el-input v-model="loginForm.password" :placeholder="$t('user.password')" prefix-icon="el-icon-lock" @keyup.enter.native="login" show-password></el-input>
            </el-form-item>
            <div id="login-button-container">
              <el-button type="primary" icon="el-icon-* mdi mdi-login" @click="login">{{ $t('login.login') }}</el-button>
            </div>
          </el-form>
        </div>
      </Box>
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

export default {
  name: 'login',
  mixins: [ApiErrorHandlingMixin],
  components: { Header, Settings, Box },
  data() {
    return {
      loginForm: {
        email: '',
        password: '',
      },
    };
  },
  computed: {
    uiCollapsed() {
      return this.$store.state.ui.uiCollapsed;
    },
    validationRules() {
      return {
        email: { required: true, message: this.$t('login.validation.email'), trigger: 'blur' },
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
            .login(this.loginForm.email, this.loginForm.password)
            .then(response => {
              const token = response.body.token;
              this.$store.commit('login', token);
              this.$router.push('/dashboard');
            })
            .catch(error => {
              if (error.status === 401) {
                this.badLogin = true; //TODO
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
  padding: 16px;
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
