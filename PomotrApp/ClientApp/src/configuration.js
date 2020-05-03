const configuration = {
    client_id: 'implicit',
    redirect_uri: 'http://localhost:5001/authentication/callback',
    response_type: 'id_token token',
    post_logout_redirect_uri: 'http://localhost:5001/',
    scope: 'openid profile email',
    authority: 'https://demo.identityserver.io',
    silent_redirect_uri: 'http://localhost:5001/authentication/silent_callback',
    automaticSilentRenew: true,
    loadUserInfo: true,
  };
  
  export default configuration;