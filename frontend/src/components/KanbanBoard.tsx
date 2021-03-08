import React, { ReactElement, useEffect, useReducer, useState } from 'react';
import {
  DragDropContext,
  DraggableLocation,
  Droppable,
  DropResult,
} from 'react-beautiful-dnd';
import KanbanColumn from './KanbanColumn';
import { reducer, Action } from '../lib/state';
import { TaskContext } from '../lib/taskContext';
import { getColumnsWithCards, ColumnWithCards, moveCard } from '../lib/api';
import Grid from '@material-ui/core/Grid';
import ErrorDialog from './dialogs/ErrorDialog';

function KanbanBoard(): ReactElement {
  const [notifyOpen, setNotifyOpen] = useState(false);
  const [{ columns }, dispatch] = useReducer(reducer, {
    columns: {},
  });

  useEffect(() => {
    getColumnsWithCards()
      .then((columns) => {
        dispatch({ type: 'init', payload: columns });
      })
      .catch(() => {
        setNotifyOpen(true);
      });
  }, []);

  return (
    <>
      <TaskContext.Provider value={{ columns, dispatch }}>
        <Grid container justify="center" style={{ maxWidth: '100%' }}>
          <DragDropContext
            onDragEnd={(result) =>
              onDragEnd(result, columns, dispatch, setNotifyOpen)
            }
          >
            {Object.entries(columns).map(([id, column]) => {
              return (
                <Grid item key={id} xs={12} sm={6} lg={3}>
                  <Droppable droppableId={String(column.id)} key={column.id}>
                    {(provided) => (
                      <KanbanColumn
                        provided={provided}
                        cards={column.cards}
                        column={column}
                      />
                    )}
                  </Droppable>
                </Grid>
              );
            })}
          </DragDropContext>
        </Grid>
      </TaskContext.Provider>

      <ErrorDialog open={notifyOpen} onClose={() => setNotifyOpen(false)} />
    </>
  );
}

export default KanbanBoard;

interface MoveData {
  cardID: number;
  previousCardId: number | null;
  columnID: number;
}

function onDragEnd(
  result: DropResult,
  columns: ColumnWithCards,
  dispatch: React.Dispatch<Action>,
  setNotify: React.Dispatch<React.SetStateAction<boolean>>
): void {
  if (!result.destination) return;
  const { source, destination } = result;
  const rollbackState = { ...columns };
  let move: MoveData;

  if (source.droppableId !== destination.droppableId) {
    move = moveToOtherColumn(columns, dispatch, source, destination);
  } else {
    move = moveInSameColumn(columns, dispatch, source, destination);
  }

  moveCard(move.cardID, move.previousCardId, move.columnID).catch(() => {
    dispatch({ type: 'init', payload: rollbackState });
    setNotify(true);
  });
}

function moveToOtherColumn(
  columns: ColumnWithCards,
  dispatch: React.Dispatch<Action>,
  source: DraggableLocation,
  destination: DraggableLocation
): MoveData {
  const sourceColumn = columns[source.droppableId];
  const destColumn = columns[destination.droppableId];
  const sourceItems = [...sourceColumn.cards];
  const destItems = [...destColumn.cards];
  const [removed] = sourceItems.splice(source.index, 1);
  const previousCardId = destItems[destination.index - 1]?.id || null;
  const newColumnId = Number(destination.droppableId);
  removed.columnID = newColumnId;
  destItems.splice(destination.index, 0, removed);
  dispatch({
    type: 'init',
    payload: {
      ...columns,
      [source.droppableId]: {
        ...sourceColumn,
        cards: sourceItems,
      },
      [destination.droppableId]: {
        ...destColumn,
        cards: destItems,
      },
    },
  });
  return { cardID: removed.id, previousCardId, columnID: newColumnId };
}

function moveInSameColumn(
  columns: ColumnWithCards,
  dispatch: React.Dispatch<Action>,
  source: DraggableLocation,
  destination: DraggableLocation
): MoveData {
  const column = columns[source.droppableId];
  const copiedItems = [...column.cards];
  const [removed] = copiedItems.splice(source.index, 1);
  const previousCardId = copiedItems[destination.index - 1]?.id || null;
  copiedItems.splice(destination.index, 0, removed);
  dispatch({
    type: 'init',
    payload: {
      ...columns,
      [source.droppableId]: {
        ...column,
        cards: copiedItems,
      },
    },
  });
  return { cardID: removed.id, previousCardId, columnID: column.id };
}
