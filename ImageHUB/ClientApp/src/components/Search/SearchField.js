import React, { useState, useEffect } from 'react';
import { fade, makeStyles } from '@material-ui/core/styles';
import SearchBar from 'material-ui-search-bar';
import axios from 'axios';


const useStyles = makeStyles(theme => ({
    search: {
        position: 'relative',
        borderRadius: theme.shape.borderRadius,
        backgroundColor: fade('#bdbdbd', 0.15),
        '&:hover': {
            backgroundColor: fade('#bdbdbd', 0.25),
        },
        marginRight: theme.spacing(2),
        marginLeft: 0,
        height: '40px',
        width: '100%',
        [theme.breakpoints.up('sm')]: {
            marginLeft: theme.spacing(3),
            width: 'auto',
            marginTop: '5px',
            marginBottom: '5px',
        },
        webkitBoxShadow: 'none',
        mozBoxShadow: 'none',
        boxShadow: 'none',
    },
}));

export default function SearchField() {
    const [value, setValue] = useState("");
    var results = [];

    useEffect(() => {
        axios.get("api/profile/GetAll")
            .then(res => {
                results = res.data;
                console.log(results);
            })
            .catch(err => {
                console.log(err);
                console.log("failed to get profiles");
            });
    });

    const classes = useStyles();
    return (
        <SearchBar className={classes.search}
            dataSource={results}
            value={value}
            onChange={(newValue) => setValue(newValue)}
            onRequestSearch={() => console.log('onRequestSearch')} />
    );
}