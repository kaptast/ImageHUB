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
  const [notificationCount, setNotificationCount] = React.useState(0);

  const index = props.match.params.index || "0";

  useEffect(() => {
    setIsLoading(true);
    let url = `api/profile/GetById?id=${index}`;
    axios.get(url)
      .then(res => {
        setProfile(res.data);
        setIsLoading(false);
      })
      .catch(err => {
        console.log(err);
        console.log("Error while loading profile");
      });

      url = `api/profile/GetNotifications?id=${index}`;
      axios.get(url)
          .then(res => setNotificationCount(res.data));
      // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.match.params.index]);

  return (
    <div>
      <Header profile={profile} notifcount={notificationCount} />
      <Feed posts={profile.posts} isLoading={isLoading} />
    </div>
  );
}
