import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../../store/Search';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Divider from '@material-ui/core/Divider';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import { Link } from "react-router-dom";
import Typography from '@material-ui/core/Typography';


const styles = theme => ({
    root: {
        minWidth: 500
    },
    inline: {
        display: 'inline',
    },
});


class SearchResults extends React.Component {
    componentWillMount() {
        const index = this.props.match.params.index || "";
        this.props.requestSearchResults(index);
    }

    componentWillReceiveProps(nextProps) {
        const index = nextProps.match.params.index || "";
        this.props.requestSearchResults(index);
    }

    constructor(props) {
        super(props);
    }

    render() {
        const { classes } = this.props;
        if (this.props.results.length === 0) {
            return (
                <Grid container direction="column" alignItems="center" justify="center">
                    <Grid item xs={12}>
                        <Typography color="inherit" variant="h5" noWrap1>
                            No results.
                        </Typography>
                    </Grid>
                </Grid>
            );
        } else {
            return (
                <Grid container direction="column" alignItems="center" justify="center">
                    <Grid item xs={12}>
                        <Paper className={classes.root}>
                            <List>
                                {this.props.results.map(result => (
                                    <Link to={'/profile/' + result.id}>
                                        <ListItem alignItems="flex-start">
                                            <ListItemAvatar>
                                                <Avatar alt={result.userName} src={'http://graph.facebook.com/' + result.id + '/picture?type=large'} />
                                            </ListItemAvatar>
                                            <ListItemText primary={result.userName} />
                                        </ListItem>
                                        <Divider variant="inset" component="li" />
                                    </Link>
                                ))}
                            </List>
                        </Paper>
                    </Grid>
                </Grid>
            );
        }
    }
}

export default withRouter(connect(
    state => state.results,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(withStyles(styles)(SearchResults)));