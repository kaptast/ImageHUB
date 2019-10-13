import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Route, Switch, Redirect } from 'react-router';
import { BrowserRouter } from 'react-router-dom'
import Layout from './components/Layout';
import Home from './components/Home';
import Profile from './components/Profile';
import Messages from './components/Messages';
import Login from './components/Login/Login';

export default function App() {

  const [isLoggedIn, setIsLoggedIn] = useState(false)
  const [name, setName] = useState("")

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
      })
  })

  const logout = () => {
    axios.get("api/auth/logout")
      .then(res => {
        setIsLoggedIn(false)
      })
  }

  console.log(isLoggedIn);

  if (!isLoggedIn) {
    return <Login />;
  } else {
    return (
      <BrowserRouter>
        <Layout>
          <Switch>
            <Route exact path='/' component={Home} />
            <Route path='/profile' component={Profile} />
            <Route path='/messages' component={Messages} />
            <Route path='/login' component={Login} />
          </Switch>
        </Layout>
      </BrowserRouter>
    );
  }
}
