import { useContext } from 'react';
import { ColumnWithCards } from '../api';
import { Action } from '../lib/state';
import { TaskContext } from '../lib/taskContext';

interface Context {
  columns: ColumnWithCards;
  dispatch: React.Dispatch<Action>;
}

export const useTaskContext = (): Context | null => {
  const tasks = useContext(TaskContext);
  return tasks;
};
