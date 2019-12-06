import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import Avatar from '@material-ui/core/Avatar';
import { Link } from "react-router-dom";
import ButtonGroup from '@material-ui/core/ButtonGroup';
import { Button } from '@material-ui/core';
import PersonAddDisabledIcon from '@material-ui/icons/PersonAddDisabled';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import axios from 'axios';

const useStyles = makeStyles(theme => ({
    link: {
        textDecoration: 'none',
        color: 'black',
        '&:focus, &:hover, &:visited, &:link, &:active': {
            textDecoration: 'none',
            color: 'black'
        }
    },
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
}));

function acceptClick(id) {
    let url = `api/friend/AcceptFriend?id=${id}`;
    axios.post(url);
}

function deleteClick(id) {
    let url = `api/friend/DeleteFriend?id=${id}`;
    axios.delete(url);
}

export default function SearchResult(props) {
    const classes = useStyles();
    const [friendStatus, setFriendStatus] = useState(0);
    const [showAddButton, setShowAddButton] = useState(false);

    useEffect(() => {
        setFriendStatus(props.profile.status);
        setShowAddButton(props.profile.showFriendButton);
    }, [props.profile.id])

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

    return (
        <ListItem button component={Link} to={'/profile/' + props.profile.userID}>
            <ListItemAvatar>
                <Avatar alt={props.profile.userName} src={'https://graph.facebook.com/' + props.profile.userID + '/picture?type=large'} />
            </ListItemAvatar>
            <ListItemText primary={props.profile.userName} />
            <ListItemSecondaryAction>
                {friendStatus === 3 && (
                    <ButtonGroup className={classes.buttons} variant="outlined">
                        <Button className={classes.button} onClick={clickedAcceptFriend} startIcon={<PersonAddIcon />}>Accept friend</Button>
                        <Button color="warning" onClick={clickedDeleteFriend} aria-label="delete friend" component="span">
                            <PersonAddDisabledIcon />
                        </Button>
                    </ButtonGroup>
                )}
                {friendStatus === 1 && (
                    <ButtonGroup className={classes.buttons} variant="outlined">
                        <Button className={classes.button} disabled>Pending</Button>
                        <Button color="warning" onClick={clickedDeleteFriend} aria-label="delete friend" component="span">
                            <PersonAddDisabledIcon />
                        </Button>
                    </ButtonGroup>
                )}
            </ListItemSecondaryAction>
        </ListItem>
    );
}