import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'http://localhost:5283', // Backend URL'nizi buraya girin
});

apiClient.interceptors.request.use(
    (config) => {
        const token = sessionStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

export default apiClient;
