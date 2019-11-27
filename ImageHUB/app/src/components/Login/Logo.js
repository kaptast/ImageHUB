import React from 'react';
import Typography from '@material-ui/core/Typography';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import { useTheme } from '@material-ui/core/styles';


export default function Logo(){
    const theme = useTheme();
    const style = useMediaQuery(theme.breakpoints.up('sm')) ? "h1" : "h3";

    return (
        <Typography variant={style}>ImageHUB</Typography>
    );
}
