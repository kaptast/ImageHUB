import React from 'react';
import SearchBar from '../components/SearchBar';
import { BrowserRouter } from 'react-router-dom';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import red from '@material-ui/core/colors/red';

export default {
    title: 'Searchbar',
};

const theme = createMuiTheme({
    palette: {
        primary: {
            main: '#fff',
        },
        secondary: red,
        error: red,
        contrastThreshold: 3,
        tonalOffset: 0.2,
    },
});

export const searchbar = () => (
    <MuiThemeProvider theme={theme}>
        <BrowserRouter>
            <SearchBar />
        </BrowserRouter>
    </MuiThemeProvider>
);
