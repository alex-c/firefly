<template>
  <div id="view-login">
    <Header>
      <div id="header-title">Firefly</div>
    </Header>
    <div id="login-content">
      <Box>
        <template v-slot:header>{{$t('login.welcome')}}</template>
        <div>
          <el-form>
            <el-form-item>
              <el-input v-model="loginForm.name" :placeholder="$t('user.name')"></el-input>
            </el-form-item>
            <el-form-item>
              <el-input
                v-model="loginForm.password"
                :placeholder="$t('user.password')"
                show-password
              ></el-input>
            </el-form-item>
            <div id="login-button-container">
              <el-button
                type="primary"
                icon="el-icon-* mdi mdi-login"
                @click="login"
              >{{$t('login.login')}}</el-button>
            </div>
          </el-form>
        </div>
      </Box>
      <div id="footer-text">
        <a href="https://www.github.com/alex-c/firefly">
          <span class="mdi mdi-github"></span>
          {{$t('login.footer')}}
        </a>
      </div>
    </div>
    <Settings />
  </div>
</template>

<script>
import Header from '@/components/Header.vue';
import Settings from '@/views/Settings.vue';
import Box from '@/components/Box.vue';

export default {
  name: 'login',
  components: { Header, Settings, Box },
  data() {
    return {
      loginForm: {
        name: '',
        password: '',
      },
    };
  },
  computed: {
    uiCollapsed() {
      return this.$store.state.ui.uiCollapsed;
    },
  },
  methods: {
    login: function() {
      this.$api.testNamespace.testCall();
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