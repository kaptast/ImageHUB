import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { Typography } from '@material-ui/core';
import Grid from '@material-ui/core/Grid';

const useStyles = makeStyles({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
    content: {
        height: 35
    },
    avatar: {
        margin: 10,
        width: 100,
        height: 100
    },
});

export default function Header(props) {
    const classes = useStyles();

    let friends = props.value.friends == 1 ? "friend" : "friends";
    let posts = props.value.posts.length == 1 ? "post" : "posts";

    return (
        <div className={classes.container}>
            <Grid container spacing={2}>
                <Grid item>
                    <Avatar alt={props.value.userName} src={props.value.avatar} className={classes.avatar} />
                </Grid>
                <Grid item xs={4} className={classes.container}>
                    <Grid container>
                        <Grid item xs>
                            <Typography variant="h5">{props.value.userName}</Typography>
                        </Grid>
                        <Grid item container spacing={1}>
                            <Grid item>
                                <Typography variant="subtitle1"><strong>{props.value.friends}</strong> {friends}</Typography>
                            </Grid>
                            <Grid item>
                                <Typography variant="subtitle1"><strong>{props.value.posts.length}</strong> {posts}</Typography>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </div >
    );
}