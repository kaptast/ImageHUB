import React from 'react';
import CardMedia from '@material-ui/core/CardMedia';

export default function Image(props) {
    var imagePath = process.env.PUBLIC_URL + props.value.image;
    
    console.log(imagePath);
    return (
        <CardMedia
            image={imagePath}
            title={props.value.userName}
            alt={props.value.userName}
            component="img"
        />
    );
}