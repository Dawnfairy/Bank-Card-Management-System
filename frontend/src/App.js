import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import Navbar from './components/Navbar';
import AddCard from './components/AddCard';
import CardsList from './components/CardsList';
import EditCard from './components/EditCard';
import Login from './components/Login';
import ProtectedRoute from './components/ProtectedRoute';
import PublicRoute from './components/PublicRoute';
import Home from './components/Home';
import './App.css';

function App() {
    return (
        <AuthProvider>
            <Router>
                <Navbar />
                <div className="container">
                    <Routes>
                        <Route path="/"
                            element={
                                <PublicRoute>

                                    <Home />
                                </PublicRoute>

                               } />
                        <Route path="/login"
                            element={
                                <PublicRoute>
                                    <Login />
                                    </PublicRoute>
                                } />
                        <Route path="/cards"
                            element={
                                <ProtectedRoute>
                                    <CardsList />
                                </ProtectedRoute>} />
                        <Route path="/add"
                            element={<ProtectedRoute>
                                <AddCard />
                            </ProtectedRoute>} />
                        <Route path="/edit/:cardType/:id"
                            element={
                                <ProtectedRoute>
                                    <EditCard />
                                </ProtectedRoute>} />
                    </Routes>
                </div>
            </Router>
        </AuthProvider>
    );
}

export default App;
