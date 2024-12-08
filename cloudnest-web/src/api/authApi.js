// src/api/auth.js
import axios from 'axios';
import { API_BASE_URL } from '../config/appConfig';

export const loginUser = async (username, password) => {
  try {
    const response = await axios.post(API_BASE_URL + '/auth/login', {
      username,
      password,
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Login failed');
  }
};

export const registerUser = async (email, password) => {
  try {
    const response = await axios.post('/api/register', {
      email,
      password,
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Registration failed');
  }
};

export const logoutUser = async () => {
  try {
    await axios.post('/api/logout');
  } catch (error) {
    console.error('Logout failed:', error);
  }
};
