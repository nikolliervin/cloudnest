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
      <Box sx={{ display: 'flex' }}>
        <SideMenu />
        <AppNavbar />
        <Box
          component="main"
          sx={(theme) => ({
            flexGrow: 1,
            backgroundColor: theme.vars
              ? `rgba(${theme.vars.palette.background.defaultChannel} / 1)`
              : alpha(theme.palette.background.default, 1),
            overflow: 'auto',
          })}
        >
          <Stack
            spacing={2}
            sx={{
              alignItems: 'center',
              mx: 3,
              pb: 5,
              mt: { xs: 8, md: 0 },
            }}
          ></Stack>
        </Box>
      </Box>
      <FileManager />
    </ThemeProvider>
  );
}
