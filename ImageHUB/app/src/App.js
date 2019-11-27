import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Route, Switch } from 'react-router';
import { BrowserRouter } from 'react-router-dom'
import Layout from './components/Layout';
import Home from './components/Home';
import Profile from './components/Profile';
import Messages from './components/Messages';
import Login from './components/Login/Login';
import Search from './components/Search/SearchResults';

export default function App() {

    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [name, setName] = useState("");

    useEffect(() => {
        axios.get("api/auth/isloggedin").then(res => {
            setIsLoggedIn(true);
            setName(res.data.name);
            console.log("Logged in");
            console.log(isLoggedIn);
        })
            .catch(err => {
                console.log(err);
                console.log("failed to log in.");
                setIsLoggedIn(false);
            });
    });

    const logout = () => {
        axios.get("api/auth/logout")
            .then(res => {
                setIsLoggedIn(false)
                console.log("Logout state:");
                console.log(isLoggedIn);
            }).catch(err => {
                console.log(err);
                console.log("failed to log out");
                setIsLoggedIn(false);
            });
    }

    return (
        <p>Hello</p>
    );
}
