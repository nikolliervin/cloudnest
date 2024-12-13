// src/api/axiosInterceptor.js
import axios from 'axios';
import { API_BASE_URL } from '../config/appConfig'

const apiClient = axios.create({
  baseURL: API_BASE_URL,
});

apiClient.interceptors.request.use(
  (config) => {
    if (config.url.includes('/login') || config.url.includes('/register')) {
      return config;
    }

    const token = sessionStorage.getItem('token');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);


export default apiClient;
