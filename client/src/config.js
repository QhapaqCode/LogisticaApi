const config = {
  SERVER_URL: process.env.REACT_APP_ENV_SERVER_URL || 'http://localhost:8087'
};

console.log('SERVER_URL', config.SERVER_URL);

export default config;