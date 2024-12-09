import * as React from 'react';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Stack from '@mui/material/Stack';
import StorageIcon from '@mui/icons-material/Storage';
import DevicesIcon from '@mui/icons-material/Devices';
import HomeIcon from '@mui/icons-material/Home';
import SettingsRoundedIcon from '@mui/icons-material/SettingsRounded';
import InfoRoundedIcon from '@mui/icons-material/InfoRounded';
import HelpRoundedIcon from '@mui/icons-material/HelpRounded';
import FolderSharedIcon from '@mui/icons-material/FolderShared';
import ScheduleIcon from '@mui/icons-material/Schedule';
import GradeIcon from '@mui/icons-material/Grade';
import DeleteIcon from '@mui/icons-material/Delete';
import Divider from '@mui/material/Divider';

const firstListItems = [
  { text: 'Home', icon: <HomeIcon /> },
  { text: 'My Storage', icon: <StorageIcon /> },
  { text: 'Devices', icon: <DevicesIcon /> },
];

const secondListItems = [
  { text: 'Shared with me', icon: <FolderSharedIcon /> },
  { text: 'Recent', icon: <ScheduleIcon /> },
  { text: 'Favorites', icon: <GradeIcon /> },
];

const thirdListItems = [
  { text: 'Deleted', icon: <DeleteIcon /> },
];

const secondaryListItems = [
  { text: 'Settings', icon: <SettingsRoundedIcon /> },
  { text: 'About', icon: <InfoRoundedIcon /> },
  { text: 'Feedback', icon: <HelpRoundedIcon /> },
];

export default function MenuContent() {
  const [selectedIndex, setSelectedIndex] = React.useState(0);

  const handleListItemClick = (index) => {
    setSelectedIndex(index);
  };

  return (
    <Stack sx={{ flexGrow: 1, p: 1, justifyContent: 'space-between' }}>
      <List dense>
        {firstListItems.map((item, index) => (
          <ListItem key={index} disablePadding sx={{ display: 'block' }}>
            <ListItemButton 
              selected={selectedIndex === index} 
              onClick={() => handleListItemClick(index)} 
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>

      <Divider />

      <List dense>
        {secondListItems.map((item, index) => (
          <ListItem key={index} disablePadding sx={{ display: 'block' }}>
            <ListItemButton 
              selected={selectedIndex === index + firstListItems.length} 
              onClick={() => handleListItemClick(index + firstListItems.length)} 
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>

      <Divider />

      <List dense>
        {thirdListItems.map((item, index) => (
          <ListItem key={index} disablePadding sx={{ display: 'block' }}>
            <ListItemButton 
              selected={selectedIndex === index + firstListItems.length + secondListItems.length} 
              onClick={() => handleListItemClick(index + firstListItems.length + secondListItems.length)} 
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>

      <Divider />

      <Stack sx={{ flexGrow: 1 }} /> 
      
      <List dense>
        {secondaryListItems.map((item, index) => (
          <ListItem key={index} disablePadding sx={{ display: 'block' }}>
            <ListItemButton 
              selected={selectedIndex === index + firstListItems.length + secondListItems.length + thirdListItems.length} 
              onClick={() => handleListItemClick(index + firstListItems.length + secondListItems.length + thirdListItems.length)} 
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Stack>
  );
}
