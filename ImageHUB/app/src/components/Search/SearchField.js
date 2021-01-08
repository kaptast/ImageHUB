import React, { useState } from 'react';
import { fade, makeStyles } from '@material-ui/core/styles';
import SearchIcon from '@material-ui/icons/Search';
import InputBase from '@material-ui/core/InputBase';
import { useHistory } from "react-router-dom";

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
        width: '100%',
        [theme.breakpoints.up('sm')]: {
            marginLeft: theme.spacing(3),
            width: 'auto',
        },
    },
    searchIcon: {
        width: theme.spacing(7),
        height: '100%',
        position: 'absolute',
        pointerEvents: 'none',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    },
    inputRoot: {
        color: 'inherit',
    },
    inputInput: {
        padding: theme.spacing(1, 1, 1, 7),
        transition: theme.transitions.create('width'),
        width: '100%',
        [theme.breakpoints.up('md')]: {
            width: 120,
            '&:focus': {
                width: 300,
            },
        },
    },
}));

export default function SearchField(props) {
    const [searchInputValue, setSearchInputValue] = useState("");
    let history = useHistory();

    const catchReturn = event => {
        if (event.key === 'Enter') {
            event.preventDefault();
            history.push('/search/' + searchInputValue);
        }
    }

    const handleChange = event => {
        setSearchInputValue(event.target.value);
    }

    const classes = useStyles();

    return (
        <div className={classes.search}>
            <div className={classes.searchIcon}>
                <SearchIcon />
            </div>
            <InputBase
                placeholder="Search…"
                classes={{
                    root: classes.inputRoot,
                    input: classes.inputInput,
                }}
                onKeyPress={catchReturn}
                value={searchInputValue}
                onChange={handleChange}
            />
        </div>
    );
}