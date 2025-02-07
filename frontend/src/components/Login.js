// login.js
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

function Login() {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        setError('');

        const data = {
            userName,
            password,
        };

        try {
            const response = await axios.post('http://localhost:5283/login', data, {
                headers: { 'Content-Type': 'application/json' },
            });
            console.log("Response data:", response.data);

            const { token } = response.data;
            //console.log(token, 'Token al�nd�:', token);

            if (token) {
                //console.log(token, 'Token al�nd�:', token);

                console.log(token, 'Token al�nd�:', token);
                // JWT token'� localStorage'e kaydet
                localStorage.setItem('token', token);
                // Giri� ba�ar�l� ise, ana sayfaya (�rne�in "/" yoluna) y�nlendir
                navigate('/cards');
            } else {
                throw new Error('Token al�namad�');
            }
        } catch (err) {
            console.error('Giri� hatas�:', err);
            setError('Kullan�c� ad� veya �ifre hatal�.');
        }
    };

    return (
        <div className="login-container">
            <h2>Giri� Yap</h2>
            {error && <p className="error-message">{error}</p>}
            <form onSubmit={handleLogin}>
                <div>
                    <label htmlFor="userName">Kullan�c� Ad�:</label>
                    <input
                        type="text"
                        id="userName"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">�ifre:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Giri� Yap</button>
            </form>
        </div>
    );
}

export default Login;
