export default {
  methods: {
    handleApiError: function(error) {
      switch (error.status) {
        case 401:
          this.$store.commit('logout');
          this.$router.push({ path: '/login' }, () => {});
          this.$message({
            message: this.$t('general.unauthorizedError'),
            type: 'error',
          });
          break;
        case 403:
          this.$router.push({ path: '/' }, () => {});
          this.$message({
            message: this.$t('general.forbiddenError'),
            type: 'error',
          });
          break;
        default:
          console.error(error);
          this.$message({
            message: this.$t('general.unexpecteError'),
            type: 'error',
          });
          break;
      }
    },
  },
};
