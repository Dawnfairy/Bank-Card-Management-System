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
            //console.log(token, 'Token alýndý:', token);

            if (token) {
                //console.log(token, 'Token alýndý:', token);

                console.log(token, 'Token alýndý:', token);
                // JWT token'ý localStorage'e kaydet
                localStorage.setItem('token', token);
                // Giriþ baþarýlý ise, ana sayfaya (örneðin "/" yoluna) yönlendir
                navigate('/cards');
            } else {
                throw new Error('Token alýnamadý');
            }
        } catch (err) {
            console.error('Giriþ hatasý:', err);
            setError('Kullanýcý adý veya þifre hatalý.');
        }
    };

    return (
        <div className="login-container">
            <h2>Giriþ Yap</h2>
            {error && <p className="error-message">{error}</p>}
            <form onSubmit={handleLogin}>
                <div>
                    <label htmlFor="userName">Kullanýcý Adý:</label>
                    <input
                        type="text"
                        id="userName"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Þifre:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Giriþ Yap</button>
            </form>
        </div>
    );
}

export default Login;
