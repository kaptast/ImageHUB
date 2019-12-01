import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Chip from '@material-ui/core/Chip';

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
}));

export default function ChipsArray(props) {
  const classes = useStyles();

  return (
      <div className={classes.root}>
      {props.tags.map((data, key) => {
        return (
          <Chip
            key={key}
            label={data.name}
            className={classes.chip}
          />
        );
      })}
      </div>
  );
}