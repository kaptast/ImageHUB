import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { Typography } from '@material-ui/core';
import Actions from './Actions';

const useStyles = makeStyles({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
    actions: {
        marginLeft: 'auto'
    },
    avatar: {
        margin: 10
    },
});

export default function Header(props) {
    const classes = useStyles();

    const avatar = 'http://graph.facebook.com/'+ props.value.id +'/picture?type=large';

    return (
        <div className={classes.container}>
                <Avatar alt={props.value.userName} src={avatar} className={classes.avatar} />
                <Typography variant="subtitle2">{props.value.userName}</Typography>
                <div className={classes.actions}>
                    <Actions />
                </div>
        </div>
    );
}