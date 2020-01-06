import React, { Component } from 'react';
import { Grid, Typography, Card, CardContent, CardActions, Button } from '@material-ui/core';
import { CardBody } from 'reactstrap';
import { Add } from '@material-ui/icons';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <Typography variant="h1">Pomotr!</Typography>
        </Grid>
        <Grid container xs={12} spacing={2}>
          <Grid item xs={6}>
            <Card>
              <CardContent>
                <Typography gutterBottom>Tasks Completed</Typography>
                
              </CardContent>
              <CardBody>Points</CardBody>
              <CardActions>
                <Button>
                  <Add></Add>
                  
                </Button>
              </CardActions>
            </Card>
          </Grid>
          <Grid item xs={6}>
            <Card>
              <CardContent>This week</CardContent>
            </Card>
          </Grid>
        </Grid>
      </Grid>
    );
  }
}
