import React, { Component, useState } from 'react';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import { AppBar, Toolbar, Drawer, List, ListItem, ListItemIcon, 
  ListItemText, Divider, IconButton, 
  Grid, Hidden, CssBaseline, Typography, Box } from '@material-ui/core';
import { Menu, AssignmentTurnedIn, ExitToApp, Close, AssignmentInd, Assignment, SupervisorAccount, AccountCircle, Home } from '@material-ui/icons'
import { makeStyles, useTheme, withStyles } from '@material-ui/core/styles';
import { useReactOidc } from '@axa-fr/react-oidc-context';

const drawerWidth = 256;

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
  },
  drawer: {
    [theme.breakpoints.up('sm')]: {
      width: drawerWidth,
      flexShrink: 0,
      marginTop: theme.mixins.toolbar.height,
    },
  },
  appBar: {
    [theme.breakpoints.up('sm')]: {
      width: `calc(100% - ${drawerWidth}px)`,
      marginLeft: drawerWidth,
    },
  },
  appBarSpacer: {
    [theme.breakpoints.up('sm')]: {
      marginLeft: drawerWidth,
    },
  },
  menuButton: {
    marginRight: theme.spacing(2),
    [theme.breakpoints.up('sm')]: {
      display: 'none',
    },
  },
  toolbar: theme.mixins.toolbar,
  drawerPaper: {
    width: drawerWidth,
  },
  content: {
    //flexGrow: 1,
    padding: theme.spacing(3),
    marginTop: '60px',
  },
}));

export const ResponsiveSideNav = (props) => {
  const { oidcUser } = useReactOidc();

//export class NavMenu extends Component {
//  static displayName = NavMenu.name;

  const classes = useStyles();
  const [showDrawer, setShowDrawer] = useState(false);

/*  constructor (props) {
    super(props);


    this.toggleSidebar = this.toggleSidebar.bind(this);
    this.state = {
      showDrawer: false
    };
  }
*/
  function toggleSidebar() {
    return setShowDrawer(!showDrawer);
  }
  /*
  toggleSidebar = () => {
    this.setState({
      showDrawer: !this.state.showDrawer
    });
  }
  */

//  render () {
//    const classes = useStyles();
    //const theme = useTheme();
    //const {classes, theme} = this.props;

    function menuListItems() {
      return(
        <>
            <ListItem button component={Link} to="/">
              <ListItemIcon>
                <Home/>
              </ListItemIcon>
              <ListItemText>Home</ListItemText>
            </ListItem>
            <ListItem button >
              <ListItemIcon>
                <AssignmentInd/>
              </ListItemIcon>
              <ListItemText>My Tasks</ListItemText>
            </ListItem>
            <ListItem button>
              <ListItemIcon>
                <Assignment/>
              </ListItemIcon>
              <ListItemText>Errands</ListItemText>
            </ListItem>
            <ListItem button component={Link} to="/family">
              <ListItemIcon>
                <SupervisorAccount/>
              </ListItemIcon>
              <ListItemText>Family Members</ListItemText>
            </ListItem>
            <Divider/>
            <ListItem button>
              <ListItemIcon>
                <AccountCircle/>
              </ListItemIcon>
              <ListItemText>My Profile</ListItemText>
            </ListItem>
            <ListItem button>
              <ListItemIcon><ExitToApp></ExitToApp></ListItemIcon>
              <ListItemText>Logout</ListItemText>
            </ListItem>
        </>);
    }

    return (
      <div className={classes.root}>
        <CssBaseline/>
          <AppBar>
            <Toolbar>
              <Hidden smUp implementation="css">
                <IconButton 
                    onClick={toggleSidebar}>
                  <Menu />
                </IconButton>
              </Hidden>
              <Box className={classes.appBarSpacer}/>
              <Typography variant="h4">Pomotr</Typography>
            </Toolbar>
          </AppBar>
          
        <nav className={classes.drawer}>
          <Hidden smUp implementation="css">
            <Drawer
            variant="temporary"
            anchor="left"
            open={showDrawer}
            onClose={toggleSidebar}
            className={classes.drawer}
            classes={{
              paper: classes.drawerPaper,
            }}
            ModalProps={{
              keepMounted: true,
            }}
            >
              <List>
                <ListItem button onClick={toggleSidebar}>
                  <ListItemIcon><Close/></ListItemIcon>
                </ListItem>
                {menuListItems()}
              </List>
            </Drawer>
          </Hidden>
          <Hidden xsDown implementation="css">
            <Drawer
              className={classes.drawer}
              variant="permanent"
              open
              onClose={toggleSidebar}
              classes={{
                paper: classes.drawerPaper,
              }}
              >
              <List>
                {menuListItems()}
              </List>
            </Drawer>
          </Hidden>
        </nav>
        <div className={classes.content}>
          {props.children}
        </div>
      </div>        
    );
//  }
}

/*
                <Grid container>
              <Grid item xs="auto">
              </Grid>
            </Grid>

*/