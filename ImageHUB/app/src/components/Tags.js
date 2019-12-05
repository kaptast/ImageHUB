import React, { useState, useEffect } from 'react';
import Feed from './Feed/HomeFeed';
import axios from 'axios';


export default function Profile(props) {
    const [posts, setPosts] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    const index = props.match.params.index || "";

    useEffect(() =>{
        setIsLoading(true);
        const url = `api/post/GetPostsByTag?tag=${index}`;
        axios.get(url)
            .then(res => {
                setPosts(res.data);
                setIsLoading(false);
            });
    }, []);

    return (
      <div>
        <Feed posts={posts} isLoading={isLoading} />
      </div>
    );
}
