import React from 'react';
import { CardActions, IconButton } from '@material-ui/core';
import ThumbUp from '@material-ui/icons/ThumbUp';
import ThumbDown from '@material-ui/icons/ThumbDown';

export default function Actions(props) {
    return (
        <CardActions disableSpacing>
            <IconButton aria-label="upvote">
                <ThumbUp />
            </IconButton>
            <IconButton aria-label="downvote">
                <ThumbDown />
            </IconButton>
        </CardActions>
    );
}