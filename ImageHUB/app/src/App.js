import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Login from './components/Login/Login';
import Layout from './components/Layout';
import Home from './components/Home';
import Profile from './components/Profile';
import Search from './components/Search/SearchResults';
import { BrowserRouter } from 'react-router-dom';
import { Route, Switch } from 'react-router';

export default function App() {

    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [name, setName] = useState("");

    useEffect(() => {
        axios.get("api/auth/isloggedin")
            .then(res => {
                console.log("ok.")
                setIsLoggedIn(true)
                setName(res.data)
            })
            .catch(err => {
                console.log(err)
                console.log("failed to log in.")
                setIsLoggedIn(false)
            });
    })

    const logout = () => {
        axios.get("api/auth/logout")
            .then(res => {
                setIsLoggedIn(false)
            })
    }

    return (
        <div>
            {isLoggedIn &&
                <>
                    <BrowserRouter>
                        <Layout name={name} loggedIn={isLoggedIn} logout={logout}>
                            <Switch>
                                <Route exact path='/' component={Home} />
                                <Route path='/profile/:index?' component={Profile} />
                                <Route path='/search/:index?' component={Search} />
                                <Route path='/login' component={Login} />
                            </Switch>
                        </Layout>
                    </BrowserRouter>
                </>}

            {!isLoggedIn &&
                <Login />}
        </div>
    )
}
