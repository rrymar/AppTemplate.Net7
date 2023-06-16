import React from 'react';
import {Route} from 'react-router-dom';
import UserDetails from './user-management/user-details/UserDetails';

import UsersList from './user-management/users-list/UsersList';
import UsersListAnt from './user-management/users-list/UsersListAnt';
import UsersListMaterial from './user-management/users-list/UsersListMaterial';

function Routes() {
    return (
        <React.Fragment>
            <Route exact path="/users">
                <UsersList/>
            </Route>
            <Route path="/users/:id">
                <UserDetails/>
            </Route>
            <Route path="/users-ant">
                <UsersListAnt/>
            </Route>
            <Route path="/users-mat">
                <UsersListMaterial/>
            </Route>
        </React.Fragment>
    );
}

export default Routes;
