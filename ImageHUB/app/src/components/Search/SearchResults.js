import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import axios from 'axios';
import SearchResult from './SearchResult';

const useStyles = makeStyles(theme => ({
    root: {
        minWidth: 500
    },
    inline: {
        display: 'inline',
    },
    link: {
        textDecoration: 'none',
        color: 'black',
        '&:focus, &:hover, &:visited, &:link, &:active': {
            textDecoration: 'none',
            color: 'black'
        }
    },
}));

export default function SearchResults(props) {

    const [results, setResults] = useState([]);

    const index = props.match.params.index || "";
    const classes = useStyles();
    const haveResults = !(results.length === 0);

    useEffect(() =>{
        const url = `api/profile/GetAllByName?name=${index}`;
        axios.get(url)
            .then(res => {
                setResults(res.data);
            });
    }, [index]);

    return (
        <div>
            {!haveResults &&
                <Grid container direction="column" alignItems="center" justify="center">
                    <Grid item xs={12}>
                        <Typography color="inherit" variant="h5" noWrap1>
                            No results.
                        </Typography>
                    </Grid>
                </Grid>
            }
            {haveResults &&
                <Grid container direction="column" alignItems="center" justify="center">
                    <Grid item xs={12}>
                        <Paper className={classes.root}>
                            <List component="nav">
                                {results.map(result => (
                                    <SearchResult profile={result} />
                                ))}
                            </List>
                        </Paper>
                    </Grid>
                </Grid>
            }
        </div>
    );
}