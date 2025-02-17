import React, { useContext } from 'react';
import { AuthContext } from '../contexts/AuthContext';
import { Navigate } from 'react-router-dom';

const PublicRoute = ({ children }) => {
    const { auth } = useContext(AuthContext);

    if (auth.token) {
        return <Navigate to="/cards" replace />;
    }

    return children;
};

export default PublicRoute;
