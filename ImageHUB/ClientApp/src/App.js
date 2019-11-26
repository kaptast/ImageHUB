import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Route, Switch, Redirect } from 'react-router';
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
  const [id, setID] = useState("");
  const [email, setEmail] = useState("");

  useEffect(() => {
    axios.get("api/auth/isloggedin", {
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(res => {
      setIsLoggedIn(true);
      setName(res.data.name);
      setID(res.data.id);
      setEmail(res.data.email);
    })
      .catch(err => {
        console.log(err);
        console.log("failed to log in.");
        setIsLoggedIn(false);
      });
  });

  const logout = () => {
    console.log("logout");
    axios.get("api/auth/logout", {
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(res => {
        console.log('Logout successful.');
        setIsLoggedIn(false);
      });
  }

  if (!isLoggedIn) {
    return <Login />;
  } else {
    return (
      <BrowserRouter>
        <Layout name={name} loggedIn={isLoggedIn} logout={logout}>
          <Switch>
            <Route exact path='/' render={(props) => <Home {...props} name={name} />} />
            <Route path='/profile/:index?' component={Profile} />
            <Route path='/messages' component={Messages} />
            <Route path='/search/:index?' component={Search} />
            <Route path='/login' component={Login} />
          </Switch>
        </Layout>
      </BrowserRouter>
    );
  }
}
