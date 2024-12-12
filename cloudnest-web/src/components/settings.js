import * as React from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button, Box, Switch, FormControlLabel } from '@mui/material';
import AppTheme from '../shared-theme/AppTheme';



export default function Settings({ open, onClose }) {
  const [formData, setFormData] = React.useState({
    username: '',
    email: '',
    password: '',
  });

  const [darkMode, setDarkMode] = React.useState(false);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleThemeToggle = () => {
    setDarkMode((prev) => !prev);
    console.log(`Dark Mode: ${!darkMode}`);
    localStorage.setItem('theme', !darkMode ? 'dark' : 'light');
  };

  const handleSave = () => {
    console.log('Updated Settings:', { ...formData, darkMode });
    onClose();
  };

  return (
     <AppTheme {...props}>
       <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm"
   PaperProps={{
    sx: (theme) => ({
      backgroundColor: theme.palette.background.default, // Fetch from theme
    }),
  }}
 >
      <DialogTitle>Settings</DialogTitle>
      <DialogContent>
        <Box display="flex" flexDirection="column" gap={2}>
          <TextField
            label="Username"
            name="username"
            value={formData.username}
            onChange={handleInputChange}
            fullWidth
          />
          <TextField
            label="Email"
            name="email"
            value={formData.email}
            onChange={handleInputChange}
            type="email"
            fullWidth
          />
          <TextField
            label="Password"
            name="password"
            value={formData.password}
            onChange={handleInputChange}
            type="password"
            fullWidth
          />
          <FormControlLabel
            control={<Switch checked={darkMode} onChange={handleThemeToggle} />}
            label={darkMode ? 'Dark Mode' : 'Light Mode'}
          />
        </Box>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="secondary">
          Cancel
        </Button>
        <Button onClick={handleSave} variant="contained" color="primary">
          Save
        </Button>
      </DialogActions>
    </Dialog>
   
</AppTheme>
  );
}
