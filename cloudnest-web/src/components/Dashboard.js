import React from 'react';
import { CssBaseline, Box, Stack } from '@mui/material';
import { alpha, createTheme, ThemeProvider } from '@mui/material/styles';
import AppNavbar from './components/AppNavbar';
import SideMenu from './components/SideMenu';
import FileManager from './fileManager';

// Create a default dark theme
const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

export default function Dashboard() {
  return (
    <ThemeProvider theme={darkTheme}>
      <CssBaseline enableColorScheme />
      <Box sx={{ display: 'flex', height: '100vh' }}>
        <SideMenu />
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'column',
            flexGrow: 1,
            overflow: 'hidden', // Prevent overflow in the main content area
          }}
        >
          <AppNavbar />
       
            <FileManager />
        
        </Box>
      </Box>
    </ThemeProvider>
  );
}
