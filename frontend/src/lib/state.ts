import { ColumnWithCards, KanbanCard } from '../api';

export interface State {
  columns: ColumnWithCards;
}

export type Action =
  | { type: 'add-new-task'; payload: KanbanCard }
  | { type: 'delete-task'; payload: KanbanCard }
  | { type: 'edit-task'; payload: { old: KanbanCard; new: KanbanCard } }
  | { type: 'init'; payload: { columns: ColumnWithCards } };

export const reducer = (state: State, action: Action): State => {
  switch (action.type) {
    case 'add-new-task':
      return addTask(state, action.payload);
    case 'delete-task':
      return deleteTask(state, action.payload);
    case 'edit-task':
      return editTask(state, action.payload.old, action.payload.new);
    case 'init':
      return { columns: action.payload.columns };
    default:
      return { ...state };
  }
};

function addTask(state: State, card: KanbanCard): State {
  const newColumns = { ...state.columns };
  newColumns[card.columnID].cards.push(card);
  return { columns: newColumns };
}

function deleteTask(state: State, card: KanbanCard): State {
  const newColumns = { ...state.columns };
  const newTasks = newColumns[card.columnID].cards;
  newTasks.splice(newTasks.indexOf(card), 1);
  newColumns[card.columnID].cards = newTasks;
  return { columns: newColumns };
}

function editTask(
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
