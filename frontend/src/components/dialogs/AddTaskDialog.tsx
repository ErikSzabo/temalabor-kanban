import React from 'react';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import DateFnsUtils from '@date-io/date-fns';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent';
import DialogActions from '@material-ui/core/DialogActions';
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
} from '@material-ui/pickers';

interface State {
  title: string;
  description: string;
  date?: Date;
}

interface Props {
  open: boolean;
  onClose: () => void;
  onSave: () => void;
  state: State;
  setState: React.Dispatch<React.SetStateAction<State>>;
}

const AddTaskDialog: React.FC<Props> = ({
  open,
  onClose,
  state,
  setState,
  onSave,
}) => {
  const handleDateChange = (date: Date | null) => {
    setState({ ...state, date: date ? date : undefined });
  };

  return (
    <Dialog open={open} onClose={onClose} aria-labelledby="Add new task">
      <DialogTitle id="form-dialog-title">Task</DialogTitle>
      <DialogContent>
        <TextField
          id="outlined-basic"
          value={state.title}
          onChange={(e) =>
            setState((prev) => ({ ...prev, title: e.target.value }))
          }
          label="Title"
          variant="outlined"
          style={{ width: '100%' }}
        />
        <TextField
          id="outlined-multiline-static"
          label="Description"
          value={state.description}
          onChange={(e) =>
            setState((prev) => ({ ...prev, description: e.target.value }))
          }
          multiline
          rows={4}
          variant="outlined"
          style={{ marginTop: 8, width: '100%' }}
        />
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
          <KeyboardDatePicker
            margin="normal"
            id="date-picker-dialog"
            label="Deadline"
            format="yyyy/MM/dd"
            value={state.date}
            onChange={handleDateChange}
            style={{ width: '100%' }}
            KeyboardButtonProps={{
              'aria-label': 'change date',
            }}
          />
        </MuiPickersUtilsProvider>
      </DialogContent>
      <DialogActions>
        <div style={{ display: 'flex', gap: 10, margin: '5px 0' }}>
          <Button variant="contained" color="primary" onClick={onSave}>
            Save
          </Button>
          <Button variant="contained" color="secondary" onClick={onClose}>
            Cancel
          </Button>
        </div>
      </DialogActions>
    </Dialog>
  );
};

export default AddTaskDialog;
