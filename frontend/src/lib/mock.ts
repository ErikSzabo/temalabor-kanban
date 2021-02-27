import { v4 as uuidv4 } from 'uuid';
import { ColumnResponse, TaskResponse } from './interfaces';

export const mockTasks: TaskResponse = {
  data: {
    tasks: [
      {
        id: uuidv4(),
        title: 'Sample task 2',
        description: 'This is a very basic sample task description',
        deadline: new Date(),
        columnId: 1,
      },
      {
        id: uuidv4(),
        title: 'Sample task 1',
        description: 'This is a very basic sample task description',
        deadline: new Date(),
        columnId: 1,
      },
      {
        id: uuidv4(),
        title: 'Sample task 3',
        description: 'This is a very basic sample task description',
        deadline: new Date(),
        columnId: 2,
      },
    ],
  },
};

export const mockColumns: ColumnResponse = {
  data: {
    columns: [
      {
        id: 1,
        name: 'Todo',
        order: 1,
      },
      {
        id: 2,
        name: 'In Progress',
        order: 2,
      },
      {
        id: 3,
        name: 'Done',
        order: 3,
      },
      {
        id: 4,
        name: 'Postponed',
        order: 4,
      },
    ],
  },
};
