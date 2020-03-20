const Api = {
  install(Vue) {
    Vue.prototype.$api = {
      testNamespace: {
        testCall: () => {
          console.log('Test call!');
        },
      },
    };
  },
};

export default Api;
