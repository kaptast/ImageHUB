import React, { Component } from 'react';
import PropTypes from 'prop-types';
import axios from 'axios';
import Button from '@material-ui/core/Button';
import { withStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import PublishIcon from '@material-ui/icons/Publish';
import { withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

const styles = theme => ({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
    button: {
        margin: theme.spacing(1),
      },
});

class UploadForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            file: {}
        };
    }

    onChange(files) {
        this.setState({
            file: files[0]
        });
    }

    onFormSubmit(event) {
        event.preventDefault();
        const formData = new FormData();
        formData.append("file", this.state.file);
        axios.post("api/images/upload", formData, {
            headers: { 'content-type': 'multipart/form-data' }
        }).then(this.props.parentCallback()).then(this.props.history.push('/'));
    }

    render() {
        const { classes } = this.props;
        return (
            <div>
                <form onSubmit={this.onFormSubmit.bind(this)} style={{ padding: 20 }}>
                    <Grid container className={classes.container} spacing={2}>
                        <Grid item xs={12}>
                            
                        </Grid>
                        <Grid item xs={12}>
                            <Button
                                type="submit"
                                variant="contained"
                                color="inherit"
                                className={classes.button}
                                startIcon={<PublishIcon />}
                            >
                                Upload
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        )
    }
}

UploadForm.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withRouter(connect()(withStyles(styles)(UploadForm)));