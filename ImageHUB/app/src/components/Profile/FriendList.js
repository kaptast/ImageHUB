import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import Typography from '@material-ui/core/Typography';
import axios from 'axios';
import SearchResult from '../Search/SearchResult';
import Dialog from '@material-ui/core/Dialog';

const useStyles = makeStyles(theme => ({
    root: {
        minWidth: 500,
        padding: 10
    },
    inline: {
        display: 'inline',
    },
    link: {
        textDecoration: 'none',
        color: 'black',
        '&:focus, &:hover, &:visited, &:link, &:active': {
            textDecoration: 'none',
            color: 'black'
        }
    },
}));

function FriendList(props) {
    const [waitingFriends, setWaitingFriends] = useState([]);
    const classes = useStyles();
    useEffect(() => {
        const url = `api/friend/WaitingFriends?id=${props.profile.userID}`;
        axios.get(url)
            .then(res => {
                var data = res.data;
                for (var i = 0; i < data.length; i++){
                    data[i].status = 3;
                }
                setWaitingFriends(data);
            })
            .catch(err => {
                console.log(err);
                console.log("Failed to get waiting friends");
            });
    }, []);

    return (
        <div className={classes.root} onClick={props.parentCallback}>
            {waitingFriends.length > 0 &&
                <>
                    <Typography variant="h6">Pending</Typography>
                    <List component="nav">
                        {waitingFriends.map(friend => (
                            <SearchResult profile={friend} />
                        ))}
                    </List>
                    <Divider />
                </>
            }
            <Typography variant="h6">Friends</Typography>
            <List component="nav">
                {props.profile.friends.map(friend => (
                    <SearchResult profile={friend} />
                ))}
            </List>
        </div>
    );
}

function FriendsDialog(props) {
    const { onClose, open } = props;
    
    const handleClose = () => {
        onClose();
    };

    return (
        <Dialog onClose={handleClose} open={open}>
            <FriendList profile={props.profile} parentCallback={handleClose}/>
        </Dialog>
    );
}

export default function FriendButton(props) {
    const [open, setOpen] = useState(false);
    
    let friends = props.profile.friends.length === 1 ? "friend" : "friends";

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = value => {
        setOpen(false);
    };

    return (
        <div>
            <Typography variant="subtitle1" onClick={handleClickOpen}><strong>{props.profile.friends.length}</strong> {friends}</Typography>
            <FriendsDialog open={open} onClose={handleClose} profile={props.profile} />
        </div>
    );
}