import React from 'react';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import { PostWithoutHeader } from '../Post/Post';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import Skeleton from '@material-ui/lab/Skeleton';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        padding: 5
    },
    skeleton: {
        width: '100%',
        height: '100%',
        maxHeight: '400',
        maxWidth: '400'
    }
}));

export default function ProfileFeed(props) {
    const classes = useStyles();
    const theme = useTheme();

    const style = useMediaQuery(theme.breakpoints.up('md')) ? "row" : "column";
    const itemSize = useMediaQuery(theme.breakpoints.up('md')) ? 4 : 12;
    const remainder = props.posts.length % 3;
    const emptyCount = (remainder === 0) ? 0 : 3 - remainder;

    const showedArray = [...props.posts];

    for (let i = 0; i < emptyCount; i++) {
        showedArray.push(
            {
                show: false
            }
        );
    }

    return (
        <div className={classes.root}>
            <Grid container direction={style} alignItems="center" justify="center" spacing={3}>
                {!props.isLoading ? (
                    showedArray.map((post, key) => (
                        <Grid item key={key} xs={itemSize}>
                            <PostWithoutHeader value={post} isLoading={props.isLoading} />
                        </Grid>
                    ))
                ) : (
                        <div>
                            <Grid item xs={itemSize}>
                                <Skeleton variant="rect" className={classes.skeleton} />
                            </Grid>
                            <Grid item xs={itemSize}>
                                <Skeleton variant="rect" className={classes.skeleton} />
                            </Grid>
                            <Grid item xs={itemSize}>
                                <Skeleton variant="rect" className={classes.skeleton} />
                            </Grid>
                        </div>
                    )}
            </Grid>
        </div>

    );
}
