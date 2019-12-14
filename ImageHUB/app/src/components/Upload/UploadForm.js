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
import Tags from '../Post/Tags';

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
    },
    skeleton: {
        width: '100%',
        height: 300,
        [theme.breakpoints.up('md')]: {
            height: 500
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
        width: '100%',
        minHeight: 300,
        [theme.breakpoints.up('md')]: {
            width: 600
        }
    },
    formButtons: {
        margin: 20,
        width: '70%',
        [theme.breakpoints.up('md')]: {
            width: 500
        }
    },
    wrapper: {
        position: 'relative',
        maxWidth: 580
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
        backgroundColor: 'rgba(100, 100, 100, 0.7)'
    }
}));

function pictureIsAllowedByTags(tags) {
    const illegalTags = ['train', 'trains', 'wagon', 'wagons', 'railroad train'];
    let returnValue = true;

    illegalTags.forEach(tag => {
        if (tags.includes(tag)) {
            returnValue = false;
        }
    })

    return Boolean(returnValue);
}

export default function UploadForm(props) {
    const [file, setFile] = useState(null);
    const [fileUrl, setFileUrl] = useState("");
    const [fileName, setFileName] = useState("");
    const [uploadDisabled, setUploadDisabled] = useState(true);
    const [haveImage, setHaveImage] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const { enqueueSnackbar } = useSnackbar();
    const [tags, setTags] = useState([]);

    const onChange = e => {
        if (e.target.files[0].size <= 1048576) {
            setIsLoading(true);
            enqueueSnackbar('Analyzing picture.', { variant: 'info' });

            setFile(e.target.files[0]);
            if (fileUrl !== "") {
                URL.revokeObjectURL(fileUrl);
            }
            setFileUrl(URL.createObjectURL(e.target.files[0]));
            setFileName(e.target.files[0].name);
            setHaveImage(true);

            const formData = new FormData()
            formData.append("file", e.target.files[0])
            axios.post("https://westeurope.api.cognitive.microsoft.com/vision/v2.0/analyze?visualFeatures=Tags&language=en", formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                    'Ocp-Apim-Subscription-Key': '0148e9a37b0949079a6734999de6658d'
                }
            }).then(res => {
                let pictureTags = res.data.tags.map(({ name }) => name);
                setTags(pictureTags);

                if (pictureIsAllowedByTags(pictureTags)) {
                    setUploadDisabled(false);
                } else {
                    setUploadDisabled(true);
                    enqueueSnackbar('Picture contains not allowed content.', { variant: 'error' });
                }

                setIsLoading(false);
            }).catch(err => {
                console.log(err);
                setIsLoading(false);
            });
        } else {
            enqueueSnackbar('The file is too large! Maximum filesize is 1 MB.', { variant: 'error' });
        }
    }

    const onFormSubmit = event => {
        event.preventDefault()
        const formData = new FormData()
        formData.append("file", file);
        formData.append("tags", tags);
        axios.post("api/post/upload", formData, {
            headers: { 'content-type': 'multipart/form-data' }
        }).then(res => {
            console.log("upload ok");
        }).then(
            props.parentCallback()
        ).catch(err => {
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
                <form onSubmit={onFormSubmit} className={classes.form}>
                    <input type="file" accept="image/png, image/jpeg" onChange={onChange} className={classes.input} id="file-input" />
                    <Grid item xs={12}>
                        {haveImage && <img className={classes.image} alt="uploaded" src={fileUrl} />}
                        {!haveImage &&
                            <label htmlFor="file-input">
                                <Skeleton variant="rect" className={classes.skeleton} />
                            </label>
                        }
                    </Grid>
                    <div className={classes.formButtons}>
                        <Grid item xs={12}>
                            <Tags tags={tags} />
                        </Grid>
                        <Grid item xs={12} className={classes.picker}>
                            <label htmlFor="file-input">
                                <Button className={classes.button} variant="contained" component="span">
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
                    </div>
                </form>
            </Grid>
        </div>
    )
}
