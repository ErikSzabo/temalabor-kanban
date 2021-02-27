import { useContext } from 'react';
import { LocalColumns } from '../lib/interfaces';
import { Action } from '../lib/state';
import { TaskContext } from '../lib/taskContext';

interface Context {
  columns: LocalColumns;
  dispatch: React.Dispatch<Action>;
}

export const useTaskContext = (): Context | null => {
  const tasks = useContext(TaskContext);
  return tasks;
};
