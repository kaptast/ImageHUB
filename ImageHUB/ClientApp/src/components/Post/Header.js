import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { Typography } from '@material-ui/core';
import Actions from './Actions';
import { Link } from "react-router-dom";

const useStyles = makeStyles({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
    actions: {
        marginLeft: 'auto'
    },
    avatar: {
        margin: 10
    },
    link: {
        textDecoration: 'none',
        color: 'black',
        '&:focus, &:hover, &:visited, &:link, &:active':{
            textDecoration: 'none',
            color: 'black'
        }
    },
});

export default function Header(props) {
    const classes = useStyles();
    const avatar = 'http://graph.facebook.com/'+ props.value.owner.id +'/picture?type=large';
    const profileLink = "/profile/" + props.value.owner.id;
    return (
        <div className={classes.container}>
                <Avatar alt={props.value.userName} src={avatar} className={classes.avatar} />
                <Typography variant="subtitle2"><Link to={profileLink} className={classes.link}>{props.value.owner.userName}</Link></Typography>
                <div className={classes.actions}>
                    <Actions />
                </div>
        </div>
    );
}