import { ColumnWithCards, KanbanCard } from './api';

export interface State {
  columns: ColumnWithCards;
}

export type Action =
  | { type: 'add-new-card'; payload: KanbanCard }
  | { type: 'delete-card'; payload: KanbanCard }
  | { type: 'edit-card'; payload: { old: KanbanCard; new: KanbanCard } }
  | { type: 'init'; payload: ColumnWithCards };

export const reducer = (state: State, action: Action): State => {
  switch (action.type) {
    case 'add-new-card':
      return addCard(state, action.payload);
    case 'delete-card':
      return deleteCard(state, action.payload);
    case 'edit-card':
      return editCard(state, action.payload.old, action.payload.new);
    case 'init':
      return { columns: action.payload };
    default:
      return { ...state };
  }
};

function addCard(state: State, card: KanbanCard): State {
  const newColumns = { ...state.columns };
  newColumns[card.columnID].cards.push(card);
  return { columns: newColumns };
}

function deleteCard(state: State, card: KanbanCard): State {
  const newColumns = { ...state.columns };
  const newTasks = newColumns[card.columnID].cards;
  newTasks.splice(newTasks.indexOf(card), 1);
  newColumns[card.columnID].cards = newTasks;
  return { columns: newColumns };
}

function editCard(
  state: State,
  oldCard: KanbanCard,
  newCard: KanbanCard
): State {
  const newColumns = { ...state.columns };
  const newTasks = newColumns[oldCard.columnID].cards;
  newTasks.splice(newTasks.indexOf(oldCard), 1, newCard);
  newColumns[oldCard.columnID].cards = newTasks;
  return { columns: newColumns };
}
