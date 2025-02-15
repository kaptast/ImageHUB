import React from 'react';
import Logo from './Logo';
import LoginButton from './LoginButton';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
    container: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100%',
        minHeight: 400,
    },
}));


export default function Login() {
    const classes = useStyles();

    return (
        <div className={classes.container}>
            <Container maxWidth='sm'>
                <Grid container justify="center" spacing={5}>
                    <Grid item>
                        <Logo />
                    </Grid>
                    <Grid item>
                        <LoginButton />
                    </Grid>
                </Grid>
            </Container>
        </div>
    );
}