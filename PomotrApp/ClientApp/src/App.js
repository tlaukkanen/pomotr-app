import React, { Component } from 'react';
import { Route } from 'react-router';
import { ResponsiveSideNav } from './components/ResponsiveSideNav';
import { Home } from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import { withOidcSecure } from '@axa-fr/react-oidc-context';

import './custom.css'
import FamilyTable from './components/Family';
import { Login } from './components/Login';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <ResponsiveSideNav>
        <Route exact path='/' component={Home} />
        <Route exact path='/login' component={Login} />
        <Route exact path='/family' component={withOidcSecure(FamilyTable)} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </ResponsiveSideNav>
    );
  }
}
