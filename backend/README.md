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
GET /api/columns/{columnID}
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

Retrieves all of the cards in order which are in the specified column. Returns `404 Not Found` if column does not exists.

```http
GET /api/columns/{columnID}/cards
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
    "columnID": 1
  },
  {
    "id": 2,
    "title": "Card title",
    "description": "Card description",
    "deadline": "date string",
    "columnID": 1
  }
]
```

## Add a card to a column

Adds a card to the end of the column. Returns `404 Not Found` if the column does not exists.

```http
POST /api/columns/{columnID}/cards
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

```json
{
  "id": 5,
  "title": "Card title",
  "description": "Card description",
  "deadline": "date string",
  "columnID": 3
}
```

# Cards

## Get a card

Gets a card by its `id`. Returns `404 Not Found` if the card does not exists.

```http
GET /api/columns/cards/{cardID}
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
  "columnID": 1
}
```

## Delete a card

Deletes a card by its `id`. Returns `404 Not Found` if the card does not exists.

```http
DELETE /api/columns/cards/{cardID}
```

**Default response**

```
Status: 204 No Content
```

## Update a card

Updates the card with the new values. You can't change the `columnID` here. A change to the `columnID` must be performed through the `moves` endpoint.

```http
PUT /api/columns/cards/{cardID}
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

Moves a card after an another. `columnID` must be specified or it will return `400 Bad Request`. If `previousCardId` is not specified, then the card will be moved to the top of the column. In this example, the actual card will be moved to the column with the id of 1 after the card with the id of 11. If the previous card is in an another column than the `columnID` it will return `400 Bad Request`.

```http
PUT /api/columns/cards/{cardID}/moves
```

**body**:

```json
{
  "columnID": 1,
  "previousCardId": 11
}
```

**Default response**

```
Status: 204 No Content
```
