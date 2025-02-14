import React from 'react';
import { GoogleLogin } from '@react-oauth/google';
import axios from 'axios';
import config from '../config';

const GoogleLoginComponent = () => {
  const handleLoginSuccess = (response) => {
    console.log('Login Success:', response);
    const token = response.credential;
    axios.post(`${config.SERVER_URL}/api/auth/google`, { token })
      .then((res) => {
        console.log('Token sent to backend:', res);
        // Handle the login success, e.g., send the token to the backend
      })
      .catch((err) => {
        console.log('Error sending token to backend:', err);
      });
  };

  const handleLoginError = (error) => {
    console.log('Login Error:', error);
    // Handle the login error
    alert('Login failed. Please try again.');
  };

  return (
    <div>
      <h2>Google Login</h2>
      <GoogleLogin
        onSuccess={handleLoginSuccess}
        onError={handleLoginError}
      />
    </div>
  );
};

export default GoogleLoginComponent;
