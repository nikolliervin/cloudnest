import React, { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button, Box } from '@mui/material';
import AppTheme from '../shared-theme/AppTheme';
import { update } from '../api/settingsApi'

export default function Settings({ open, onClose }) {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
    oldPassword: '',  // Added oldPassword to the form data
  });

  useEffect(() => {
    if (open) {
      const userData = JSON.parse(sessionStorage.getItem('userData'));
      if (userData) {
        setFormData({
          username: userData.uniqueName || '',
          email: userData.email || '',
          password: '',
          oldPassword: '',  // Reset oldPassword when opening the dialog
        });
      } else {
        console.error('User data not found in sessionStorage');
      }
    }
  }, [open]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSave = async () => {
    const accountSettingsDto = {
      username: formData.username,
      email: formData.email,
      password: formData.password,
      oldPassword: formData.oldPassword, // Include oldPassword in the request
    };

    try {
      const data = await update(accountSettingsDto);

      if (data.success) {
        toast.success('Account settings updated successfully!');
        console.log('Account settings saved successfully:', data);
        sessionStorage.setItem('userData', JSON.stringify(data.data));
        onClose();
      } else {
        toast.error(data.message || 'An error occurred while updating account settings.');
        console.error('Error saving account settings:', data.message);
      }
    } catch (error) {
      toast.error(error.message || 'Error saving account settings. Please try again.');
      console.error('Error saving account settings:', error);
    }
  };

  return (
    <AppTheme>
      <Dialog
        open={open}
        onClose={onClose}
        fullWidth
        maxWidth="sm"
        PaperProps={{
          sx: (theme) => ({
            backgroundColor: theme.palette.background.paper,
            padding: theme.spacing(3),
            borderRadius: '10px',
          }),
        }}
      >
        <DialogTitle sx={{ textAlign: 'center', fontWeight: 500 }}>Account Settings</DialogTitle>
        <DialogContent
          sx={{
            display: 'flex',
            flexDirection: 'column',
            gap: 2,
            padding: 0,
          }}
        >
          <Box display="flex" flexDirection="column" gap={2}>
            <TextField
              label="Username"
              name="username"
              value={formData.username}
              onChange={handleInputChange}
              placeholder="Enter your username"
              fullWidth
              sx={{
                '& .MuiInputBase-root': {
                  height: 45,
                },
                '& .MuiInputLabel-root': {
                  top: -6,
                },
                '& .MuiOutlinedInput-root': {
                  borderRadius: '5px',
                },
              }}
            />
            <TextField
              label="Email"
              name="email"
              value={formData.email}
              onChange={handleInputChange}
              type="email"
              placeholder="Enter your email"
              fullWidth
              sx={{
                '& .MuiInputBase-root': {
                  height: 45,
                },
                '& .MuiInputLabel-root': {
                  top: -6,
                },
                '& .MuiOutlinedInput-root': {
                  borderRadius: '5px',
                },
              }}
            />
            <TextField
              label="Password"
              name="password"
              value={formData.password}
              onChange={handleInputChange}
              type="password"
              placeholder="Enter your password"
              fullWidth
              sx={{
                '& .MuiInputBase-root': {
                  height: 45,
                },
                '& .MuiInputLabel-root': {
                  top: -6,
                },
                '& .MuiOutlinedInput-root': {
                  borderRadius: '5px',
                },
              }}
            />
            <TextField
              label="Old Password"  // New field for old password
              name="oldPassword"
              value={formData.oldPassword}
              onChange={handleInputChange}
              type="password"
              placeholder="Enter your old password"
              fullWidth
              sx={{
                '& .MuiInputBase-root': {
                  height: 45,
                },
                '& .MuiInputLabel-root': {
                  top: -6,
                },
                '& .MuiOutlinedInput-root': {
                  borderRadius: '5px',
                },
              }}
            />
          </Box>
        </DialogContent>
        <DialogActions sx={{ padding: '10px 24px 16px', gap: 2 }}>
          <Button onClick={onClose} color="secondary" sx={{ textTransform: 'none' }}>
            Cancel
          </Button>
          <Button
            variant="contained"
            color="primary"
            sx={{ textTransform: 'none' }}
            onClick={handleSave}
          >
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </AppTheme>
  );
}
