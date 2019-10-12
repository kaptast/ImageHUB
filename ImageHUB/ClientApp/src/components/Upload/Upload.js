import React from 'react';
import { func } from 'prop-types';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import PropTypes from 'prop-types';
import DialogTitle from '@material-ui/core/DialogTitle';
import Dialog from '@material-ui/core/Dialog';
import AddIcon from '@material-ui/icons/Add';
import UploadForm from './UploadForm';

function UploadDialog(props){
    const { onClose, selectedValue, open } = props;

    const handleClose = () => {
        onClose();
      };

    return (
        <Dialog onClose={handleClose} open={open}>
            <UploadForm />
        </Dialog>
    );
}

export default function UploadButton(){
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = value => {
    setOpen(false);
    setSelectedValue(value);
  };

  return (
    <div>
      <IconButton color="inherit" onClick={handleClickOpen}>
          <AddIcon />
      </IconButton>
      <UploadDialog open={open} onClose={handleClose} />
    </div>
  );
}