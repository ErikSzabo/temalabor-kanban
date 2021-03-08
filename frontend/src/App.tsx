import React from 'react';
import KanbanBoard from './components/KanbanBoard';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import blue from '@material-ui/core/colors/blue';
import './App.css';
import Container from '@material-ui/core/Container';

const THEME = createMuiTheme({
  palette: {
    type: 'dark',
    primary: blue,
    background: {
      paper: '#222222',
    },
  },
});

const App: React.FC<unknown> = () => {
  return (
    <MuiThemeProvider theme={THEME}>
      <Container maxWidth="xl">
        <KanbanBoard />
      </Container>
    </MuiThemeProvider>
  );
};

export default App;
