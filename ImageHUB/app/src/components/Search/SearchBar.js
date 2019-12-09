import React from 'react';
import { Link } from "react-router-dom";
import { fade, makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import Badge from '@material-ui/core/Badge';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import HomeIcon from '@material-ui/icons/Home';
import AccountCircle from '@material-ui/icons/AccountCircle';
import MoreIcon from '@material-ui/icons/MoreVert';
import UploadButton from '../Upload/Upload';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import SearchField from './SearchField';
import axios from 'axios';

const useStyles = makeStyles(theme => ({
    grow: {
        flexGrow: 1,
    },
    menuButton: {
        marginRight: theme.spacing(2),
    },
    title: {
        display: 'none',
        [theme.breakpoints.up('sm')]: {
            display: 'block',
            textDecoration: 'none'
        },
    },
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
    sectionDesktop: {
        display: 'none',
        [theme.breakpoints.up('md')]: {
            display: 'flex',
        },
    },
    sectionMobile: {
        display: 'flex',
        [theme.breakpoints.up('md')]: {
            display: 'none',
        },
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

export default function SearchBar(props) {
    const classes = useStyles();
    const [mobileMoreAnchorEl, setMobileMoreAnchorEl] = React.useState(null);
    const [notificationCount, setNotificationCount] = React.useState(0);

    const isMobileMenuOpen = Boolean(mobileMoreAnchorEl);

    function handleMobileMenuClose() {
        setMobileMoreAnchorEl(null);
    }

    function handleMobileMenuOpen(event) {
        setMobileMoreAnchorEl(event.currentTarget);
    }

    function handleLogout(event) {
        props.logout();
    }

    React.useEffect(() => {
        axios.get("api/profile/GetNotifications")
            .then(res => setNotificationCount(res.data))
            .catch(err => {
                console.log(err)
                console.log("failed to get notifications")
            });
    }, []);

    const mobileMenuId = 'primary-search-account-menu-mobile';
    const renderMobileMenu = (
        <Menu
            anchorEl={mobileMoreAnchorEl}
            anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
            id={mobileMenuId}
            keepMounted
            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
            open={isMobileMenuOpen}
            onClose={handleMobileMenuClose}
        >
            <MenuItem>
                <UploadButton />
                <p>Upload</p>
            </MenuItem>
            <Link className={classes.link} to={`/`}>
                <MenuItem>
                    <IconButton aria-label="feed" color="inherit">
                        <HomeIcon />
                    </IconButton>
                    <p>Feed</p>
                </MenuItem>
            </Link>
            <Link className={classes.link} to={`/profile`}>
                <MenuItem>
                    <IconButton aria-label="account of current user" color="inherit">
                        <AccountCircle />
                    </IconButton>
                    <p>Profile</p>
                </MenuItem>
            </Link>
            <MenuItem onClick={handleLogout} >
                <IconButton aria-label="account of current user" color="inherit">
                    <ExitToAppIcon />
                </IconButton>
                <p>Logout</p>
            </MenuItem>
        </Menu>
    );

    return (
        <div className={classes.grow}>
            <AppBar>
                <Toolbar>
                    <Link className={classes.link} to={`/`} >
                        <Typography className={classes.title} color="inherit" variant="h5" noWrap>
                            ImageHUB
                        </Typography>
                    </Link>
                    <SearchField />
                    <div className={classes.grow} />
                    <div className={classes.sectionDesktop}>
                        <UploadButton />
                        <Link className={classes.link} to={`/profile`}>
                            <IconButton aria-label="account of current user" color="inherit">
                                <Badge badgeContent={notificationCount} color="error">
                                    <AccountCircle />
                                </Badge>
                            </IconButton>
                        </Link>
                        <IconButton color="inherit" onClick={handleLogout}>
                            <ExitToAppIcon />
                        </IconButton>
                    </div>
                    <div className={classes.sectionMobile}>
                        <IconButton
                            aria-label="show more"
                            aria-controls={mobileMenuId}
                            aria-haspopup="true"
                            onClick={handleMobileMenuOpen}
                            color="inherit"
                        >
                            <MoreIcon />
                        </IconButton>
                    </div>
                </Toolbar>
            </AppBar>
            {renderMobileMenu}
        </div>
    );
}
