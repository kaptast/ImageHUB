import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { Typography, Button } from '@material-ui/core';
import Grid from '@material-ui/core/Grid';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import PersonAddDisabledIcon from '@material-ui/icons/PersonAddDisabled';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import axios from 'axios';

const useStyles = makeStyles(theme => ({
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
    buttons: {
        [theme.breakpoints.up('md')]: {
            minWidth: 216
        },
    },
    button: {
        [theme.breakpoints.up('md')]: {
            minWidth: 160
        },
    }
}));

function handleClick(id) {
    let url = `api/friend/AddFriend?id=${id}`;
    axios.post(url);
}

function acceptClick(id) {
    let url = `api/friend/AcceptFriend?id=${id}`;
    axios.post(url);
}

function deleteClick(id) {
    let url = `api/friend/DeleteFriend?id=${id}`;
    axios.delete(url);
}

export default function Header(props) {
    const classes = useStyles();

    const [friendStatus, setFriendStatus] = useState(0);
    const [showAddButton, setShowAddButton] = useState(false);

    useEffect(() => {
        setFriendStatus(props.profile.status);
        setShowAddButton(props.profile.showFriendButton);
    }, [props.profile.id])

    const clickedAddFriend = () => {
        handleClick(props.profile.userID);
        setFriendStatus(1);
        setShowAddButton(false);
    }

    const clickedAcceptFriend = () => {
        acceptClick(props.profile.userID);
        setFriendStatus(2);
        setShowAddButton(false);
    }

    const clickedDeleteFriend = () => {
        deleteClick(props.profile.userID);
        setFriendStatus(0);
        setShowAddButton(true);
    }

    console.log(props);
    console.log(friendStatus);

    let friends = props.profile.friends.length === 1 ? "friend" : "friends";
    let posts = props.profile.posts.length === 1 ? "post" : "posts";

    const avatar = 'https://graph.facebook.com/' + props.profile.userID + '/picture?type=large';

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
                        {showAddButton && (
                            <Grid item xs={4}>
                                <Button className={classes.buttons} variant="outlined" startIcon={<PersonAddIcon />} onClick={clickedAddFriend}>Add Friend</Button>
                            </Grid>
                        )}
                        {friendStatus === 1 && (
                            <Grid item xs={4}>
                                <ButtonGroup className={classes.buttons} variant="outlined">
                                    <Button className={classes.button} disabled>Pending</Button>
                                    <Button color="warning" onClick={clickedDeleteFriend} aria-label="delete friend" component="span">
                                        <PersonAddDisabledIcon />
                                    </Button>
                                </ButtonGroup>
                            </Grid>
                        )}
                        {friendStatus === 3 && (
                            <Grid item xs={4}>
                                <ButtonGroup className={classes.buttons} variant="outlined">
                                    <Button className={classes.button} onClick={clickedAcceptFriend} startIcon={<PersonAddIcon />}>Accept friend</Button>
                                    <Button color="warning" onClick={clickedDeleteFriend} aria-label="delete friend" component="span">
                                        <PersonAddDisabledIcon />
                                    </Button>
                                </ButtonGroup>
                            </Grid>
                        )}
                        {friendStatus === 2 && (
                            <Grid item xs={4}>
                                <Button className={classes.buttons} variant="outlined" color="warning" startIcon={<PersonAddDisabledIcon />} onClick={clickedDeleteFriend} aria-label="delete friend" component="span">
                                    Delete friend
                                </Button>
                            </Grid>
                        )}
                    </Grid>
                </Grid>

            </Grid>
        </div >
    );
}