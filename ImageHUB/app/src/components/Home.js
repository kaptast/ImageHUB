import React, { useState, useEffect } from 'react';
import Feed from './Feed/HomeFeed';
import axios from 'axios';

export default function Home() {

    const [homePosts, setHomePosts] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        setIsLoading(true)
        axios.get("api/profile")
            .then(res => {
                console.log("profile get ok");
            })
            .then(axios.get("api/post")
                .then(res => {
                    setHomePosts(res.data);
                    setIsLoading(false);
                }))
            .catch(err => {
                console.log(err)
                console.log("failed to get profile")
            });
    }, []);

    return (
        <div>
            <Feed posts={homePosts} isLoading={isLoading} />
        </div>
    );
}

