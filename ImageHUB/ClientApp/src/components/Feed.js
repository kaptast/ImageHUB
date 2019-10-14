import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import { PostWithHeader } from './Post/Post';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        padding: 5
    },
}));

export default function Feed(props) {
    const classes = useStyles();

    console.log("Feed posts");
    console.log(props.posts);

    return (
        <div className={classes.root}>
            <Grid container direction="column" alignItems="center" justify="center" spacing={3}>
                {props.posts.map(post => (
                    <Grid item xs={12}>
                        <PostWithHeader value={post} />
                    </Grid>
                ))}
            </Grid>
        </div>

    );
}