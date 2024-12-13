// src/api/auth.js
import axios from 'axios';
import { API_BASE_URL } from '../api/config/appConfig'
import { registerDto } from '../dtos/registerDto';

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

export const registerUser = async (registerDto) => {
  let response;
  try {
    console.log(registerDto);
    response = await axios.post(API_BASE_URL + '/auth/register',
      registerDto
    );
    return response.data;
  } catch (error) {
    console.log(response);
    console.log(error);
    throw new Error(error.response?.data?.message || 'Registration failed');
  }
};

export const logoutUser = async () => {
  try {
    const token = sessionStorage.getItem('token');

    if (!token) {
      console.log('No token found, skipping logout');
      return;
    }
    await axios.post(
      `${API_BASE_URL}/auth/logout`,
      {},
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
  } catch (error) {
    console.error('Logout failed:', error);
  }
};

