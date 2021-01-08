import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import { Typography } from '@material-ui/core';
import { Link } from "react-router-dom";
import { SmallChipsArray } from './Tags';

const useStyles = makeStyles({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
    avatar: {
        margin: 10
    },
    actions: {
        marginLeft: 'auto'
    },
    link: {
        textDecoration: 'none',
        color: 'black',
        '&:focus, &:hover, &:visited, &:link, &:active': {
            textDecoration: 'none',
            color: 'black'
        }
    },
});

export default function Header(props) {
    const classes = useStyles();
    const avatar = 'https://graph.facebook.com/' + props.value.ownerDTO.id + '/picture?type=large';
    const profileLink = "/profile/" + props.value.ownerDTO.id;

    return (
        <div className={classes.container}>
            <Avatar alt={props.value.userName} src={avatar} className={classes.avatar} />
            <Typography variant="subtitle2"><Link to={profileLink} className={classes.link}>{props.value.ownerDTO.userName}</Link></Typography>
            <div className={classes.actions}>
                <SmallChipsArray tags={props.value.postTags} />
            </div>
        </div>
    );
}