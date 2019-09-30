import React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Profile from './components/Profile';
import Messages from './components/Messages';

export default () => (
  <Layout>
    <Switch>
      <Route exact path='/' component={Home} />
      <Route path='/profile' component={Profile} />
      <Route path='/messages' component={Messages} />
    </Switch>
  </Layout>
);
