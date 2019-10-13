import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import Dialog from '@material-ui/core/Dialog';
import AddIcon from '@material-ui/icons/Add';
import UploadForm from './UploadForm';

function UploadDialog(props){
    const { onClose, open } = props;

    const handleClose = () => {
        onClose();
      };

    return (
        <Dialog onClose={handleClose} open={open}>
            <UploadForm parentCallback={handleClose}/>
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