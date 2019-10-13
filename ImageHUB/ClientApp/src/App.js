import React from 'react';
import { Route, Switch } from 'react-router';
import { BrowserRouter } from 'react-router-dom'
import Layout from './components/Layout';
import Home from './components/Home';
import Profile from './components/Profile';
import Messages from './components/Messages';
import Login from './components/Login/Login';

export default () => (
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
