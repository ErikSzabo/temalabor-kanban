# Columns

## List columns

Retrieves all of the columns in order.

```http
GET /api/columns
```

**Default response**

```
Status: 200 OK
```

```json
[
  {
    "id": 1,
    "name": "Todo"
  },
  {
    "id": 2,
    "name": "In Progress"
  }
]
```

## Get a column

Gets a column by its `id`. Returns `404 Not Found` status if the column does not exists.

```http
GET /api/columns/{column_id}
```

**Default response**

```
Status: 200 OK
```

```json
{
  "id": 1,
  "name": "Todo"
}
```

## Get column cards

Retrieves all of the cards in order which are in the specified column. Returns `400 Bad Request` if column does not exists.

```http
GET /api/columns/{column_id}/cards
```

**Default response**

```
Status: 200 OK
```

```json
[
  {
    "id": 1,
    "title": "Card title",
    "description": "Card description",
    "deadline": "date string",
    "column_id": 1
  },
  {
    "id": 2,
    "title": "Card title",
    "description": "Card description",
    "deadline": "date string",
    "column_id": 1
  }
]
```

## Add a card to a column

Adds a card to the end of the column. Returns `400 Bad Request` if the column does not exists.

```http
POST /api/columns/{column_id}/cards
```

**body**:

```json
{
  "title": "Card title",
  "description": "Card description",
  "deadline": "date string"
}
```

**Default response**

```
Status: 201 Created
```

## Move a card to the top of the column

Moves a a card to the top of the column. If the column or the card does not exists it will return `400 Bad Request`.

```http
PUT /api/columns/{column_id}/cards/{card_id}/movetop
```

**Default response**

```
Status: 204 No content
```

# Cards

## Get all cards

Lists all the existing cards.

```http
GET /api/cards
```

**Default response**

```
Status: 200 OK
```

```json
[
  {
    "id": 1,
    "title": "Card title",
    "description": "Card description",
    "deadline": "date string",
    "column_id": 1
  },
  {
    "id": 2,
    "title": "Card title",
    "description": "Card description",
    "deadline": "date string",
    "column_id": 1
  }
]
```

## Get a card

Gets a card by its `id`. Returns `˙404 Not Found` if the card does not exists.

```http
GET /api/cards/{card_id}
```

**Default response**

```
Status: 200 OK
```

```json
{
  "id": 1,
  "title": "Card title",
  "description": "Card description",
  "deadline": "date string",
  "column_id": 1
}
```

## Delete a card

Deletes a card by its `id`. Returns `400 Bad Request` if the card does not exists.

```http
DELETE /api/cards/{card_id}
```

**Default response**

```
Status: 204 No Content
```

## Update a card

Updates the card with the new values.

```http
PUT /api/cards/{card_id}
```

**body**:

```json
{
  "title": "your title",
  "description": "your description",
  "deadline": "your deadline"
}
```

**Default response**

```
Status: 204 No Content
```

## Move a card

Moves a card after an another. Both ids must be specified or it will return `400 Bad Request`.

```http
PUT /api/cards/{card_id}/moveafter/{previous_id}
```

**Default response**

```
Status: 204 No Content
```
