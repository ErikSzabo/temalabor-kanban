import React from 'react';
import { LocalColumns } from './interfaces';
import { Action } from './state';

interface Context {
  columns: LocalColumns;
  dispatch: React.Dispatch<Action>;
}

export const TaskContext = React.createContext<Context | null>(null);
