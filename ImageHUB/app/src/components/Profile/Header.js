import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { Typography, Button } from '@material-ui/core';
import Grid from '@material-ui/core/Grid';
import axios from 'axios';

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

function handleClick(id) {
    let url = `api/friend/AddFriend?id=${id}`;
    axios.post(url);
}

function acceptClick(id) {
    let url = `api/friend/AcceptFriend?id=${id}`;
    axios.post(url);
}

export default function Header(props) {
    const classes = useStyles();

    console.log(props);

    let friends = props.profile.friends.length === 1 ? "friend" : "friends";
    let posts = props.profile.posts.length === 1 ? "post" : "posts";

    const avatar = 'https://graph.facebook.com/' + props.profile.avatar + '/picture?type=large';

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
                                <Typography variant="subtitle1"><strong>{props.profile.friends.length}</strong> {friends}</Typography>
                            </Grid>
                            <Grid item>
                                <Typography variant="subtitle1"><strong>{props.profile.posts.length}</strong> {posts}</Typography>
                            </Grid>
                        </Grid>
                        {props.profile.showFriendButton && (
                            <Grid item xs={4}>
                                <Button variant="outlined" onClick={() => handleClick(props.profile.id)}>Add Friend</Button>
                            </Grid>
                        )}
                        {props.profile.status === 1 && (
                            <Grid item xs={4}>
                                <Button variant="outlined" disabled>Pending</Button>
                            </Grid>
                        )}
                        {props.profile.status === 3 && (
                            <Grid item xs={4}>
                                <Button variant="outlined" onClick={() => acceptClick(props.profile.id)}>Accept friend</Button>
                            </Grid>
                        )}
                    </Grid>
                </Grid>

            </Grid>
        </div >
    );
}