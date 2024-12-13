import * as React from 'react';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';

export default function FeedbackModal({ open, onClose }) {
  const [feedback, setFeedback] = React.useState('');

  const handleFeedbackChange = (event) => {
    setFeedback(event.target.value);
  };

  const handleSubmit = () => {
    console.log('Feedback submitted:', feedback);
    onClose(); // Close modal after feedback is submitted
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Feedback</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          label="Your Feedback"
          type="text"
          fullWidth
          variant="outlined"
          value={feedback}
          onChange={handleFeedbackChange}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Cancel
        </Button>
        <Button onClick={handleSubmit} color="primary">
          Submit
        </Button>
      </DialogActions>
    </Dialog>
  );
}
