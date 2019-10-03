import React from 'react';
import Image from '../components/Post/Image';
import Post from '../components/Post/Post';
import Header from '../components/Post/Header';

export default {
  title: 'Image',
};

const post = {
  image: "https://i.imgur.com/jp5z1s0.jpg",
  avatar: "https://i.imgur.com/jp5z1s0.jpg",
  userName: "Gipsz Jakab",
  title: "single image"
}

export const SingleImageMedia = () => (
  <Image value={post} />
);

export const SinglePost = () => (
  <Post value={post} />
);

export const PostHeader = () => (
  <Header value={post} />
);