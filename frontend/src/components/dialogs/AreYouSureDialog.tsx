import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContentText from '@material-ui/core/DialogContentText';

interface Props {
  open: boolean;
  text: string;
  onClose: () => void;
  onAgree: () => void;
}

const AreYouSureDialog: React.FC<Props> = ({
  open,
  onClose,
  onAgree,
  text,
}) => {
  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle id="are-your-sure-dialog-title">Are you sure?</DialogTitle>
      <DialogContent>
        <DialogContentText>{text}</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={onAgree} color="primary" variant="contained" autoFocus>
          Do it!
        </Button>
        <Button onClick={onClose} color="secondary" variant="contained">
          Cancel
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AreYouSureDialog;
