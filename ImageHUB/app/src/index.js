import './index.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import red from '@material-ui/core/colors/red';
import gray from '@material-ui/core/colors/blueGrey';
import App from './App';
import { SnackbarProvider } from 'notistack';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const theme = createMuiTheme({
  palette: {
    primary: {
      main: '#fff',
    },
    secondary: gray,
    error: red,
    contrastThreshold: 3,
    tonalOffset: 0.2,
  },
  typography: {
    fontFamily: [
      '-apple-system',
      'BlinkMacSystemFont',
      'Roboto',
      '"Segoe UI"',
      '"Helvetica Neue"',
      'Arial',
      'sans-serif',
      '"Apple Color Emoji"',
      '"Segoe UI Emoji"',
      '"Segoe UI Symbol"',
    ].join(','),
  },
});

ReactDOM.render(
  <MuiThemeProvider theme={theme}>
    <SnackbarProvider maxSnack={3}>
      <BrowserRouter basename={baseUrl}>
        <App />
      </BrowserRouter>
    </SnackbarProvider>
  </MuiThemeProvider>,
  rootElement);

