import React, { ReactElement, useEffect, useReducer } from 'react';
import { DragDropContext, Droppable, DropResult } from 'react-beautiful-dnd';
import TaskColumn from './TaskColumn';
import { mockColumns, mockTasks } from '../lib/mock';
import { LocalColumns } from '../lib/interfaces';
import { reducer, Action } from '../lib/state';
import { TaskContext } from '../lib/taskContext';
import Grid from '@material-ui/core/Grid';

const onDragEnd = (
  result: DropResult,
  columns: LocalColumns,
  dispatch: React.Dispatch<Action>
) => {
  if (!result.destination) return;
  const { source, destination } = result;

  if (source.droppableId !== destination.droppableId) {
    const sourceColumn = columns[source.droppableId];
    const destColumn = columns[destination.droppableId];
    const sourceItems = [...sourceColumn.tasks];
    const destItems = [...destColumn.tasks];
    const [removed] = sourceItems.splice(source.index, 1);
    removed.columnId = Number(destination.droppableId);
    destItems.splice(destination.index, 0, removed);
    dispatch({
      type: 'init',
      payload: {
        columns: {
          ...columns,
          [source.droppableId]: {
            ...sourceColumn,
            tasks: sourceItems,
          },
          [destination.droppableId]: {
            ...destColumn,
            tasks: destItems,
          },
        },
      },
    });
  } else {
    const column = columns[source.droppableId];
    const copiedItems = [...column.tasks];
    const [removed] = copiedItems.splice(source.index, 1);
    copiedItems.splice(destination.index, 0, removed);
    dispatch({
      type: 'init',
      payload: {
        columns: {
          ...columns,
          [source.droppableId]: {
            ...column,
            tasks: copiedItems,
          },
        },
      },
    });
  }
};

function ColumnList(): ReactElement {
  const [{ columns }, dispatch] = useReducer(reducer, {
    columns: {},
  });

  useEffect(() => {
    const container: LocalColumns = {};
    mockColumns.data.columns.forEach((col) => {
      container[col.id] = {
        ...col,
        tasks: mockTasks.data.tasks.filter((task) => task.columnId === col.id),
      };
    });
    dispatch({ type: 'init', payload: { columns: container } });
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
                      tasks={column.tasks}
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
