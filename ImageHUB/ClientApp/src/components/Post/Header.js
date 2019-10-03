import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { Typography } from '@material-ui/core';

const useStyles = makeStyles({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
    /*header:{
        maxWidth: 400,
        width: '100%',
        position: 'absolute',
        top: 0
    },*/
    avatar: {
        margin: 10
    },
});

export default function Header(props) {
    const classes = useStyles();

    console.log(props);
    return (
        <div className={classes.header}>
            <div className={classes.container}>
                <Avatar alt={props.value.userName} src={props.value.avatar} className={classes.avatar} />
                <Typography variant="subtitle2">{props.value.userName}</Typography>
            </div>
        </div>
    );
}