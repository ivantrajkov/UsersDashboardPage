import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter as Router, Routes, Route, BrowserRouter } from 'react-router-dom';
import AuthProvider from './security/AuthProvider.jsx';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
    <AuthProvider>
      <App />
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>,
)
