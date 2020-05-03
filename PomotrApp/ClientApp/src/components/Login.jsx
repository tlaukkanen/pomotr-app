import React from 'react';
import { Grid, TextField, Button } from '@material-ui/core';

export const Login = () => {

    return (
    <>
        <Grid container>
            <Grid item xs={12}>
                <TextField 
                    xs={12}
                    label="Username"
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    xs={12}
                    password
                    label="Password"
                    />
            </Grid>
            <Grid item xs={12}>
                <Button>Login</Button>
            </Grid>
        </Grid>

    </>);

}