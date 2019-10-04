import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Counter';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Post from './Post/Post';
import useMediaQuery from '@material-ui/core/useMediaQuery';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        padding: 5
    },
}));

export default function ProfileFeed(props) {
    const classes = useStyles();

    const theme = useTheme();
    const style = useMediaQuery(theme.breakpoints.up('sm')) ? "row" : "column";
    const itemSize = useMediaQuery(theme.breakpoints.up('sm')) ? 4 : 12;

    console.log(props);

    return (
        <div className={classes.root}>
            <Grid container direction={style} alignItems="center" justify="center" spacing={3}>
                {props.posts.map(post => (
                    <Grid item key={post.title} xs={itemSize}>
                        <Post value={post} />
                    </Grid>
                ))}
            </Grid>
        </div>

    );
}
