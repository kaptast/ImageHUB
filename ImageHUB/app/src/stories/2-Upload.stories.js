import React from 'react';
import UploadForm from '../components/Upload/UploadForm';
import UploadButton from '../components/Upload/Upload';
import { SnackbarProvider } from 'notistack';

export default {
    title: 'Upload',
};

const handleClose = () => {

};

export const Upload = () => (
    <SnackbarProvider maxSnack={3}>
        <UploadButton />
    </SnackbarProvider>
);

export const UploadDialog = () => (
    <SnackbarProvider maxSnack={3}>
        <UploadForm parentCallback={handleClose} />
    </SnackbarProvider>
);
