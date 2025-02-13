// src/components/Navbar.js
import React, { useContext, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';
import { AuthContext } from '../contexts/AuthContext';
import { logout } from '../api/apiService';

const Navbar = () => {

    const { auth, hasPermission, logoutStorage } = useContext(AuthContext); // Context üzerinden auth bilgilerine ulaþýn.
    const navigate = useNavigate();
    // Context'ten gelen kullanýcý adý, yoksa varsayýlan "Kullanýcý"
    const userName = auth.userName || "xxx";
    let welcomeMessage = "";

    if (auth.role === "0" || auth.role === 0) {
        welcomeMessage = `Hosgeldiniz Sube ${userName}`;
    } else if (auth.role === "1" || auth.role === 1) {
        welcomeMessage = `Hosgeldiniz Admin ${userName}`;
    } else {
        welcomeMessage = `Hosgeldiniz ${userName}`;
    }
    const handleLogout = async () => {
        try {
            const response = await logout(userName);
            console.log(response.data);
            console.log('response:', response);
            logoutStorage();
            navigate('/login');
        } catch (error) {
            console.error('Cikis hatasi:', error);
        }

    };

    return (
        <nav className="navbar">
            <div className="navbar-container">
                <Link className="navbar-logo">
                    BankKart
                </Link>
                <ul className="navbar-menu">
                    {
                        hasPermission('BankCardController', 'GetAll') &&
                        hasPermission('CreditCardController', 'GetAll') && (
                            <li>
                                <Link to="/cards">Kartlar</Link>
                            </li>
                        )}
                    {
                        hasPermission('BankCardController', 'Create') &&
                        hasPermission('CreditCardController', 'Create') && (
                            <li>
                                <Link to="/add">Kart Ekle</Link>
                            </li>
                        )}
                </ul>
                {auth.token && (
                    <div className="navbar-welcome">
                        <span>{welcomeMessage}</span>
                        <button className="logout-button" onClick={handleLogout}>
                            Cikis Yap
                        </button>

                    </div>
                )}
            </div>
        </nav>
    );
};

export default Navbar;
