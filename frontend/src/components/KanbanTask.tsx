import React, { useState } from 'react';
import { CSSProperties } from 'react';
import { DraggableProvided } from 'react-beautiful-dnd';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import MoreHorizRoundedIcon from '@material-ui/icons/MoreHorizRounded';
import IconButton from '@material-ui/core/IconButton';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import ErrorDialog from './dialogs/ErrorDialog';
import KanbanCardDialog from './dialogs/KanbanCardDialog';
import { useTaskContext } from '../hooks/useTaskContext';
import AreYouSureDialog from './dialogs/AreYouSureDialog';
import { deleteCard, KanbanCard, updateCard } from '../lib/api';
import { CardEditState } from '../lib/interfaces';

const taskStyle: CSSProperties = {
  color: 'white',
  userSelect: 'none',
  margin: '8px 5px',
  minHeight: '50px',
  background: '#21262d',
  border: '1px solid #30363d',
  borderRadius: '3px',
};

const titleStyle: CSSProperties = {
  marginBottom: 4,
};

const menuIconStyle: CSSProperties = {
  margin: 0,
  padding: '2px 4px',
};

const topStyle: CSSProperties = {
  display: 'flex',
  alignItems: 'center',
};

interface Props {
  provided: DraggableProvided;
  card: KanbanCard;
}

const KanbanTask: React.FC<Props> = ({ provided, card }) => {
  const state = useTaskContext();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [editOpen, setEditOpen] = useState(false);
  const [sureOpen, setSureOpen] = useState(false);
  const [notifyOpen, setNotifyOpen] = useState(false);
  const [editState, setEditState] = useState<CardEditState>({ ...card });

  const onSave = () => {
    const newCard = { ...card, ...editState };
    updateCard(newCard)
      .then(() => {
        state?.dispatch({
          type: 'edit-card',
          payload: { old: card, new: newCard },
        });
      })
      .catch(() => setNotifyOpen(true));
    setEditOpen(false);
  };

  const onDelete = () => {
    deleteCard(card.id)
      .then(() => {
        state?.dispatch({ type: 'delete-card', payload: card });
      })
      .catch(() => setNotifyOpen(true));
    setSureOpen(false);
  };

  return (
    <>
      <Card
        ref={provided.innerRef}
        {...provided.draggableProps}
        {...provided.dragHandleProps}
        style={{
          ...provided.draggableProps.style,
          ...taskStyle,
        }}
      >
        <CardContent>
          <div style={topStyle}>
            <Typography
              color="textSecondary"
              variant="subtitle2"
              style={{ flexGrow: 1 }}
            >
              {new Date(card.deadline).toISOString().substr(0, 10)}
            </Typography>
            <IconButton
              aria-label="more"
              aria-controls="long-menu"
              aria-haspopup="true"
              onClick={(e) => setAnchorEl(e.currentTarget)}
              style={menuIconStyle}
            >
              <MoreHorizRoundedIcon />
            </IconButton>
            <Menu
              id="long-menu"
              anchorEl={anchorEl}
              keepMounted
              open={Boolean(anchorEl)}
              onClose={() => setAnchorEl(null)}
            >
              <MenuItem
                onClick={() => {
                  setAnchorEl(null);
                  setEditOpen(true);
                }}
              >
                Edit
              </MenuItem>
              <MenuItem
                onClick={() => {
                  setAnchorEl(null);
                  setSureOpen(true);
                }}
              >
                Delete
              </MenuItem>
            </Menu>
          </div>
          <Typography variant="h6" component="h2" style={titleStyle}>
            {card.title}
          </Typography>
          <Typography variant="body2" color="textSecondary" component="p">
            {card.description}
          </Typography>
        </CardContent>
      </Card>

      <KanbanCardDialog
        onClose={() => setEditOpen(false)}
        onSave={onSave}
        open={editOpen}
        state={editState}
        setState={setEditState}
      />
      <AreYouSureDialog
        text={`You are trying to delete a task, named: ${card.title}. Are you sure about that?`}
        open={sureOpen}
        onClose={() => setSureOpen(false)}
        onAgree={onDelete}
      />
      <ErrorDialog open={notifyOpen} onClose={() => setNotifyOpen(false)} />
    </>
  );
};

export default KanbanTask;
