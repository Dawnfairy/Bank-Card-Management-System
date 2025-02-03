// src/components/EditCard.js
import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './EditCard.css';

const EditCard = () => {
  const { cardType, id } = useParams(); // cardType parametresini de alıyoruz
  const navigate = useNavigate();
  const [card, setCard] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  const fetchCard = async () => {
    try {
      let response;
      if (cardType === 'credit') {
        response = await axios.get(`http://localhost:5283/api/creditcards/byId/${id}`);
      } else if (cardType === 'bank') {
        response = await axios.get(`http://localhost:5283/api/bankcards/byId/${id}`);
      } else {
        throw new Error('Geçersiz kart türü');
      }
      setCard(response.data);
      setLoading(false);
    } catch (err) {
      console.error('Kartı çekerken hata oluştu:', err);
      setError(true);
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCard();
  }, [cardType, id]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setCard((prevCard) => ({
      ...prevCard,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (cardType === 'credit') {
        await axios.put(`http://localhost:5283/api/creditcards/updateById/${id}`, card);
      } else if (cardType === 'bank') {
        await axios.put(`http://localhost:5283/api/bankcards/updateById/${id}`, card);
      }
      alert('Kart başarıyla güncellendi!');
      navigate('/');
    } catch (err) {
      console.error('Kartı güncellerken hata oluştu:', err);
      alert('Kartı güncellerken bir hata oluştu.');
    }
  };

  if (loading) {
    return <p>Yükleniyor...</p>;
  }

  if (error || !card) {
    return <p>Kart bilgisi bulunamadı.</p>;
  }

  return (
    <div className="edit-card-container">
      <h2>Kart Düzenle</h2>
      <form onSubmit={handleSubmit} className="edit-card-form">
        {/* Kart Türünü Görüntüleme */}
        <div className="form-group">
          <label>Kart Türü:</label>
          <span className="card-type-label">
            {cardType === 'credit' ? 'Kredi Kartı' : 'Banka Kartı'}
          </span>
        </div>

        {/* Ortak Alanlar */}
        <div className="form-group">
          <label>Kart Numarası:</label>
          <input
            type="text"
            name="cardNumber"
            value={card.cardNumber}
            onChange={handleChange}
            required
            maxLength="16"
            className="form-control"
            placeholder="16 haneli kart numarası"
          />
        </div>
        <div className="form-group">
          <label>Kart Sahibi Adı:</label>
          <input
            type="text"
            name="cardHolderName"
            value={card.cardHolderName}
            onChange={handleChange}
            required
            className="form-control"
            placeholder="Kart sahibi adı"
          />
        </div>
        <div className="form-group">
          <label>Son Kullanma Tarihi:</label>
          <input
            type="date"
            name="expirationDate"
            value={new Date(card.expirationDate).toISOString().split('T')[0]}
            onChange={handleChange}
            required
            className="form-control"
          />
        </div>
        <div className="form-group">
          <label>CVV:</label>
          <input
            type="text"
            name="cvv"
            value={card.cvv}
            onChange={handleChange}
            required
            maxLength="4"
            className="form-control"
            placeholder="3-4 haneli güvenlik kodu"
          />
        </div>
        <div className="form-group">
          <label>Banka Adı:</label>
          <input
            type="text"
            name="bankName"
            value={card.bankName}
            onChange={handleChange}
            required
            className="form-control"
            placeholder="Banka adı"
          />
        </div>

        {/* Kredi Kartı Alanları */}
        {cardType === 'credit' && (
          <div className="card-fields">
            <h3>Kredi Kartı Bilgileri</h3>
            <div className="form-group">
              <label>Kredi Limiti:</label>
              <input
                type="number"
                name="creditLimit"
                value={card.creditLimit}
                onChange={handleChange}
                required
                className="form-control"
                placeholder="Örneğin: 5000"
              />
            </div>
            <div className="form-group">
              <label>Kullanılabilir Bakiye:</label>
              <input
                type="number"
                name="availableBalance"
                value={card.availableBalance}
                onChange={handleChange}
                required
                className="form-control"
                placeholder="Örneğin: 2500"
              />
            </div>
            <div className="form-group">
              <label>Minimum Ödeme:</label>
              <input
                type="number"
                name="minimumPayment"
                value={card.minimumPayment}
                onChange={handleChange}
                required
                className="form-control"
                placeholder="Örneğin: 100"
              />
            </div>
            <div className="form-group">
              <label>Faiz Oranı (%):</label>
              <input
                type="number"
                name="interestRate"
                value={card.interestRate}
                onChange={handleChange}
                required
                step="0.01"
                className="form-control"
                placeholder="Örneğin: 1.5"
              />
            </div>
            <div className="form-group">
              <label>Ekstre Tarihi:</label>
              <input
                type="date"
                name="billingDate"
                value={new Date(card.billingDate).toISOString().split('T')[0]}
                onChange={handleChange}
                required
                className="form-control"
              />
            </div>
            <div className="form-group">
              <label>Son Ödeme Tarihi:</label>
              <input
                type="date"
                name="dueDate"
                value={new Date(card.dueDate).toISOString().split('T')[0]}
                onChange={handleChange}
                required
                className="form-control"
              />
            </div>
            <div className="form-group form-check">
              <input
                type="checkbox"
                name="installments"
                checked={card.installments}
                onChange={handleChange}
                className="form-check-input"
                id="installments"
              />
              <label className="form-check-label" htmlFor="installments">
                Taksit Desteği Var mı?
              </label>
            </div>
          </div>
        )}

        {/* Banka Kartı Alanları */}
        {cardType === 'bank' && (
          <div className="card-fields">
            <h3>Banka Kartı Bilgileri</h3>
            <div className="form-group">
              <label>Hesap Numarası:</label>
              <input
                type="text"
                name="accountNumber"
                value={card.accountNumber}
                onChange={handleChange}
                required
                className="form-control"
                placeholder="Hesap numarası"
              />
            </div>
            <div className="form-group">
              <label>IBAN:</label>
              <input
                type="text"
                name="IBAN"
                value={card.iban}
                onChange={handleChange}
                required
                className="form-control"
                placeholder="IBAN numarası"
              />
            </div>
            <div className="form-group">
              <label>Bakiye:</label>
              <input
                type="number"
                name="balance"
                value={card.balance}
                onChange={handleChange}
                required
                className="form-control"
                placeholder="Örneğin: 1500"
              />
            </div>
            <div className="form-group">
              <label>Günlük Para Çekme Limiti:</label>
              <input
                type="number"
                name="withdrawalLimit"
                value={card.withdrawalLimit}
                onChange={handleChange}
                required
                className="form-control"
                placeholder="Örneğin: 500"
              />
            </div>
            <div className="form-group form-check">
              <input
                type="checkbox"
                name="isContactless"
                checked={card.isContactless}
                onChange={handleChange}
                className="form-check-input"
                id="isContactless"
              />
              <label className="form-check-label" htmlFor="isContactless">
                Temassız Ödeme Desteği Var mı?
              </label>
            </div>
          </div>
        )}

        <button type="submit" className="btn btn-primary">
          Güncelle
        </button>
      </form>
    </div>
  );
};

export default EditCard;
