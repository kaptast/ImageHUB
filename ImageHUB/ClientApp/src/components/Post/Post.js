import React from 'react';
import Card from '@material-ui/core/Card';
import Image from './Image';
import { makeStyles } from '@material-ui/core/styles';
import Header from './Header';

const useStyles = makeStyles(theme => ({
    card: {
        maxWidth: 800
    },
    header: {
        top: "0px",
        position: 'absolute'
    },
}));

export default function Post(props) {
    const classes = useStyles();

    return (
        <Card className={classes.card}>
            <Image value={props.value} />
            <Header value={props.value} />
        </Card>
    );
}