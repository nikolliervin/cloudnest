import * as React from 'react';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';

export default function AboutModal({ open, onClose }) {
  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>About</DialogTitle>
      <DialogContent>
        <p>CloudNest is a project I've been working on to help you manage files and groups in the cloud. It's built with a solid backend in .NET, React on the frontend, and has some cool features like file uploads and group permissions. The goal is to make cloud storage more seamless and customizable for everyone!</p>
        <p>Version 1.0.0</p>
        <p>Developed by <a href="https://github.com/nikolliervin" target="_blank" rel="noopener noreferrer">Ervin Nikolli</a></p>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
}
