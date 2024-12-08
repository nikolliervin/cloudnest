import React from 'react';
import { Routes, Route, BrowserRouter, Navigate } from 'react-router-dom';
import './App.css';
import SignIn from './components/signIn'; // Assuming SignIn component is created

function App() {
  return (
    <BrowserRouter>
      {/* Define routes */}
      <Routes>
        {/* Redirect root path (/) to /signin */}
        <Route path="/" element={<Navigate to="/signin" />} />
        
        {/* Define route for sign-in page */}
        <Route path="/signin" element={<SignIn />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
