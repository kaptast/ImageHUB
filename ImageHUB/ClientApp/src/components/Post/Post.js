import React from 'react';
import Card from '@material-ui/core/Card';
import Image from './Image';
import { makeStyles } from '@material-ui/core/styles';
import Header from './Header';

const useStyles = makeStyles(theme => ({
    card: {
        maxWidth: '100%',
        [theme.breakpoints.up('sm')]: {
            maxWidth: 800,
        },
    },
    header: {
        top: "0px",
        position: 'absolute'
    },
}));

export function PostWithHeader(props) {
    const classes = useStyles();
    if (props.value.show) {
        return (
            <Card className={classes.card}>
                <Image value={props.value} />
                <Header value={props.value} />
            </Card>
        );
    } else {
        return (
            <span />
        );
    }
}

export function PostWithoutHeader(props) {
    const classes = useStyles();
    if (props.value.show) {
        return (
            <Card className={classes.card}>
                <Image value={props.value} />
            </Card>
        );
    } else {
        return (
            <span />
        );
    }
}