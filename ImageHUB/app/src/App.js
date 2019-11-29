import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Login from './components/Login/Login';
import Layout from './components/Layout';
import { BrowserRouter } from 'react-router-dom';
import { Route, Switch, Redirect } from 'react-router';

export default function App() {

    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [name, setName] = useState("");
    const [id, setId] = useState("");
    const [file, setFile] = useState(null);
    const [urls, setUrls] = useState([]);

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

    useEffect(() => {
        if (isLoggedIn) {
            axios.get("api/profile")
                .then(res => {
                    console.log("profile get ok")
                    console.log(res)
                    setId(res.data.userID)
                })
                .catch(err => {
                    console.log(err)
                    console.log("failed to get profile")
                })
            axios.get("api/post").then(res => setUrls(res.data))
        }
    }, [isLoggedIn])

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
                                
                            </Switch>
                        </Layout>
                    </BrowserRouter>
                </>}

            {!isLoggedIn &&
                <Login />}
        </div>
    )
}
