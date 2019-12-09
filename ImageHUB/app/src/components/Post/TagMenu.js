import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import Chip from '@material-ui/core/Chip';
import { Link } from "react-router-dom";

const useStyles = makeStyles(theme => ({
    chip: {
        display: 'inline-block'
    },
    link: {
        textDecoration: 'none',
        color: 'black',
        '&:focus, &:hover, &:visited, &:link, &:active': {
            textDecoration: 'none',
            color: 'black'
        }
    }
}));

export default function TagMenu(props) {
    const [anchorEl, setAnchorEl] = useState(null);

    const handleClick = event => {
        setAnchorEl(event.currentTarget);
    }

    const handleClose = () => {
        setAnchorEl(null);
    }

    const classes = useStyles();

    return (
        <div className={classes.chip}>
            <Chip label="..." onClick={handleClick} />
            <Menu
                id="tags-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                {props.tags.map((data, key) => (
                    <Link className={classes.link} key={key} to={'tag/' + data}>
                        <MenuItem >
                            {data}
                        </MenuItem>
                    </Link>
                ))}
            </Menu>
        </div>
    );
}