
import apiClient from "./axiosInterceptor"; // Import the custom API client


export const createDirectory = (directoryData) => {
  return apiClient.post('/api/Directory/create', directoryData);
};


export const updateDirectory = (directoryData) => {
  return apiClient.put('/api/Directory/update', directoryData);
};

export const deleteDirectory = (directoryId) => {
  return apiClient.delete('/api/Directory/delete', {
    params: { id: directoryId },
  });
};


export const getDirectory = (directoryId) => {
  return apiClient.get('/api/Directory/directory', {
    params: { id: directoryId },
  });
};

export const getDirectories = () => {
  return apiClient.get('/api/Directory/directories');
};
