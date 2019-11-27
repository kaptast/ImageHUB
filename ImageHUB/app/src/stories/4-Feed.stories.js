import React from 'react';
import Feed from '../components/Feed';
import ProfileFeed from '../components/ProfileFeed';

export default {
    title: 'Feed',
};

const posts = [
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

export const HomeFeed = () => (
    <Feed posts={posts} />
  );

export const Profile = () => (
    <ProfileFeed posts={posts} />
  );

