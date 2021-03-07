import React from 'react';
import { ColumnWithCards } from '../api';
import { Action } from './state';

interface Context {
  columns: ColumnWithCards;
  dispatch: React.Dispatch<Action>;
}

export const TaskContext = React.createContext<Context | null>(null);
