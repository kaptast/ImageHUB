import React from 'react';
import CardMedia from '@material-ui/core/CardMedia';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
    img: {
        width: '100%',
        height: '100%',
        [theme.breakpoints.up('md')]: {
            width: 800,
            height: 800,
        },
    },
    smallImg: {
        width: '100%',
        height: '100%',
        [theme.breakpoints.up('md')]: {
            width: 800,
            maxHeight: 500,
        },
    },
    crop: {
        overflow: 'hidden',
    },
}));

export function Image(props) {
    const classes = useStyles();

    return (
        <div className={classes.crop}>
            <CardMedia
                image={"data:image/jpeg;base64," + props.value.image}
                title={props.value.userName}
                alt={props.value.userName}
                component="img"
                className={classes.img}
            />
        </div>
    );
}

export function SmallImage(props) {
    const classes = useStyles();

    return (
        <div className={classes.crop}>
            <CardMedia
                image={"data:image/jpeg;base64," + props.value.image}
                title={props.value.userName}
                alt={props.value.userName}
                component="img"
                className={classes.smallImg}
            />
        </div>
    );
}