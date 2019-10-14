import React from 'react';
import ProfileFeed from '../components/ProfileFeed';
import Header from '../components/Profile/Header';

export default {
  title: 'Profile',
};

const profile = {
  avatar: "https://i.imgur.com/jp5z1s0.jpg",
  userName: "Gipsz Jakab",
  friends: 15,
  posts: [
    {
      image: "https://i.imgur.com/jp5z1s0.jpg",
      avatar: "https://i.imgur.com/jp5z1s0.jpg",
      userName: "Gipsz Jakab",
      title: "single image",
      show: true
    },
    {
      image: "https://i.imgur.com/jp5z1s0.jpg",
      avatar: "https://i.imgur.com/jp5z1s0.jpg",
      userName: "Gipsz Jakab",
      title: "single image",
      show: true
    },
    {
      image: "https://i.imgur.com/jp5z1s0.jpg",
      avatar: "https://i.imgur.com/jp5z1s0.jpg",
      userName: "Gipsz Jakab",
      title: "single image",
      show: true
    },
    {
      image: "https://i.imgur.com/jp5z1s0.jpg",
      avatar: "https://i.imgur.com/jp5z1s0.jpg",
      userName: "Gipsz Jakab",
      title: "single image",
      show: true
    },
    {
      image: "https://i.imgur.com/jp5z1s0.jpg",
      avatar: "https://i.imgur.com/jp5z1s0.jpg",
      userName: "Gipsz Jakab",
      title: "single image",
      show: true
    }
  ]
}

const profile2 = {
  avatar: "https://i.imgur.com/jp5z1s0.jpg",
  userName: "Gipsz Jakab",
  friends: 1,
  posts: [
    {
      image: "https://i.imgur.com/jp5z1s0.jpg",
      avatar: "https://i.imgur.com/jp5z1s0.jpg",
      userName: "Gipsz Jakab",
      title: "single image",
      show: true
    }
  ]
}

export const ProfileHeader = () => (
  <Header profile={profile} />
);

export const ProfileHeaderWith1Value = () => (
  <Header profile={profile2} />
);

export const ProfileWithFeed = () => (
  <div>
    <Header profile={profile} />
    <ProfileFeed posts={profile.posts} />
  </div>
);