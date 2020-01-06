import React, { Component } from 'react';
import { Route } from 'react-router';
import ResponsiveSideNav from './components/ResponsiveSideNav';
import { Home } from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import FamilyTable from './components/Family';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <ResponsiveSideNav>
        <Route exact path='/' component={Home} />
        <Route exact path='/family' component={FamilyTable} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </ResponsiveSideNav>
    );
  }
}
