import React from 'react';
import { Routes, Route, BrowserRouter, Navigate } from 'react-router-dom';
import './App.css';
import SignIn from './components/signIn'; // Assuming SignIn component is created
import SignUp from './components/signup';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/signin" />} />
         <Route path="/signup" element={<SignUp />} />
        <Route path="/signin" element={<SignIn />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
