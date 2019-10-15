import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Counter';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import { PostWithoutHeader } from '../components/Post/Post';
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

    const style = useMediaQuery(theme.breakpoints.up('md')) ? "row" : "column";
    const itemSize = useMediaQuery(theme.breakpoints.up('md')) ? 4 : 12;
    const remainder = props.posts.length % 3;
    const emptyCount = (remainder == 0) ? 0 : 3 - remainder;

    console.log(props);

    for (let i = 0; i < emptyCount; i++) {
        props.posts.push(
            {
                show: false
            }
        );
    }
    
    return (
        <div className={classes.root}>
            <Grid container direction={style} alignItems="center" justify="center" spacing={3}>
                {props.posts.map(post => (
                    <Grid item key={post.title} xs={itemSize}>
                        <PostWithoutHeader value={post} />
                    </Grid>
                ))}
            </Grid>
        </div>

    );
}
