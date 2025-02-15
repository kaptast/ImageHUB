import React from 'react';
import SearchBar from './Search/SearchBar';
import Container from '@material-ui/core/Container';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
  icon: {
    marginRight: theme.spacing(2),
  },
  heroContent: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(8, 0, 6),
  },
  heroButtons: {
    marginTop: theme.spacing(4),
  }
}));

export default function (props) {
  const classes = useStyles();

  return (
    <div>
      <SearchBar userName={props.name} loggedIn={props.loggedIn} logout={props.logout}/>
      <div className={classes.heroContent}>
        <Container maxWidth="xl">
          {props.children}
        </Container>
      </div>
    </div>
  );
}