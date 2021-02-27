import { Orderable } from './interfaces';

export const sort = (a: Orderable, b: Orderable) =>
  a.order < b.order ? -1 : 1;
