// apiEndpoints.js
const endpoints = {
    auth: {
        login: '/api/login',
        logout: '/api/logout'
    },
    bankcards: {
        getAll: '/api/bankcards/all',
        getById: (id) => `/api/bankcards/byId/${id}`,
        create: '/api/bankcards/create',
        updateById: (id) => `/api/bankcards/updateById/${id}`,
        deleteById: (id) => `/api/bankcards/deleteById/${id}`,
    },
    creditcards: {
        getAll: '/api/creditcards/all',
        getById: (id) => `/api/creditcards/byId/${id}`,
        create: '/api/creditcards/create',
        updateById: (id) => `/api/creditcards/updateById/${id}`,
        deleteById: (id) => `/api/creditcards/deleteById/${id}`,
    },
    user: {
        getAll: '/api/user/all',
        getById: (id) => `/api/user/byId/${id}`,
        create: '/api/user/create',
        update: (id) => `/api/user/update/${id}`,
    },
    rolPermissions: {
        getById: (id) => `/api/rolePermissions/byId/${id}`
    }
};

export default endpoints;
