// src/components/Login.js
import React, { useState, useEffect } from 'react';
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

    const [loginCompleted, setLoginCompleted] = useState(false);
    const { auth, loginStorage, hasPermission } = useContext(AuthContext);

    useEffect(() => {
        if (loginCompleted) {

            if (!hasPermission('AuthController', 'Login')) {
                alert('Giris yapmak icin yetkiniz yok.');
            } else {
            navigate('/cards');
            }
        }
    }, [auth, loginCompleted, hasPermission, navigate]);

    const handleLogin = async (e) => {
        e.preventDefault();
        setError('');

        try {
            const response = await loginApi({ userName, password });


            if (!response || response.error || !response.data || !response.data.data) {
                setError(response?.data?.Message || 'Giriþ baþarýsýz: Geçersiz yanýt.');
                return;
            }


            const { token, role } = response.data.data;
            if (!token) {
                setError('Token alýnamadý.');
                return;
            }

            console.log('Token:', token);
            console.log('Role:', role);

            const permissionsResponse = await getRolPermissionsById(role);


            if (!permissionsResponse || !permissionsResponse.data || !Array.isArray(permissionsResponse.data.data)) {
                setError(permissionsResponse?.data?.message || 'Yetkisiz giriþ: izin bilgilerine ulaþýlamadý.');
                return;
            }


            const permissions = permissionsResponse.data.data;

            loginStorage(token, role, permissions, userName);

            setLoginCompleted(true);

        } catch (err) {
            if (err.response && err.response.status === 401) {
                setError(err.response.data.Message || 'Yetkisiz eriþim.');
            } else {
                setError(err.message || 'Kullanýcý adý veya þifre hatalý.');
            }
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