import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Home.css';

const Home = () => {
    const navigate = useNavigate();

    return (
        <div className="home-container">
            <header className="home-header">
                <div className="header-content">
                    <h1>Bank Kart Uygulamaniza Hos Geldiniz</h1>
                    <p>
                        Guvenli ve hizli islemlerle kartlarinizi yonetin, finansal ozgurlugunuzu kesfedin.
                    </p>
                    <button
                        className="login-button"
                        onClick={() => navigate('/login')}
                    >
                        Giris Yap
                    </button>
                </div>
            </header>
            <section className="features">
                <div className="feature">
                    <h2>Guvenli Islemler</h2>
                    <p>
                        En son guvenlik teknolojileri ile kart islemleriniz her zaman koruma altinda.
                    </p>
                </div>
                <div className="feature">
                    <h2>Aninda Bildirim</h2>
                    <p>
                        Gercek zamanli bildirimler sayesinde tum kart hareketlerinizi aninda takip edin.
                    </p>
                </div>
                <div className="feature">
                    <h2>Kolay Yonetim</h2>
                    <p>
                        Kartlarinizi kolayca ekleyin, silin ve yoneterek finansal hayatinizi duzenleyin.
                    </p>
                </div>
            </section>
            <footer className="home-footer">
                <p>&copy; 2025 BankKart. Tum haklari saklidir.</p>
            </footer>
        </div>
    );
};

export default Home;
