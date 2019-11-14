import React, { useState } from 'react';
import { fade, withStyles } from '@material-ui/core/styles';
import SearchIcon from '@material-ui/icons/Search';
import InputBase from '@material-ui/core/InputBase';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

const styles = theme => ({
    search: {
        position: 'relative',
        borderRadius: theme.shape.borderRadius,
        backgroundColor: fade('#bdbdbd', 0.15),
        '&:hover': {
            backgroundColor: fade('#bdbdbd', 0.25),
        },
        marginRight: theme.spacing(2),
        marginLeft: 0,
        width: '100%',
        [theme.breakpoints.up('sm')]: {
            marginLeft: theme.spacing(3),
            width: 'auto',
        },
    },
    searchIcon: {
        width: theme.spacing(7),
        height: '100%',
        position: 'absolute',
        pointerEvents: 'none',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    },
    inputRoot: {
        color: 'inherit',
    },
    inputInput: {
        padding: theme.spacing(1, 1, 1, 7),
        transition: theme.transitions.create('width'),
        width: '100%',
        [theme.breakpoints.up('md')]: {
            width: 120,
            '&:focus': {
                width: 300,
            },
        },
    },
});

class SearchField extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            searchInputValue: ""
        };
    }

    catchReturn(event) {
        if (event.key == 'Enter') {
            event.preventDefault();
            this.props.history.push('/search/'+ this.state.searchInputValue);
        }
    }

    handleChange(event) {
        this.setState({
            searchInputValue: event.target.value
        });
    }

    render() {
        const { classes } = this.props;
        return (
            <div className={classes.search}>
                <div className={classes.searchIcon}>
                    <SearchIcon />
                </div>
                <InputBase
                    placeholder="Search…"
                    classes={{
                        root: classes.inputRoot,
                        input: classes.inputInput,
                    }}
                    onKeyPress={this.catchReturn.bind(this)}
                    value={this.state.searchInputValue}
                    onChange={this.handleChange.bind(this)}
                />
            </div>
        );
    }
}
export default withRouter(connect()(withStyles(styles)(SearchField)));