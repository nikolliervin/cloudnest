import * as React from 'react';
import { Button, Typography, Stack, Box } from '@mui/material';

export default function DriveDetailsModal({ selectedDrive, onClose }) {
  return (
    <Box
      sx={{
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 400,
        bgcolor: 'background.paper',
        borderRadius: '12px', // rounded corners
        boxShadow: 24,
        p: 4,
      }}
    >
      <Typography variant="h6" component="h2">
        Drive Details
      </Typography>
      <Typography sx={{ mt: 2 }}>Drive Name: {selectedDrive?.driveName}</Typography>
      <Typography>File System: {selectedDrive?.fileSystem || "N/A"}</Typography>
      <Typography>Total Size: {selectedDrive?.totalSize} GB</Typography>
      <Typography>Used Space: {selectedDrive?.usedSpace} GB</Typography>
      <Typography>Free Space: {selectedDrive?.freeSpace} GB</Typography>
      <Typography>Mount Point: {selectedDrive?.mountPoint || "N/A"}</Typography>
      
      <Stack direction="row" spacing={2} sx={{ mt: 2 }}>
        <Button variant="contained" color="primary" onClick={onClose}>
          Close
        </Button>
      </Stack>
    </Box>
  );
}
