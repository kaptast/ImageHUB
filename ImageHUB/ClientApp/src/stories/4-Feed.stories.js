import React from 'react';
import Feed from '../components/Feed';

export default {
    title: 'Feed',
};

const posts = [
    {
        image: "https://i.imgur.com/jp5z1s0.jpg",
        avatar: "https://i.imgur.com/jp5z1s0.jpg",
        userName: "Gipsz Jakab",
        title: "single image"
    },
    {
        image: "https://i.imgur.com/jp5z1s0.jpg",
        avatar: "https://i.imgur.com/jp5z1s0.jpg",
        userName: "Gipsz Jakab",
        title: "single image"
    },
    {
        image: "https://i.imgur.com/jp5z1s0.jpg",
        avatar: "https://i.imgur.com/jp5z1s0.jpg",
        userName: "Gipsz Jakab",
        title: "single image"
    },
    {
        image: "https://i.imgur.com/jp5z1s0.jpg",
        avatar: "https://i.imgur.com/jp5z1s0.jpg",
        userName: "Gipsz Jakab",
        title: "single image"
    },
    {
        image: "https://i.imgur.com/jp5z1s0.jpg",
        avatar: "https://i.imgur.com/jp5z1s0.jpg",
        userName: "Gipsz Jakab",
        title: "single image"
    }
]

export const HomeFeed = () => (
    <Feed posts={posts} />
  );
