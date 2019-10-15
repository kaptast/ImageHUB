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

    console.log(props);

    let friends = "friends";//props.value.friends == 1 ? "friend" : "friends";
    let posts = props.profile.posts.length == 1 ? "post" : "posts";

    const avatar = 'http://graph.facebook.com/'+ props.profile.avatar +'/picture?type=large';

    return (
        <div className={classes.container}>
            <Grid container spacing={2}>
                <Grid item>
                    <Avatar alt={props.profile.userName} src={avatar} className={classes.avatar} />
                </Grid>
                <Grid item xs={4} className={classes.container}>
                    <Grid container>
                        <Grid item xs>
                            <Typography variant="h5">{props.profile.userName}</Typography>
                        </Grid>
                        <Grid item container spacing={1}>
                            <Grid item>
                                <Typography variant="subtitle1"><strong>{0/*props.value.friends*/}</strong> {friends}</Typography>
                            </Grid>
                            <Grid item>
                                <Typography variant="subtitle1"><strong>{props.profile.posts.length}</strong> {posts}</Typography>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </div >
    );
}