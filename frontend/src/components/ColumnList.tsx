import React, { ReactElement, useEffect, useReducer } from 'react';
import { DragDropContext, Droppable, DropResult } from 'react-beautiful-dnd';
import TaskColumn from './TaskColumn';
import { reducer, Action } from '../lib/state';
import { TaskContext } from '../lib/taskContext';
import { getColumnsWithCards, ColumnWithCards, moveCard } from '../api';
import Grid from '@material-ui/core/Grid';

const onDragEnd = (
  result: DropResult,
  columns: ColumnWithCards,
  dispatch: React.Dispatch<Action>
) => {
  if (!result.destination) return;
  const { source, destination } = result;

  if (source.droppableId !== destination.droppableId) {
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
        columns: {
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
      },
    });
    moveCard(removed.id, previousCardId, newColumnId).catch(() => {
      // Do something
    });
  } else {
    const column = columns[source.droppableId];
    const copiedItems = [...column.cards];
    const [removed] = copiedItems.splice(source.index, 1);
    const previousCardId = copiedItems[destination.index - 1]?.id || null;
    copiedItems.splice(destination.index, 0, removed);
    dispatch({
      type: 'init',
      payload: {
        columns: {
          ...columns,
          [source.droppableId]: {
            ...column,
            cards: copiedItems,
          },
        },
      },
    });
    moveCard(removed.id, previousCardId, column.id).catch(() => {
      // Do something
    });
  }
};

function ColumnList(): ReactElement {
  const [{ columns }, dispatch] = useReducer(reducer, {
    columns: {},
  });

  useEffect(() => {
    getColumnsWithCards().then((columns) => {
      dispatch({ type: 'init', payload: { columns } });
    });
  }, []);

  return (
    <TaskContext.Provider value={{ columns, dispatch }}>
      <Grid container justify="center" style={{ maxWidth: '100%' }}>
        <DragDropContext
          onDragEnd={(result) => onDragEnd(result, columns, dispatch)}
        >
          {Object.entries(columns).map(([id, column]) => {
            return (
              <Grid item key={id} xs={12} sm={6} lg={3}>
                <Droppable droppableId={String(column.id)} key={column.id}>
                  {(provided) => (
                    <TaskColumn
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
  );
}

export default ColumnList;
