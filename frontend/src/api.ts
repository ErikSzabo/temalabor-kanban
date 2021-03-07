const apiBaseUrl = process.env.API_URL || 'https://localhost:44321';

export interface Column {
  id: number;
  name: string;
}

export interface KanbanCard {
  id: number;
  columnID: number;
  title: string;
  description: string;
  deadline: Date;
}

export interface ColumnWithCards {
  [key: string]: {
    id: number;
    name: string;
    order: number;
    cards: KanbanCard[];
  };
}

export interface CardCreate {
  columnID: number;
  title: string;
  description: string;
  deadline: Date;
}

export async function getColumnsWithCards(): Promise<ColumnWithCards> {
  const response = await fetch(`${apiBaseUrl}/api/columns`);
  handleStatusError(response);
  const columns: Column[] = await response.json();
  const columnsWithCards: ColumnWithCards = {};
  for (let i = 0; i < columns.length; i++) {
    const column = columns[i];
    const response = await fetch(
      `${apiBaseUrl}/api/columns/${column.id}/cards`
    );
    const cards: KanbanCard[] = await response.json();
    columnsWithCards[column.id] = { ...column, cards, order: i };
  }
  return columnsWithCards;
}

export async function createCard(card: CardCreate): Promise<KanbanCard> {
  const response = await fetch(
    `${apiBaseUrl}/api/columns/${card.columnID}/cards`,
    {
      headers: {
        'content-type': 'application/json',
        accepts: 'application/json',
      },
      method: 'POST',
      body: JSON.stringify({
        title: card.title,
        description: card.title,
        deadline: card.deadline,
      }),
    }
  );
  handleStatusError(response);
  const createdCard: KanbanCard = await response.json();
  createdCard.deadline = new Date(createdCard.deadline);
  return createdCard;
}

export function updateCard(card: KanbanCard): Promise<Response> {
  return fetch(`${apiBaseUrl}/api/columns/cards/${card.id}`, {
    headers: {
      'content-type': 'application/json',
    },
    method: 'PUT',
    body: JSON.stringify(card),
  }).then(handleStatusError);
}

export function deleteCard(cardID: number): Promise<Response> {
  return fetch(`${apiBaseUrl}/api/columns/cards/${cardID}`, {
    method: 'DELETE',
  }).then(handleStatusError);
}

export function moveCard(
  cardID: number,
  previousCardId: number | null,
  columnID: number
): Promise<Response> {
  return fetch(`${apiBaseUrl}/api/columns/cards/${cardID}/moves`, {
    headers: {
      'content-type': 'application/json',
    },
    method: 'PUT',
    body: JSON.stringify({
      columnID,
      previousCardId,
    }),
  }).then(handleStatusError);
}

function handleStatusError(res: Response): Response {
  if (res.status >= 400 && res.status <= 600) throw new Error();
  return res;
}
