import React from 'react';
import { action } from '@storybook/addon-actions';
import SearchBar from '../components/SearchBar';
import { BrowserRouter } from 'react-router-dom';

export default {
    title: 'Searchbar',
};

export const searchbar = () => (
    <BrowserRouter>
        <SearchBar />
    </BrowserRouter>
);
