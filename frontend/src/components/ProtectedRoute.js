import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../contexts/AuthContext';

const ProtectedRoute = ({ children }) => {
    const { auth } = useContext(AuthContext);

    // Eðer token yoksa kullanýcý login sayfasýna yönlendiriliyor.
    if (!auth.token) {
        return <Navigate to="/" replace />;
    }

    return children;
};

export default ProtectedRoute;