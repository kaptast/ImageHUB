import React from 'react';
import LoginButton from '../components/Login/LoginButton';
import LoginLogo from '../components/Login/Logo';
import LoginPage from '../components/Login/Login';

export default {
  title: 'Login',
};

export const Button = () => <LoginButton />;

export const Logo = () => <LoginLogo />;

export const Login = () => <LoginPage />;