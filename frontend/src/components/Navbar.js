// src/components/Navbar.js
import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';

const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="navbar-container">
        <Link to="/" className="navbar-logo">
          BankKart
        </Link>
        <ul className="navbar-menu">
          <li>
            <Link to="/cards">Kartlar</Link>
          </li>
          <li>
            <Link to="/add">Kart Ekle</Link>
          </li>
        </ul>
      </div>
    </nav>
  );
};

export default Navbar;
