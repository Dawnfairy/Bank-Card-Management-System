import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './AddCard.css';
import endpoints from '../api/apiEndpoint';
import apiClient from '../api/apiClient';

const AddCard = () => {
    const navigate = useNavigate();
    const [cardType, setCardType] = useState('');

    const [creditCardData, setCreditCardData] = useState({
        CardNumber: '',
        CardHolderName: '',
        ExpirationDate: '',
        CVV: '',
        BankName: '',
        CreditLimit: '',
        AvailableBalance: '',
        MinimumPayment: '',
        InterestRate: '',
        BillingDate: '',
        DueDate: '',
        Installments: false,
    });

    const [bankCardData, setBankCardData] = useState({
        CardNumber: '',
        CardHolderName: '',
        ExpirationDate: '',
        CVV: '',
        BankName: '',
        AccountNumber: '',
        IBAN: '',
        Balance: '',
        WithdrawalLimit: '',
        IsContactless: false,
    });

    // Türkçe etiket eşleştirmeleri
    const creditCardLabels = {
        CardNumber: "Kart Numarası",
        CardHolderName: "Kart Sahibi Adı",
        ExpirationDate: "Son Kullanma Tarihi",
        CVV: "CVV",
        BankName: "Banka Adı",
        CreditLimit: "Kredi Limiti",
        AvailableBalance: "Kullanılabilir Bakiye",
        MinimumPayment: "Minimum Ödeme",
        InterestRate: "Faiz Oranı",
        BillingDate: "Ekstre Tarihi",
        DueDate: "Son Ödeme Tarihi",
        Installments: "Taksit Desteği"
    };

    const bankCardLabels = {
        CardNumber: "Kart Numarası",
        CardHolderName: "Kart Sahibi Adı",
        ExpirationDate: "Son Kullanma Tarihi",
        CVV: "CVV",
        BankName: "Banka Adı",
        AccountNumber: "Hesap Numarası",
        IBAN: "IBAN",
        Balance: "Bakiye",
        WithdrawalLimit: "Günlük Para Çekme Limiti",
        IsContactless: "Temassız Ödeme Desteği"
    };

    const handleCardTypeChange = (e) => setCardType(e.target.value);

    // Genel input değişimi
    const handleInputChange = (e, setState) => {
        const { name, value, type, checked } = e.target;
        setState((prevData) => ({
            ...prevData,
            [name]: type === 'checkbox' ? checked : value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!cardType) {
            alert('Lütfen kart türünü seçin.');
            return;
        }
        const isCredit = cardType === 'Credit';

        const payload = isCredit
            ? { ...creditCardData, cardType: 2 } // 2: Kredi Kartı
            : { ...bankCardData, cardType: 1 };   // 1: Banka Kartı

        const endpoint = isCredit
            ? endpoints.creditcards.create
            : endpoints.bankcards.create;

        try {
            const response = await apiClient.post(endpoint, payload);
            console.log('Kart başarıyla eklendi:', response.data.data);
            alert('Kart başarıyla eklendi!');

            // Form sıfırlama işlemi
            setCardType('');
            setCreditCardData({
                CardNumber: '',
                CardHolderName: '',
                ExpirationDate: '',
                CVV: '',
                BankName: '',
                CreditLimit: '',
                AvailableBalance: '',
                MinimumPayment: '',
                InterestRate: '',
                BillingDate: '',
                DueDate: '',
                Installments: false,
            });
            setBankCardData({
                CardNumber: '',
                CardHolderName: '',
                ExpirationDate: '',
                CVV: '',
                BankName: '',
                AccountNumber: '',
                IBAN: '',
                Balance: '',
                WithdrawalLimit: '',
                IsContactless: false,
            });
            navigate('/cards'); // Kartlar listesine yönlendirme
        } catch (error) {
            console.error('Kart ekleme hatası:', error.response ? error.response.data : error.message);
            alert('Kart eklerken bir hata oluştu.');
        }
    };

    const getInputType = (key) => {
        const lower = key.toLowerCase();
        if (lower.includes('date')) {
            return 'date';
        }
        else if (lower.includes('limit') || lower.includes('balance') || lower.includes('payment') || lower.includes('rate') || lower.includes('withdrawal')) {
            return 'number';
        }
        return 'text';
    };


    const currentCardData = cardType === 'Credit' ? creditCardData : bankCardData;
    const setCurrentCardData = cardType === 'Credit' ? setCreditCardData : setBankCardData;
    const currentLabels = cardType === 'Credit' ? creditCardLabels : bankCardLabels;

    return (
        <div className="add-card-container">
            <h2>Kart Ekle</h2>
            <form onSubmit={handleSubmit} className="add-card-form">
                <div className="form-group">
                    <label>Kart Türü:</label>
                    <select value={cardType} onChange={handleCardTypeChange} required className="form-control">
                        <option value="">Seçiniz</option>
                        <option value="Bank">Banka Kartı</option>
                        <option value="Credit">Kredi Kartı</option>
                    </select>
                </div>

                {cardType && (
                    <div className="card-fields">
                        <h3>{cardType === 'Credit' ? 'Kredi Kartı Bilgileri' : 'Banka Kartı Bilgileri'}</h3>
                        {Object.keys(currentCardData).map((key) => (
                            <div key={key} className="form-group">
                                <label>{currentLabels[key]}:</label>
                                {typeof currentCardData[key] === 'boolean' ? (
                                    <input
                                        type="checkbox"
                                        name={key}
                                        checked={currentCardData[key]}
                                        onChange={(e) => handleInputChange(e, setCurrentCardData)}
                                        className="form-check-input"
                                    />
                                ) : (
                                    <input
                                        type={
                                            getInputType(
                                                key
                                            )
                                        }
                                        name={key}
                                        value={currentCardData[key]}
                                        onChange={(e) => handleInputChange(e, setCurrentCardData)}
                                        required
                                        className="form-control"
                                        placeholder={currentLabels[key]}
                                    />
                                )}
                            </div>
                        ))}
                    </div>
                )}

                <button type="submit" className="btn btn-primary">Kaydet</button>
            </form>
        </div>
    );
};

export default AddCard;