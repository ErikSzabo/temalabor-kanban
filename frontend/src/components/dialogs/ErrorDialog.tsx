import React from 'react';
import Snackbar from '@material-ui/core/Snackbar';

interface Props {
  open: boolean;
  onClose: () => void;
}

const ErrorDialog: React.FC<Props> = ({ open, onClose }) => {
  return (
    <Snackbar
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'left',
      }}
      open={open}
      autoHideDuration={5000}
      onClose={onClose}
      message="Server error, check your internet connection"
    />
  );
};

export default ErrorDialog;
