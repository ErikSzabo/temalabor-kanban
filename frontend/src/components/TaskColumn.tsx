import React, { CSSProperties, useState } from 'react';
import { Draggable, DroppableProvided } from 'react-beautiful-dnd';
import { Column } from '../api';
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
import { KanbanCard, createCard } from '../api';

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
  cards: KanbanCard[];
  column: Column;
}

interface State {
  title: string;
  description: string;
  date?: Date;
}

const TaskColumn: React.FC<Props> = ({ provided, cards, column }) => {
  const matches = useMediaQuery('(max-width:600px)');
  const state = useTaskContext();
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
    createCard({
      title: addState.title,
      description: addState.description,
      deadline: addState.date || new Date(),
      columnID: column.id,
    }).then((card) => {
      state?.dispatch({
        type: 'add-new-task',
        payload: card,
      });
    });
    resetAddState();
  };

  return (
    <Paper style={{ background: '#010409', margin: 8 }}>
      <div style={topStyle}>
        <div style={topNavStyle}>
          <div style={{ display: 'flex', flexGrow: 1 }}>
            <StyledBadge badgeContent={cards.length} color="primary">
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
        {cards.map((card, index) => {
          return (
            <Draggable
              key={card.id}
              draggableId={`${column.id}-${card.id}`}
              index={index}
            >
              {(provided) => {
                return <TaskElement provided={provided} card={card} />;
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
