import React, { useState } from 'react';
import axios from 'axios';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import PublishIcon from '@material-ui/icons/Publish';
import Skeleton from '@material-ui/lab/Skeleton';
import TextField from '@material-ui/core/TextField';
import { useSnackbar } from 'notistack';
import CircularProgress from '@material-ui/core/CircularProgress';

const useStyles = makeStyles(theme => ({
    container: {
        display: 'flex',
        justifyContent: 'flex',
        alignItems: 'flex-end',
    },
    image: {
        width: '100%',
        minHeight: 300,
        minWidth: 300,
        [theme.breakpoints.up('md')]: {
            width: 600,
        }
    },
    skeleton: {
        width: '100%',
        minHeight: 300,
        minWidth: 300,
        [theme.breakpoints.up('md')]: {
            width: 600,
            maxHeight: 500,
            minHeight: 500
        }
    },
    input: {
        display: 'none',
    },
    textField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: '100%'
    },
    picker: {
        display: 'flex',
    },
    button: {
        margin: theme.spacing(1),
    },
    form: {
        padding: 20,
        width: '100%'
    },
    wrapper: {
        position: 'relative'
    },
    spinner: {
        position: 'absolute',
        top: 0,
        left: 0,
        width: '100%',
        height: '100%',
        zIndex: 2000,
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'rgba(100, 100, 100, 0.5)'
    }
}));

export default function UploadForm(props) {
    const [file, setFile] = useState(null);
    const [fileUrl, setFileUrl] = useState("");
    const [fileName, setFileName] = useState("");
    const [uploadDisabled, setUploadDisabled] = useState(true);
    const [haveImage, setHaveImage] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const { enqueueSnackbar } = useSnackbar();

    const onChange = e => {
        if (e.target.files[0].size <= 1048576) {
            setIsLoading(true);
            setFile(e.target.files[0]);
            if (fileUrl !== "") {
                URL.revokeObjectURL(fileUrl);
            }
            setFileUrl(URL.createObjectURL(e.target.files[0]));
            setFileName(e.target.files[0].name);
            setUploadDisabled(false);
            setHaveImage(true);
            setIsLoading(false);
        } else {
            enqueueSnackbar('The file is too large! Maximum filesize is 1 MB.', { variant: 'error' });
        }
    }

    const onFormSubmit = event => {
        event.preventDefault()
        const formData = new FormData()
        formData.append("file", file)
        axios.post("api/post/upload", formData, {
            headers: { 'content-type': 'multipart/form-data' }
        }).then(res => {
            console.log("upload ok");
        })
        .then(props.parentCallback())
        .catch(err => {
            console.log(err);
            console.log("failed to upload file");
        });
    }

    const classes = useStyles();
    return (
        <div className={classes.wrapper}>
            {isLoading &&
                <div className={classes.spinner}>
                    <CircularProgress color="secondary" />
                </div>
            }
            <Grid container className={classes.container}>
                <Grid item xs={12}>
                    {haveImage && <img className={classes.image} src={fileUrl} />}
                    {!haveImage && <Skeleton variant="rect" className={classes.skeleton} />}
                </Grid>
                <form onSubmit={onFormSubmit} className={classes.form}>
                    <Grid item xs={12} className={classes.picker}>
                        <input
                            type="file"
                            accept="image/png, image/jpeg"
                            onChange={onChange}
                            className={classes.input}
                            id="file-input"
                        />
                        <label htmlFor="file-input">
                            <Button className={classes.button} variant="outlined" component="span">
                                Browse
                            </Button>
                        </label>
                        <TextField
                            id="standard-read-only-input"
                            label="File name"
                            defaultValue=""
                            color="secondary"
                            value={fileName}
                            className={classes.textField}
                            InputProps={{
                                readOnly: true,
                            }}
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
                </form>
            </Grid>
        </div>
    )
}