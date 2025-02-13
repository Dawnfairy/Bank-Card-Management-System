// src/components/Login.js
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import { AuthContext } from '../contexts/AuthContext';
import './Login.css';
import { login as loginApi, getRolPermissionsById } from '../api/apiService';

const Login = () => {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const { loginStorage } = useContext(AuthContext);


    const handleLogin = async (e) => {
        e.preventDefault();
        setError('');

        try {
            // Login API çaðrýsýný dýþarýda tanýmlý response deðiþkenine atýyoruz.
            let response;
            try {
                response = await loginApi({ userName, password });
                console.log('response:', response);
            } catch (unauthError) {
                throw new Error('Kullanici adi veya sifre hatali.');
            }

            const { token, role } = response.data;
            if (!token) {
                throw new Error('Token alinamadi.'); 
            }

            // Ýzin bilgilerini çekiyoruz.
            let permissionsResponse;
            try {
                permissionsResponse = await getRolPermissionsById(role);
            } catch (innerError) {
                throw new Error('Yetkisiz giris: izin bilgilerine ulasilamadi.');
            }

            console.log('permissionsResponse:', permissionsResponse);
            const permissions = permissionsResponse.data;
            if (!permissions) {
                throw new Error('Yetkisiz giris: izin bilgileri bos.');
            }
            console.log('permissions:', permissions);

            // Oturum bilgilerini güncelliyoruz.
            loginStorage(token, role, permissions, userName);
            console.log('role:', role);
            console.log('token:', token);

            navigate('/cards');
        } catch (err) {
            console.error('Giris hatasi:', err);
            setError(err.message || 'Kullanici adi veya sifre hatali.');
        }
    };

    return (
        <div className="login-container">
            <h2>Giris Yap</h2>
            {error && <p className="error-message">{error}</p>}
            <form onSubmit={handleLogin} className="login-form">
                <div className="form-group">
                    <label htmlFor="userName">Kullanici Adi</label>
                    <input
                        type="text"
                        id="userName"
                        placeholder="Kullanici Adi"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="password">Sifre</label>
                    <input
                        type="password"
                        id="password"
                        placeholder="Sifre"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" className="login-button">
                    Giris Yap
                </button>
            </form>
        </div>
    );
};

export default Login;