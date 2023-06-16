import React from 'react';
import {AppBar, makeStyles, Toolbar} from '@material-ui/core';
import {Router, Switch} from 'react-router-dom';
import {createBrowserHistory} from 'history';
import UsersRoutes from '../users/Routes';
import ToolbarLink from './ToolbarLink';

const useStyles = makeStyles((theme) => ({
    pageContainer: {
        margin: theme.spacing(2),
    }
}));

const history = createBrowserHistory();

function App() {
    const classes = useStyles();
    return (
        <div className="App">
            <Router history={history}>
                <AppBar position="static">
                    <Toolbar>
                        <ToolbarLink title="App Template" route="/"/>
                        <ToolbarLink title="Users Material" route="/users-mat"/>
                        <ToolbarLink title="Users Primereact" route="/users"/>
                        <ToolbarLink title="Users Ant Design" route="/users-ant"/>
                    </Toolbar>
                </AppBar>
                <div className={classes.pageContainer}>
                    <Switch>
                        <UsersRoutes />
                    </Switch>
                </div>
            </Router>
        </div>
    );
}

export default App;
