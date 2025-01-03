import * as React from 'react';
import { styled } from '@mui/material/styles';
import Divider, { dividerClasses } from '@mui/material/Divider';
import Menu from '@mui/material/Menu';
import MuiMenuItem from '@mui/material/MenuItem';
import { paperClasses } from '@mui/material/Paper';
import { listClasses } from '@mui/material/List';
import ListItemText from '@mui/material/ListItemText';
import ListItemIcon, { listItemIconClasses } from '@mui/material/ListItemIcon';
import LogoutRoundedIcon from '@mui/icons-material/LogoutRounded';
import MoreVertRoundedIcon from '@mui/icons-material/MoreVertRounded';
import MenuButton from './MenuButton';
import { logoutUser } from '../../api/authApi';
import { useNavigate } from 'react-router-dom';
import Settings from '../AccountSettings';
import FeedbackModal from '../FeedbackModal';
import AboutModal from '../AboutModal'; // Import the AboutModal

const MenuItem = styled(MuiMenuItem)({
  margin: '2px 0',
});

export default function OptionsMenu() {
  const navigate = useNavigate();
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [settingsOpen, setSettingsOpen] = React.useState(false);
  const [feedbackOpen, setFeedbackOpen] = React.useState(false);
  const [aboutOpen, setAboutOpen] = React.useState(false); // State for AboutModal

  const open = Boolean(anchorEl);

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogOut = async () => {
    try {
      logoutUser();
      sessionStorage.clear();
      navigate('/signin');
    } catch (error) {
      console.error('Error during logout:', error);
    }
  };

  const handleSettingsOpen = () => {
    console.log('Opening settings');  // Debug log
    setSettingsOpen(true);
    handleClose();
  };

  const handleSettingsClose = () => {
    setSettingsOpen(false);
  };

  const handleFeedbackOpen = () => {
    console.log('Opening feedback modal');  // Debug log
    setFeedbackOpen(true);
    handleClose();
  };

  const handleFeedbackClose = () => {
    console.log('Closing feedback modal');  // Debug log
    setFeedbackOpen(false);
  };

  const handleAboutOpen = () => {
    console.log('Opening about modal');  
    setAboutOpen(true);
    handleClose();
  };

  const handleAboutClose = () => {
    console.log('Closing about modal'); 
    setAboutOpen(false);
  };

  return (
    <React.Fragment>
      <MenuButton
        aria-label="Open menu"
        onClick={handleClick}
        sx={{ borderColor: 'transparent' }}
      >
        <MoreVertRoundedIcon />
      </MenuButton>
      <Menu
        anchorEl={anchorEl}
        id="menu"
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
        sx={{
          [`& .${listClasses.root}`]: {
            padding: '4px',
          },
          [`& .${paperClasses.root}`]: {
            padding: 0,
          },
          [`& .${dividerClasses.root}`]: {
            margin: '4px -4px',
          },
        }}
      >
        <MenuItem onClick={handleSettingsOpen}>My account</MenuItem>
        <Divider />
        <MenuItem onClick={handleFeedbackOpen}>Feedback</MenuItem> {/* Feedback item */}
        <Divider />
        <MenuItem onClick={handleAboutOpen}>About</MenuItem> 
        <Divider />
        <MenuItem
          onClick={handleLogOut}
          sx={{
            [`& .${listItemIconClasses.root}`]: {
              ml: 'auto',
              minWidth: 0,
            },
          }}
        >
          <ListItemText>Logout</ListItemText>
          <ListItemIcon>
            <LogoutRoundedIcon fontSize="small" />
          </ListItemIcon>
        </MenuItem>
      </Menu>

      {/* Render Settings Component as Popup */}
      <Settings open={settingsOpen} onClose={handleSettingsClose} />

      {/* Render Feedback Modal */}
      <FeedbackModal open={feedbackOpen} onClose={handleFeedbackClose} />

      {/* Render About Modal */}
      <AboutModal open={aboutOpen} onClose={handleAboutClose} /> 
    </React.Fragment>
  );
}
