import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import AddCard from './components/AddCard';
import CardsList from './components/CardsList';
import EditCard from './components/EditCard';
import './App.css';

function App() {
  return (
    <Router>
    <Navbar />
    <div className="container">
      <Routes>
        <Route path="/" element={<CardsList />} />
        <Route path="/add" element={<AddCard />} />
        <Route path="/edit/:cardType/:id" element={<EditCard />} />
      </Routes>
    </div>
  </Router>
  );
}

export default App;
