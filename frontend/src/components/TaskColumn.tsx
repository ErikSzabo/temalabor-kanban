import React, { CSSProperties, useState } from 'react';
import { Draggable, DroppableProvided } from 'react-beautiful-dnd';
import { Column, Task } from '../lib/interfaces';
import TaskElement from './Task';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import AddRoundedIcon from '@material-ui/icons/AddRounded';
import AddTaskDialog from './dialogs/AddTaskDialog';
import { useTaskContext } from '../hooks/useTaskContext';
import Paper from '@material-ui/core/Paper';
import Badge from '@material-ui/core/Badge';
import { withStyles, createStyles } from '@material-ui/core/styles';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import { v4 as uuidv4 } from 'uuid';

const StyledBadge = withStyles(() =>
  createStyles({
    badge: {
      right: -20,
      top: 16,
      padding: '0 4px',
    },
  })
)(Badge);

const topStyle: CSSProperties = {
  padding: 4,
  marginBottom: 0,
  color: 'white',
};

const topNavStyle: CSSProperties = {
  padding: '0 10px',
  display: 'flex',
  alignItems: 'center',
};

interface Props {
  provided: DroppableProvided;
  tasks: Task[];
  column: Column;
}

interface State {
  title: string;
  description: string;
  date?: Date;
}

const TaskColumn: React.FC<Props> = ({ provided, tasks, column }) => {
  const matches = useMediaQuery('(max-width:600px)');
  const state = useTaskContext();
  const columns = state?.columns;
  const [isAdding, setIsAdding] = useState<boolean>(false);
  const [addState, setAddState] = useState<State>({
    title: '',
    description: '',
    date: new Date(),
  });

  const containerStyle: CSSProperties = {
    padding: 4,
    minHeight: matches ? 'auto' : 'calc(100vh - 150px)',
    marginTop: 0,
  };

  const resetAddState = () => {
    setIsAdding(false);
    setAddState({
      title: '',
      description: '',
      date: new Date(),
    });
  };

  const handleTaskAdding = () => {
    state?.dispatch({
      type: 'add-new-task',
      payload: {
        title: addState.title,
        description: addState.description,
        deadline: addState.date || new Date(),
        id: uuidv4(),
        columnId: column.id,
      },
    });
    resetAddState();
  };

  return (
    <Paper style={{ background: '#010409', margin: 8 }}>
      <div style={topStyle}>
        <div style={topNavStyle}>
          <div style={{ display: 'flex', flexGrow: 1 }}>
            <StyledBadge
              badgeContent={columns![column.id].tasks.length}
              color="primary"
            >
              <Typography variant="h6">{column.name}</Typography>
            </StyledBadge>
          </div>
          <IconButton aria-label="add a task" onClick={() => setIsAdding(true)}>
            <AddRoundedIcon />
          </IconButton>
          <AddTaskDialog
            onSave={handleTaskAdding}
            onClose={resetAddState}
            open={isAdding}
            setState={setAddState}
            state={addState}
          />
        </div>
      </div>
      <div
        {...provided.droppableProps}
        ref={provided.innerRef}
        style={containerStyle}
      >
        {tasks.map((task, index) => {
          return (
            <Draggable
              key={task.id}
              draggableId={`${column.id}-${task.id}`}
              index={index}
            >
              {(provided) => {
                return <TaskElement provided={provided} task={task} />;
              }}
            </Draggable>
          );
        })}
        {provided.placeholder}
      </div>
    </Paper>
  );
};

export default TaskColumn;
