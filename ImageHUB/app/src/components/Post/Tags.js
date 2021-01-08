import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Chip from '@material-ui/core/Chip';
import TagMenu from './TagMenu';
import { Link } from "react-router-dom";

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
    justifyContent: 'center',
    flexWrap: 'wrap',
    padding: theme.spacing(0.5),
  },
  chip: {
    margin: theme.spacing(0.5),
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

export default function ChipsArray(props) {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      {props.tags.map((data, key) => (
        <Chip
          key={key}
          label={data}
          className={classes.chip}
        />
      ))}
    </div>
  );
}

export function SmallChipsArray(props) {
  const classes = useStyles();

  let showMenu = props.tags.length > 4;
  let smallArray = [];
  if (showMenu) {
    smallArray.push(props.tags[0]);
    smallArray.push(props.tags[1]);
    smallArray.push(props.tags[2]);
    smallArray.push(props.tags[3]);
  }

  return (
    <div className={classes.root}>
      {showMenu &&
        <div>
          {smallArray.map((data, key) => (
            <Link className={classes.link} key={key} to={'/tag/' + data}>
              <Chip
                key={key}
                label={data}
                className={classes.chip}
              />
            </Link>
          ))}
          <TagMenu className={classes.chip} tags={props.tags} />
        </div>
      }
      {!showMenu &&
        <div>
          {props.tags.map((data, key) => (
            <Link className={classes.link} key={key} to={"tag/" + data}>
              <Chip
                key={key}
                label={data}
                className={classes.chip}
              />
            </Link>
          ))}
        </div>
      }
    </div>
  );
}