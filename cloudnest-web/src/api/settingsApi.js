// src/api/auth.js
import apiClient from './client/apiClient'// Import the Axios client with interceptor
import { accountSettingsDto } from '../dtos/accountSettingsDto';

export const update = async (accountSettingsDto) => {
  try {
    const response = await apiClient.put('/User/update', accountSettingsDto);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Update failed');
  }
};

export const getSettings = async () => {
  try {
    const response = await apiClient.get('/User/settings');
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Fetching settings failed');
  }
};
