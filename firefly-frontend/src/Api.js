function processResponse(response) {
  return new Promise((resolve, reject) => {
    if (response.status === 204) {
      resolve({ status: response.status });
    } else if (response.status === 401 || response.status === 403 || response.status == 500) {
      reject({ status: response.status });
    } else {
      let handler;
      response.status < 400 ? (handler = resolve) : (handler = reject);
      response.json().then(json => handler({ status: response.status, body: json }));
    }
  });
}

function catchNetworkError(response) {
  return new Promise((_, reject) => {
    reject({ status: null, message: 'general.networkError' });
  });
}

function getAuthorizationHeader() {
  return 'Bearer ' + localStorage.getItem('token');
}

const Api = {
  install(Vue) {
    Vue.prototype.$api = {
      login: (id, password) => {
        return fetch('http://localhost:5000/api/auth', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ id, password }),
        })
          .catch(catchNetworkError)
          .then(processResponse);
      },
      changePassword: (oldPassword, newPassword) => {
        return fetch('http://localhost:5000/api/self/password', {
          method: 'POST',
          withCredentials: true,
          credentials: 'include',
          headers: { 'Content-Type': 'application/json', Authorization: getAuthorizationHeader() },
          body: JSON.stringify({ old: oldPassword, new: newPassword }),
        })
          .catch(catchNetworkError)
          .then(processResponse);
      },
      users: {
        getUsers: (page, elementsPerPage, search) => {
          return fetch(`http://localhost:5000/api/users?page=${page}&elementsPerPage=${elementsPerPage}&search=${search}`, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
          })
            .catch(catchNetworkError)
            .then(processResponse);
        },
      },
    };
  },
};

export default Api;
