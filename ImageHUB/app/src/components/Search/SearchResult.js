import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import { Link } from "react-router-dom";
import Typography from '@material-ui/core/Typography';
import axios from 'axios';

const useStyles = makeStyles(theme => ({
    link: {
        textDecoration: 'none',
        color: 'black',
        '&:focus, &:hover, &:visited, &:link, &:active': {
            textDecoration: 'none',
            color: 'black'
        }
    },
}));

export default function SearchResult(props) {
    return (
        <ListItem button component={Link} to={'/profile/' + result.userID}>
            <ListItemAvatar>
                <Avatar alt={result.userName} src={'https://graph.facebook.com/' + result.userID + '/picture?type=large'} />
            </ListItemAvatar>
            <ListItemText primary={result.userName} />
        </ListItem>
    );
}