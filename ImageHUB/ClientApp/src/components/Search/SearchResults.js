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


const styles = theme => ({
    root: {
        width: '100%',
        maxWidth: 360,
        backgroundColor: theme.palette.background.paper,
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
        const index = nextProps.match.params.index|| "";
        this.props.requestSearchResults(index);
      }

    constructor(props) {
        super(props);
    }

    render() {
        const { classes } = this.props;
        return (
            <List className={classes.root}>
                {this.props.results.map(result => (
                    <div>
                        <ListItem alignItems="flex-start">
                            <ListItemAvatar>
                                <Avatar alt={result.userName} src="/static/images/avatar/1.jpg" />
                            </ListItemAvatar>
                            <ListItemText primary={result.userName}/>
                        </ListItem>
                        <Divider variant="inset" component="li" />
                    </div>
                ))}
            </List>
        );
    }
}

export default withRouter(connect(
    state => state.results,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(withStyles(styles)(SearchResults)));