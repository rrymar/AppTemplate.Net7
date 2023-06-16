import {useParams} from 'react-router-dom';
import CrudParams from 'core/crudParams';
import {Typography, TextField, Button, makeStyles, Paper} from '@material-ui/core';
import React, {useEffect, useState} from 'react';
import {User} from '../user';
import axios from 'axios';
import {useHistory} from 'react-router-dom';

const useStyles = makeStyles(theme => ({
    root: {
        padding: theme.spacing(3, 3),
        margin: theme.spacing(3, 2),
        width: 320
    },
    textField: {
        margin: theme.spacing(1),
        width: 250
    },
    button: {
        margin: theme.spacing(2)
    },
}));

function UserDetails() {
    const [item, setItem] = useState<Partial<User>>({username: '', lastName: '', firstName: '', email: ''});

    const classes = useStyles();
    let {id} = useParams<CrudParams>();

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const name = event.target.name;
        setItem({...item, [name]: event.target.value});
    };

    let history = useHistory();

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (isNew) {
            axios.post<Partial<User>>('/api/Users/', item)
                .then(res => history.push('/users-mat'));
        } else {
            axios.put<Partial<User>>('/api/Users/' + id, item)
                .then(res => history.push('/users-mat'));
        }
    };

    const isNew = id === 'new';
    const title = isNew ? 'New User' : 'User';

    useEffect(() => {
        if (isNew) {
            return;
        }
        axios.get<User>('/api/Users/' + id).then(res => {
            setItem(res.data);
        });
    }, []);

    return (
        <Paper className={classes.root}>
            <form onSubmit={handleSubmit}>
                <Typography variant="h6">
                    {title}
                </Typography>

                <TextField name="username" label="User Name" required className={classes.textField}
                           value={item?.username} onChange={handleChange}/>
                <TextField name="firstName" label="First Name" className={classes.textField}
                           value={item?.firstName} onChange={handleChange}/>
                <TextField name="lastName" label="Last Name" className={classes.textField}
                           value={item?.lastName} onChange={handleChange}/>
                <TextField name="email" label="Email" className={classes.textField}
                           value={item?.email} onChange={handleChange}/>

                <Button type="submit" variant="contained" color="primary" className={classes.button}>
                    Save
                </Button>
            </form>
        </Paper>
    );
}

export default UserDetails;
