import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../contexts/AuthContext';

const ProtectedRoute = ({ children }) => {
    const { auth } = useContext(AuthContext);

    // E�er token yoksa kullan�c� login sayfas�na y�nlendiriliyor.
    if (!auth.token) {
        return <Navigate to="/" replace />;
    }

    return children;
};

export default ProtectedRoute;