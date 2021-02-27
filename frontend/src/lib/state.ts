import { LocalColumns, Task } from './interfaces';

export interface State {
  columns: LocalColumns;
}

export type Action =
  | { type: 'add-new-task'; payload: Task }
  | { type: 'delete-task'; payload: Task }
  | { type: 'edit-task'; payload: { old: Task; new: Task } }
  | { type: 'init'; payload: { columns: LocalColumns } };

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

function addTask(state: State, task: Task): State {
  const newColumns = { ...state.columns };
  newColumns[task.columnId].tasks.push(task);
  return { columns: newColumns };
}

function deleteTask(state: State, task: Task): State {
  const newColumns = { ...state.columns };
  const newTasks = newColumns[task.columnId].tasks;
  newTasks.splice(newTasks.indexOf(task), 1);
  newColumns[task.columnId].tasks = newTasks;
  return { columns: newColumns };
}

function editTask(state: State, oldTask: Task, newTask: Task): State {
  const newColumns = { ...state.columns };
  const newTasks = newColumns[oldTask.columnId].tasks;
  newTasks.splice(newTasks.indexOf(oldTask), 1, newTask);
  newColumns[oldTask.columnId].tasks = newTasks;
  return { columns: newColumns };
}
