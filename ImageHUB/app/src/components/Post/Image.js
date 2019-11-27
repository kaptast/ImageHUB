import React from 'react';
import CardMedia from '@material-ui/core/CardMedia';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import useMediaQuery from '@material-ui/core/useMediaQuery';

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
    var imagePath = process.env.PUBLIC_URL + props.value.image;
    const classes = useStyles();

    return (
        <div className={classes.crop}>
            <CardMedia
                image={imagePath}
                title={props.value.userName}
                alt={props.value.userName}
                component="img"
                className={classes.img}
            />
        </div>
    );
}

export function SmallImage(props) {
    var imagePath = process.env.PUBLIC_URL + props.value.image;
    const classes = useStyles();

    return (
        <div className={classes.crop}>
            <CardMedia
                image={imagePath}
                title={props.value.userName}
                alt={props.value.userName}
                component="img"
                className={classes.smallImg}
            />
        </div>
    );
}