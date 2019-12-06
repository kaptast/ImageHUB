import React, { useState, useEffect } from 'react';
import Feed from './Feed/ProfileFeed';
import Header from './Profile/Header';
import axios from 'axios';

const defaultProfile = {
    name: "",
    posts: [],
    friends: [],
    showFriendButton: false,
}

export default function Profile(props) {
    const [profile, setProfile] = useState(defaultProfile);
    const [isLoading, setIsLoading] = useState(true);

    const index = props.match.params.index || "0";

    useEffect(() =>{
        setIsLoading(true);
        const url = `api/profile/GetById?id=${index}`;
        axios.get(url)
            .then(res => {
                setProfile(res.data);
                setIsLoading(false);
            });
    }, [props.match.params.index]);

    return (
      <div>
        <Header profile={profile} />
        <Feed posts={profile.posts} isLoading={isLoading} />
      </div>
    );
}
