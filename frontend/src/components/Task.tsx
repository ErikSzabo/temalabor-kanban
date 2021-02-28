import React, { useState } from 'react';
import { CSSProperties } from 'react';
import { DraggableProvided } from 'react-beautiful-dnd';
import { Task } from '../lib/interfaces';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import ListRoundedIcon from '@material-ui/icons/ListRounded';
import MoreHorizRoundedIcon from '@material-ui/icons/MoreHorizRounded';
import IconButton from '@material-ui/core/IconButton';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import AddTaskDialog from './dialogs/AddTaskDialog';
import { v4 as uuidv4 } from 'uuid';
import { useTaskContext } from '../hooks/useTaskContext';
import AreYouSureDialog from './dialogs/AreYouSureDialog';

interface State {
  title: string;
  description: string;
  date?: Date;
}

interface Props {
  provided: DraggableProvided;
  task: Task;
}

const taskStyle: CSSProperties = {
  color: 'white',
  userSelect: 'none',
  margin: '8px 5px',
  minHeight: '50px',
  background: '#21262d',
  border: '1px solid #30363d',
  borderRadius: '3px',
};

const TaskElement: React.FC<Props> = ({ provided, task }) => {
  const state = useTaskContext();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [open, setOpen] = useState<boolean>(false);
  const [sureOpen, setSureOpen] = useState<boolean>(false);
  const [editState, setEditState] = useState<State>({
    title: task.title,
    description: task.description,
    date: task.deadline,
  });
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const onClose = () => {
    setOpen(false);
  };
  const onSave = () => {
    state?.dispatch({
      type: 'edit-task',
      payload: {
        old: task,
        new: {
          title: editState.title,
          description: editState.description,
          deadline: editState.date || new Date(),
          id: uuidv4(),
          columnId: task.columnId,
        },
      },
    });
    onClose();
  };

  const onDelete = () => {
    state?.dispatch({ type: 'delete-task', payload: task });
    handleClose();
    setSureOpen(false);
  };

  return (
    <Card
      ref={provided.innerRef}
      {...provided.draggableProps}
      {...provided.dragHandleProps}
      style={{
        ...taskStyle,
        ...provided.draggableProps.style,
      }}
    >
      <CardContent>
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <Typography
            color="textSecondary"
            variant="subtitle2"
            style={{ flexGrow: 1 }}
          >
            {task.deadline.toISOString().substr(0, 10)}
          </Typography>
          <IconButton
            aria-label="more"
            aria-controls="long-menu"
            aria-haspopup="true"
            onClick={handleClick}
            style={{ margin: 0, padding: '2px 4px' }}
          >
            <MoreHorizRoundedIcon />
          </IconButton>
          <Menu
            id="long-menu"
            anchorEl={anchorEl}
            keepMounted
            open={Boolean(anchorEl)}
            onClose={handleClose}
          >
            <MenuItem
              onClick={() => {
                handleClose();
                setOpen(true);
              }}
            >
              Edit
            </MenuItem>
            <MenuItem
              onClick={() => {
                handleClose();
                setSureOpen(true);
              }}
            >
              Delete
            </MenuItem>
          </Menu>
        </div>
        <Typography
          variant="h6"
          component="h2"
          style={{
            display: 'flex',
            alignItems: 'center',
            gap: 10,
            marginBottom: 4,
          }}
        >
          <ListRoundedIcon />
          {task.title}
        </Typography>
        <Typography variant="body2" color="textSecondary" component="p">
          {task.description}
        </Typography>
      </CardContent>
      <AddTaskDialog
        onClose={onClose}
        onSave={onSave}
        open={open}
        state={editState}
        setState={setEditState}
      />
      <AreYouSureDialog
        text={`You are trying to delete a task, named: ${task.title}. Are you sure about that?`}
        open={sureOpen}
        onClose={() => setSureOpen(false)}
        onAgree={onDelete}
      />
    </Card>
  );
};

export default TaskElement;
