import React from 'react';
import { Route, IndexRoute } from 'react-router';

import NewcomersNetworkUI from './lib/NewcomersNetworkUI.react.jsx';
import Main from './lib/Main.react.jsx';
import About from './lib/About.react.jsx';

export default (
    <Route path="/" component={NewcomersNetworkUI}>
        <IndexRoute component={Main} /*onEnter={requireAuth}*/ />
        <Route path="about" component={About} />
    </Route>
);

/*
function requireAuth(nextState, replace) {  
  if (!sessionStorage.getItem('token')) {
    replace({
      pathname: '/login',
      state: { nextPathname: nextState.location.pathname }
    })
  }
}
*/