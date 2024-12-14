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

export const getStorageData = async () => {
  try {
    const response = await apiClient.get('/User/getStorageData');
    
    if (response.data.success) {
      return response.data.data;
    } else {
      throw new Error(response.data.message || 'Error fetching storage data');
    }
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Fetching storage data failed');
  }
};
