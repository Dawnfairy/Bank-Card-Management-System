// src/components/AddCard.js
import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './AddCard.css';

const AddCard = () => {
  const navigate = useNavigate();
  const [cardType, setCardType] = useState('');
  const [creditCardData, setCreditCardData] = useState({
    cardNumber: '',
    cardHolderName: '',
    expirationDate: '',
    cvv: '',
    bankName: '',
    creditLimit: '',
    availableBalance: '',
    minimumPayment: '',
    interestRate: '',
    billingDate: '',
    dueDate: '',
    installments: false,
  });

  const [bankCardData, setDebitCardData] = useState({
    cardNumber: '',
    cardHolderName: '',
    expirationDate: '',
    cvv: '',
    bankName: '',
    accountNumber: '',
    IBAN: '',
    balance: '',
    withdrawalLimit: '',
    isContactless: false,
  });

  const handleCardTypeChange = (e) => {
    setCardType(e.target.value);
  };

  const handleCreditCardChange = (e) => {
    const { name, value, type, checked } = e.target;
    setCreditCardData((prevData) => ({
      ...prevData,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const handleDebitCardChange = (e) => {
    const { name, value, type, checked } = e.target;
    setDebitCardData((prevData) => ({
      ...prevData,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    let payload = {};
    if (cardType === 'Credit') {
      payload = {
        ...creditCardData,
        cardType: 2, // CardType.Credit
      };
    } else if (cardType === 'Debit') {
      payload = {
        ...bankCardData,
        cardType: 1, // CardType.Bank
      };
    } else {
      alert('Lütfen kart türünü seçin.');
      return;
    }
    try {

        let endpoint = '';
        if (cardType === 'Credit') {
          endpoint = 'http://localhost:5283/api/creditcards/create';
        } else if (cardType === 'Debit') {
          endpoint = 'http://localhost:5283/api/bankcards/create';
        } else {
          throw new Error('Geçersiz kart tipi');
        }
      const response = await axios.post(endpoint, payload);
      console.log('Kart başarıyla eklendi:', response.data);
      alert('Kart başarıyla eklendi!');
      // Formu sıfırlamak isterseniz:
      setCardType('');
      setCreditCardData({
        cardNumber: '',
        cardHolderName: '',
        expirationDate: '',
        cvv: '',
        bankName: '',
        creditLimit: '',
        availableBalance: '',
        minimumPayment: '',
        interestRate: '',
        billingDate: '',
        dueDate: '',
        installments: false,
      });
      setDebitCardData({
        cardNumber: '',
        cardHolderName: '',
        expirationDate: '',
        cvv: '',
        bankName: '',
        accountNumber: '',
        IBAN: '',
        balance: '',
        withdrawalLimit: '',
        isContactless: false,
      });
      navigate('/'); // Kartlar listesine yönlendirme
    } catch (error) {
      console.error('Kart ekleme hatası:',  error.response ? error.response.data : error.message);
      alert('Kart eklerken bir hata oluştu.');
    }
  };

  return (
    <div className="add-card-container">
      <h2>Kart Ekle</h2>
      <form onSubmit={handleSubmit} className="add-card-form">
        <div className="form-group">
          <label>Kart Türü:</label>
          <select
            name="cardType"
            value={cardType}
            onChange={handleCardTypeChange}
            required
            className="form-control"
          >
            <option value="">Seçiniz</option>
            <option value="Debit">Banka Kartı</option>
            <option value="Credit">Kredi Kartı</option>
          </select>
        </div>

        {cardType === 'Credit' && (
          <div className="card-fields">
            <h3>Kredi Kartı Bilgileri</h3>
            <div className="form-group">
              <label>Kart Numarası:</label>
              <input
                type="text"
                name="cardNumber"
                value={creditCardData.cardNumber}
                onChange={handleCreditCardChange}
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
                value={creditCardData.cardHolderName}
                onChange={handleCreditCardChange}
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
                value={creditCardData.expirationDate}
                onChange={handleCreditCardChange}
                required
                className="form-control"
              />
            </div>
            <div className="form-group">
              <label>CVV:</label>
              <input
                type="text"
                name="cvv"
                value={creditCardData.cvv}
                onChange={handleCreditCardChange}
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
                value={creditCardData.bankName}
                onChange={handleCreditCardChange}
                required
                className="form-control"
                placeholder="Banka adı"
              />
            </div>
            <div className="form-group">
              <label>Kredi Limiti:</label>
              <input
                type="number"
                name="creditLimit"
                value={creditCardData.creditLimit}
                onChange={handleCreditCardChange}
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
                value={creditCardData.availableBalance}
                onChange={handleCreditCardChange}
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
                value={creditCardData.minimumPayment}
                onChange={handleCreditCardChange}
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
                value={creditCardData.interestRate}
                onChange={handleCreditCardChange}
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
                value={creditCardData.billingDate}
                onChange={handleCreditCardChange}
                required
                className="form-control"
              />
            </div>
            <div className="form-group">
              <label>Son Ödeme Tarihi:</label>
              <input
                type="date"
                name="dueDate"
                value={creditCardData.dueDate}
                onChange={handleCreditCardChange}
                required
                className="form-control"
              />
            </div>
            <div className="form-group form-check">
              <input
                type="checkbox"
                name="installments"
                checked={creditCardData.installments}
                onChange={handleCreditCardChange}
                className="form-check-input"
                id="installments"
              />
              <label className="form-check-label" htmlFor="installments">
                Taksit Desteği Var mı?
              </label>
            </div>
          </div>
        )}

        {cardType === 'Debit' && (
          <div className="card-fields">
            <h3>Banka Kartı Bilgileri</h3>
            <div className="form-group">
              <label>Kart Numarası:</label>
              <input
                type="text"
                name="cardNumber"
                value={bankCardData.cardNumber}
                onChange={handleDebitCardChange}
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
                value={bankCardData.cardHolderName}
                onChange={handleDebitCardChange}
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
                value={bankCardData.expirationDate}
                onChange={handleDebitCardChange}
                required
                className="form-control"
              />
            </div>
            <div className="form-group">
              <label>CVV:</label>
              <input
                type="text"
                name="cvv"
                value={bankCardData.cvv}
                onChange={handleDebitCardChange}
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
                value={bankCardData.bankName}
                onChange={handleDebitCardChange}
                required
                className="form-control"
                placeholder="Banka adı"
              />
            </div>
            <div className="form-group">
              <label>Hesap Numarası:</label>
              <input
                type="text"
                name="accountNumber"
                value={bankCardData.accountNumber}
                onChange={handleDebitCardChange}
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
                value={bankCardData.iban}
                onChange={handleDebitCardChange}
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
                value={bankCardData.balance}
                onChange={handleDebitCardChange}
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
                value={bankCardData.withdrawalLimit}
                onChange={handleDebitCardChange}
                required
                className="form-control"
                placeholder="Örneğin: 500"
              />
            </div>
            <div className="form-group form-check">
              <input
                type="checkbox"
                name="isContactless"
                checked={bankCardData.isContactless}
                onChange={handleDebitCardChange}
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
          Kaydet
        </button>
      </form>
    </div>
  );
};

export default AddCard;