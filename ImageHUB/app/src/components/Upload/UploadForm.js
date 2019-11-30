import React, { useState } from 'react';
import axios from 'axios';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import PublishIcon from '@material-ui/icons/Publish';

const useStyles = makeStyles(theme => ({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'center',
    },
    button: {
        margin: theme.spacing(1),
    },
}));

export default function UploadForm(props) {

    const [file, setFile] = useState(null);
    const [uploadDisabled, setUploadDisabled] = useState(true);

    const onChange = e => {
        setFile(e.target.files[0]);
        setUploadDisabled(false);
    }

    const onFormSubmit = event => {
        event.preventDefault()
        const formData = new FormData()
        formData.append("file", file)
        axios.post("api/post/upload", formData, {
            headers: { 'content-type': 'multipart/form-data' }
        }).then(props.parentCallback());
    }

    const classes = useStyles();
    return (
        <div>
            <form onSubmit={onFormSubmit} style={{ padding: 20 }}>
                <Grid container className={classes.container} spacing={2}>
                    <Grid item xs={12}>
                        <input 
                            type="file"
                            id="avatar" 
                            name="avatar"
                            accept="image/png, image/jpeg"
                            onChange={onChange}
                        />
                    </Grid>
                        <Grid item xs={12}>
                            <Button
                                type="submit"
                                variant="contained"
                                color="inherit"
                                disabled={uploadDisabled}
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