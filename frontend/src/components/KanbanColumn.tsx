import React, { CSSProperties, useState } from 'react';
import { Draggable, DroppableProvided } from 'react-beautiful-dnd';
import { Column } from '../lib/api';
import KanbanTask from './KanbanTask';
import StyledBadge from './customUI/StyledBadge';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import AddRoundedIcon from '@material-ui/icons/AddRounded';
import ErrorDialog from './dialogs/ErrorDialog';
import KanbanCardDialog from './dialogs/KanbanCardDialog';
import Paper from '@material-ui/core/Paper';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import { useTaskContext } from '../hooks/useTaskContext';
import { KanbanCard, createCard } from '../lib/api';
import { CardEditState } from '../lib/interfaces';

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

const columnStyle: CSSProperties = {
  background: '#010409',
  margin: 8,
};

interface Props {
  provided: DroppableProvided;
  cards: KanbanCard[];
  column: Column;
}

const KanbanColumn: React.FC<Props> = ({ provided, cards, column }) => {
  const matches = useMediaQuery('(max-width:600px)');
  const state = useTaskContext();
  const [isAdding, setIsAdding] = useState(false);
  const [notifyOpen, setNotifyOpen] = useState(false);
  const [addState, setAddState] = useState<CardEditState>({
    title: '',
    description: '',
    deadline: new Date(),
  });

  const containerStyle: CSSProperties = {
    padding: 4,
    height: matches ? 'auto' : 'calc(100vh - 150px)',
    marginTop: 0,
    background: '#010409',
    overflow: 'auto',
  };

  const resetAddState = () => {
    setIsAdding(false);
    setAddState({
      title: '',
      description: '',
      deadline: new Date(),
    });
  };

  const handleTaskAdding = () => {
    createCard({
      title: addState.title,
      description: addState.description,
      deadline: addState.deadline,
      columnID: column.id,
    })
      .then((card) => {
        state?.dispatch({
          type: 'add-new-card',
          payload: card,
        });
      })
      .catch(() => setNotifyOpen(true));
    resetAddState();
  };

  return (
    <>
      <Paper style={columnStyle}>
        <div style={topStyle}>
          <div style={topNavStyle}>
            <div style={{ flexGrow: 1 }}>
              <StyledBadge badgeContent={cards.length} color="primary">
                <Typography variant="h6">{column.name}</Typography>
              </StyledBadge>
            </div>
            <IconButton
              aria-label="add a task"
              onClick={() => setIsAdding(true)}
            >
              <AddRoundedIcon />
            </IconButton>
          </div>
        </div>

        <Paper
          {...provided.droppableProps}
          ref={provided.innerRef}
          style={containerStyle}
        >
          {cards.map((card, index) => (
            <Draggable
              key={card.id}
              draggableId={`${column.id}-${card.id}`}
              index={index}
            >
              {(provided) => <KanbanTask provided={provided} card={card} />}
            </Draggable>
          ))}
          {provided.placeholder}
        </Paper>
      </Paper>

      <KanbanCardDialog
        onSave={handleTaskAdding}
        onClose={resetAddState}
        open={isAdding}
        setState={setAddState}
        state={addState}
      />
      <ErrorDialog open={notifyOpen} onClose={() => setNotifyOpen(false)} />
    </>
  );
};

export default KanbanColumn;
