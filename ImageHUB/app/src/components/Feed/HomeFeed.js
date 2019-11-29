import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import { PostWithHeader } from '../Post/Post';
import Skeleton from '@material-ui/lab/Skeleton';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        padding: 5
    },
}));

export default function HomeFeed(props) {
    const classes = useStyles();

    return (
        <div className={classes.root}>
            <Grid container direction="column" alignItems="center" justify="center" spacing={3}>
                {!props.isLoading ? (
                    props.posts.map((post, key) => (
                        <Grid item xs={12} key={key}>
                            <PostWithHeader value={post} />
                        </Grid>
                    ))
                    ) : (
                        <Grid item xs={12}>
                            <Skeleton variant="rect" width={800} height={800} />
                        </Grid>
                    )}
            </Grid>
        </div>

    );
}