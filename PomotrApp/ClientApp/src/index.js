import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { AuthenticationProvider, oidcLog } from '@axa-fr/react-oidc-context';
import App from './App';
//import registerServiceWorker from './registerServiceWorker';
import { createMuiTheme, ThemeProvider } from '@material-ui/core/styles';
import oidcConfiguration from './configuration';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const theme = createMuiTheme({
  palette: {
    primary: {
      main: '#E0F3B2'
    },
    secondary: {
      main: '#59C3C0'
    },
    canvas: {
      main: '#E8C916'
    }
    //D0D2A5
    //E8C916
    //A77A64
  },
  typography: {
    fontFamily: [
      '-apple-system',
      'BlinkMacSystemFont',
      '"Segoe UI"',
      'Roboto',
      '"Helvetica Neue"',
      'Arial',
      'sans-serif',
      '"Apple Color Emoji"',
      '"Segoe UI Emoji"',
      '"Segoe UI Symbol"',
    ].join(','),
  },
});

ReactDOM.render(
  <ThemeProvider theme={theme}>
    <BrowserRouter basename={baseUrl}>
      <AuthenticationProvider configuration={oidcConfiguration} loggerLevel={oidcLog.DEBUG}>
        <App />
      </AuthenticationProvider>
    </BrowserRouter>
  </ThemeProvider>,
  rootElement);

// Uncomment the line above that imports the registerServiceWorker function
// and the line below to register the generated service worker.
// By default create-react-app includes a service worker to improve the
// performance of the application by caching static assets. This service
// worker can interfere with the Identity UI, so it is
// disabled by default when Identity is being used.
//
//registerServiceWorker();

