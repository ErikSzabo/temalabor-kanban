export interface Orderable {
  order: number;
}

export interface Task {
  id: string;
  title: string;
  description: string;
  deadline: Date;
  columnId: number;
}

export interface Tasks {
  tasks: Task[];
}

export interface TaskResponse {
  data: Tasks;
}

export interface Column extends Orderable {
  id: number;
  name: string;
  order: number;
}

export interface Columns {
  columns: Column[];
}

export interface ColumnResponse {
  data: Columns;
}

export interface LocalColumns {
  [key: string]: {
    id: number;
    name: string;
    order: number;
    tasks: Task[];
  };
}
