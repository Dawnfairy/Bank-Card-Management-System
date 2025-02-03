// src/components/CardsList.js
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './CardsList.css';

const CardsList = () => {
  const [creditCards, setCreditCards] = useState([]);
  const [bankCards, setBankCards] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const navigate = useNavigate();

  const apiBase = 'http://localhost:5283/api';

  const fetchCards = async () => {
    try {
      const [creditResponse, bankResponse] = await Promise.all([
        axios.get(`${apiBase}/creditcards/all`),
        axios.get(`${apiBase}/bankcards/all`),
      ]);

      // Kredi kartlarına cardType 2 ekliyoruz
      const fetchedCreditCards = creditResponse.data.map(card => ({ ...card, cardType: 2 }));

      // Banka kartlarına cardType 1 ekliyoruz
      const fetchedBankCards = bankResponse.data.map(card => ({ ...card, cardType: 1 }));

      setCreditCards(fetchedCreditCards);
      setBankCards(fetchedBankCards);
      setLoading(false);
    } catch (err) {
      console.error('Kartları çekerken hata oluştu:', err);
      setError(true);
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCards();
  }, []);

  const handleDeleteCreditCard = async (id) => {
    console.log(`Kredi kartı silme işlemi başlatılıyor: ID=${id}`); // Debugging için eklenen satır
    if (!window.confirm('Bu kredi kartını silmek istediğinize emin misiniz?')) {
      return;
    }

    try {
      await axios.delete(`${apiBase}/creditcards/deleteById/${id}`);
      setCreditCards(prevCards => prevCards.filter(card => card.id !== id));
      alert('Kredi kartı başarıyla silindi!');
    } catch (err) {
      console.error('Kredi kartını silerken hata oluştu:', err);
      alert('Kredi kartını silerken bir hata oluştu.');
    }
  };

  // Banka Kartı Silme Fonksiyonu
  const handleDeleteBankCard = async (id) => {
    console.log(`Banka kartı silme işlemi başlatılıyor: ID=${id}`); // Debugging için eklenen satır
    if (!window.confirm('Bu banka kartını silmek istediğinize emin misiniz?')) {
      return;
    }

    try {
      await axios.delete(`${apiBase}/bankcards/deleteById/${id}`);
      setBankCards(prevCards => prevCards.filter(card => card.id !== id));
      alert('Banka kartı başarıyla silindi!');
    } catch (err) {
      console.error('Banka kartını silerken hata oluştu:', err);
      alert('Banka kartını silerken bir hata oluştu.');
    }
  };

  if (loading) {
    return <p>Yükleniyor...</p>;
  }

  if (error) {
    return <p>Kartlar yüklenirken bir hata oluştu.</p>;
  }

  return (
    <div className="cards-list">
      <h2>Kartlarınız</h2>

      {/* Kredi Kartları Tablosu */}
      <section>
        <h3>Kredi Kartları</h3>
        {creditCards.length === 0 ? (
          <p>Henüz bir kredi kartı eklemediniz.</p>
        ) : (
          <table className="cards-table">
            <thead>
              <tr>
                <th>Kart Numarası</th>
                <th>Kart Sahibi</th>
                <th>Banka Adı</th>
                <th>Son Kullanma Tarihi</th>
                <th>CVV</th>
                <th>Kredi Limiti (TL)</th>
                <th>Kullanılabilir Bakiye (TL)</th>
                <th>Minimum Ödeme (TL)</th>
                <th>Faiz Oranı (%)</th>
                <th>Ekstre Tarihi</th>
                <th>Son Ödeme Tarihi</th>
                <th>Taksit Desteği</th>
                <th>İşlem</th>
              </tr>
            </thead>
            <tbody>
              {creditCards.map(card => (
                <tr key={card.id}>
                  <td>{card.cardNumber}</td>
                  <td>{card.cardHolderName}</td>
                  <td>{card.bankName}</td>
                  <td>{new Date(card.expirationDate).toLocaleDateString()}</td>
                  <td>{card.cvv}</td>
                  <td>{card.creditLimit} TL</td>
                  <td>{card.availableBalance} TL</td>
                  <td>{card.minimumPayment} TL</td>
                  <td>{card.interestRate} %</td>
                  <td>{new Date(card.billingDate).toLocaleDateString()}</td>
                  <td>{new Date(card.dueDate).toLocaleDateString()}</td>
                  <td>{card.installments ? 'Var' : 'Yok'}</td>
                  <td>
                    <button
                      className="edit-button"
                      onClick={() => navigate(`/edit/credit/${card.id}`)}
                    >
                      Düzenle
                    </button>
                    <button
                      className="delete-button"
                      onClick={() => handleDeleteCreditCard(card.id)}
                    >
                      Sil
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>

      {/* Banka Kartları Tablosu */}
      <section>
        <h3>Banka Kartları</h3>
        {bankCards.length === 0 ? (
          <p>Henüz bir banka kartı eklemediniz.</p>
        ) : (
          <table className="cards-table">
            <thead>
              <tr>
                <th>Kart Numarası</th>
                <th>Kart Sahibi</th>
                <th>Banka Adı</th>
                <th>Son Kullanma Tarihi</th>
                <th>CVV</th>
                <th>Hesap Numarası</th>
                <th>IBAN</th>
                <th>Bakiye (TL)</th>
                <th>Günlük Para Çekme Limiti (TL)</th>
                <th>Temassız Ödeme Desteği</th>
                <th>İşlem</th>
              </tr>
            </thead>
            <tbody>
              {bankCards.map(card => (
                <tr key={card.id}>
                  <td>{card.cardNumber}</td>
                  <td>{card.cardHolderName}</td>
                  <td>{card.bankName}</td>
                  <td>{new Date(card.expirationDate).toLocaleDateString()}</td>
                  <td>{card.cvv}</td>
                  <td>{card.accountNumber}</td>
                  <td>{card.iban}</td>
                  <td>{card.balance} TL</td>
                  <td>{card.withdrawalLimit} TL</td>
                  <td>{card.isContactless ? 'Var' : 'Yok'}</td>
                  <td>
                    <button
                      className="edit-button"
                      onClick={() => navigate(`/edit/bank/${card.id}`)}
                    >
                      Düzenle
                    </button>
                    <button
                      className="delete-button"
                      onClick={() => handleDeleteBankCard(card.id)}
                    >
                      Sil
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
};

export default CardsList;
