import React from 'react';
import FriendList from '../components/Profile/FriendList';
import { BrowserRouter } from 'react-router-dom';

export default {
    title: 'FriendList',
};

const profile = {
    friends: [
        {
            userID: 1231141412,
            userName: "Teszt Elek"
        },
        {
            userID: 12314514154,
            userName: "Teszt Ernő"
        },
    ]
}

export const FriendButton = () => (
    <BrowserRouter>
        <FriendList profile={profile} />
    </BrowserRouter>
);


