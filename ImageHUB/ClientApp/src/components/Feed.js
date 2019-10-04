import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Post from './Post/Post';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        padding: 5
    },
}));

export default function Feed(props) {
    const classes = useStyles();

    console.log(props);

    return (
        <div className={classes.root}>
            <Grid container direction="column" alignItems="center" justify="center" spacing={3}>
                {props.posts.map(post => (
                    <Grid item key={post.title} xs={12}>
                        <Post value={post} />
                    </Grid>
                ))}
            </Grid>
        </div>

    );
}