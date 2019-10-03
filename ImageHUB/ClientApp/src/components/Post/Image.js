import React from 'react';
import CardMedia from '@material-ui/core/CardMedia';

export default function Image(props) {
    return (
        <CardMedia
            image={props.value.image}
            title={props.value.title}
            alt={props.value.title}
            component="img"
        />
    );
}