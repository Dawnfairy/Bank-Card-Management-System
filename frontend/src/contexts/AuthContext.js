// contexts/AuthContext.js
import React, { createContext, useState } from 'react';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {


    const [auth, setAuth] = useState({
        token: sessionStorage.getItem('token') || null,
        role: sessionStorage.getItem('role') || null,
        permissions: JSON.parse(sessionStorage.getItem('permissions')) || [],
        userName: sessionStorage.getItem('userName') || null
    });


    const loginStorage = (token, role, permissions, userName) => {
        sessionStorage.setItem('token', token);
        sessionStorage.setItem('role', role);
        sessionStorage.setItem('permissions', JSON.stringify(permissions));
        sessionStorage.setItem('userName', userName);
        setAuth({ token, role, permissions, userName });
    };


    const logoutStorage = () => {
        sessionStorage.clear();
        setAuth({ token: null, role: null, permissions: [], userName: null });
    };

    // Ýzin kontrolü için yardýmcý fonksiyon.
    const hasPermission = (controllerName, actionName) => {
        return auth.permissions.some(
            perm =>
                perm.controllerName === controllerName &&
                perm.actionName === actionName
        );
    };

    return (
        <AuthContext.Provider value={{ auth, loginStorage, logoutStorage, hasPermission }}>
            {children}
        </AuthContext.Provider>
    );
};


