import React from 'react';
import { Route, Switch } from 'react-router';
import { BrowserRouter } from 'react-router-dom'
import Layout from './components/Layout';
import Home from './components/Home';
import ProfileFeed from './components/ProfileFeed';
import Messages from './components/Messages';
import Login from './components/Login/Login';

export default () => (
  <BrowserRouter>
    <Layout>
      <Switch>
        <Route exact path='/' component={Home} />
        <Route path='/profile' component={ProfileFeed} />
        <Route path='/messages' component={Messages} />
        <Route path='/login' component={Login} />
      </Switch>
    </Layout>
  </BrowserRouter>
);
