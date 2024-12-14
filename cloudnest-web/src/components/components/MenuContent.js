import * as React from 'react';
import { useState, useEffect } from 'react';
import { getStorageData } from '../../api/settingsApi';  // Import the API function to fetch storage data
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Stack from '@mui/material/Stack';
import StorageIcon from '@mui/icons-material/Storage';
import DevicesIcon from '@mui/icons-material/Devices';
import HomeIcon from '@mui/icons-material/Home';
import FolderSharedIcon from '@mui/icons-material/FolderShared';
import ScheduleIcon from '@mui/icons-material/Schedule';
import GradeIcon from '@mui/icons-material/Grade';
import DeleteIcon from '@mui/icons-material/Delete';
import Divider from '@mui/material/Divider';
import Modal from '@mui/material/Modal';
import DriveDetailsModal from '../DriveDetailsModal';
import LinearProgress from '@mui/material/LinearProgress';

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

export default function MenuContent() {
  const [selectedIndex, setSelectedIndex] = React.useState(0);
  const [openModal, setOpenModal] = React.useState(false);
  const [selectedDrive, setSelectedDrive] = React.useState(null); // To store selected drive's details
  const [drives, setDrives] = useState([]); // To store the drives data
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const handleListItemClick = (index) => {
    setSelectedIndex(index);
  };

  const handleOpenModal = (drive) => {
    setSelectedDrive(drive);
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setOpenModal(false); 
    setSelectedDrive(null); 
  };

  
  useEffect(() => {
    const fetchStorageData = async () => {
      try {
        const data = await getStorageData();  
        setDrives(data);  
        setLoading(false);
      } catch (error) {
        setError(error.message);
        setLoading(false);
      }
    };
    
    fetchStorageData();
  }, []);

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

   
      <List dense>
        {loading && <div>Loading drives...</div>}  
        {error && <div style={{ color: 'red' }}>{error}</div>} 

        {drives.map((drive, index) => (
          <ListItem key={index} disablePadding sx={{ display: 'block' }}>
            <ListItemButton onClick={() => handleOpenModal(drive)}>
              <ListItemIcon>
                <StorageIcon />
              </ListItemIcon>
              <ListItemText
                primary={`Drive: ${drive.driveName}`}
                secondary={`Used: ${drive.usedSpace} GB | Free: ${drive.freeSpace} GB`}
              />
            </ListItemButton>

            
            <LinearProgress
              variant="determinate"
              value={(drive.usedSpace / drive.totalSize) * 100}
              sx={{ mt: 1 }}
            />
          </ListItem>
        ))}
      </List>

      <Divider />

      
      <Modal open={openModal} onClose={handleCloseModal}>
        <DriveDetailsModal 
          selectedDrive={selectedDrive} 
          onClose={handleCloseModal} 
        />
      </Modal>

      <Stack sx={{ flexGrow: 1 }} />
    </Stack>
  );
}
