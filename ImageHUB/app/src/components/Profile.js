import React, { useState, useEffect } from 'react';
import Feed from './Feed/ProfileFeed';
import Header from './Profile/Header';

const defaultProfile = {
    name: "",
    index: "asd",
    posts: [],
    friends: [],
    showFriendButton: false,
}

export default function Profile(props) {
    const [profile, setProfile] = useState(defaultProfile);
    const [isLoading, setIsLoading] = useState(true);

    const index = parseInt(props.match.params.index, 10) || 0;

    useEffect(() =>{
        setIsLoading(true);
        const url = `api/profile/GetById?id=${index}`;
        axios.get("api/profile")
            .then(res => {
                setProfile(res.data);
                setIsLoading(false);
            });
    }, []);

    return (
      <div>
        <Header profile={profile} />
        <Feed posts={profile.posts} isLoading={isLoading} />
      </div>
    );
}
