import * as React from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button, Box } from '@mui/material';
import AppTheme from '../shared-theme/AppTheme';

export default function Settings({ open, onClose }) {
  const [formData, setFormData] = React.useState({
    username: '',
    email: '',
    password: '',
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  return (
    <AppTheme>
      <Dialog
        open={open}
        onClose={onClose}
        fullWidth
        maxWidth="sm" // Set modal to 'sm' for a more compact size
        PaperProps={{
          sx: (theme) => ({
            backgroundColor: theme.palette.background.paper, // Lighter background for a clean look
            padding: theme.spacing(3), // Subtle padding
            borderRadius: '10px', // Rounded corners for a modern feel
          }),
        }}
      >
        <DialogTitle sx={{ textAlign: 'center', fontWeight: 500 }}>Account Settings</DialogTitle>
        <DialogContent
          sx={{
            display: 'flex',
            flexDirection: 'column',
            gap: 2,
            padding: 0, // Remove extra padding for a more minimal layout
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
                  height: 45, // Make input height a bit more spacious
                },
                '& .MuiInputLabel-root': {
                  top: -6, // Keep label visibility intact
                },
                '& .MuiOutlinedInput-root': {
                  borderRadius: '5px', // Rounded borders for inputs
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
          </Box>
        </DialogContent>
    <DialogActions sx={{ padding: '10px 24px 16px', gap: 2 }}>
  <Button onClick={onClose} color="secondary" sx={{ textTransform: 'none' }}>
    Cancel
  </Button>
  <Button variant="contained" color="primary" sx={{ textTransform: 'none' }}>
    Save
  </Button>
</DialogActions>

      </Dialog>
    </AppTheme>
  );
}
