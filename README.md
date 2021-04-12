# About the project

This is a very simple single user kanban board manager web application. You have 4 foxed columns (todo, in progress, done, postponed). You can create and edit or remove cards in every column. A card can have a title (or name), a description and a deadline. You can drag and drop cards between columns as you like.

Built with ASP.NET, EntityFrameworkCore, MSSQL and React.

# Frontend

## Installation Requirements

- node version 12+
- npm version 6+
- other dependencies are in the package.json and npm handles them automatically.

If you have all these, navigate to the `frontend` directory and then run:

```
npm install
```

After that, to start the React frontend application:

```
npm start
```

## Project structure

Frontend is located at the frontend directory.

```bash
├───components
│ ├───customUI
│ └───dialogs
├───hooks
└───lib
```

In the components directory, you can find the 3 main components: KanbanBoard, KanbanColumn and KanbanTask. A KanbanBoard has KanbanColumns and a KanbanColumn has KanbanCards.

In the dialog folder, we have a confirmation dialog (AreYouSureDialog) for card deletion, an ErrorDialog, which is actually a SnackBar to show the user if the server is encountered an error and a Card dialog, which is responsible for adding and editing cards.

In the customUI directory, there is only a custom Styled materialUI badge.

For hooks, there is only one, the useTaskContext hook, which uses the useContext hook to provide Column and Card data for every component.

In the lib directory we can find everything else, like api related files, state managing files, and the actual TaskContext. An interfaces.ts file also located here. The file names speaks for themselves.

Don't search for style files. Only inline styling was used on top of materialUI.

## How it works

You will see a board when you open the app in the browser, which is essentially 4 columns right now. Todo, in progress, done and postponed. In the columns you can see your cards (tasks). If you add, edit or remove a card, the changes will only be visible after the server responded. (these are quite fast operations). On the other hand, if you move a card, you will see the changes immediately. If the server fails, then a previous state will be restored. (Note: if the server fails, and you made other successfull changes before you recieve the error message, it's possible that you will be in an outdated state after the rollback happens. In that case, consider refreshing the page)

# Backend

## Installation Requirements

- .NET SDK 5.0 with IIS Express
- Microsoft SQL Server localdb running
- Frameworks
  - ASP.NET Core
  - .NET Core
- Nuget packages installed
  - Microsoft.EntityFrameworkCore.SqlServer 5.0.3
  - Microsoft.EntityFrameworkCore.Design 5.0.3
- Visual Studio 2019+

To start the backend, setup the kanban project as the startup project and hit the green arrow in Visual Studio.
