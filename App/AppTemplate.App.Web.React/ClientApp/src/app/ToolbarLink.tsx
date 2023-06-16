import {Button, makeStyles} from '@material-ui/core';
import {Link as RouterLink} from 'react-router-dom';
import React from 'react';

const useStyles = makeStyles((theme) => ({
    navigateButton: {
        marginLeft: theme.spacing(2),
    },
}));

interface ToolbarLinkProps {
    title: string;
    route: string;
}

function ToolbarLink(props: ToolbarLinkProps) {
    const classes = useStyles();
    return (
        <Button
            color="inherit"
            className={classes.navigateButton}
            component={RouterLink}
            to={props.route}>
            {props.title}
        </Button>
    );
}

export default ToolbarLink;
