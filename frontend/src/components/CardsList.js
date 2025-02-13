
import React, { useEffect, useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../contexts/AuthContext';
import { deleteBankCardById, deleteCreditCardById, getAllBankCards, getAllCreditCards } from '../api/apiService';
import './CardsList.css';

const CardsList = () => {
    const [creditCards, setCreditCards] = useState([]);
    const [bankCards, setBankCards] = useState([]);
    const [cardsLoading, setCardsLoading] = useState(true);
    const [creditError, setCreditError] = useState(null);
    const [bankError, setBankError] = useState(null);
    const navigate = useNavigate();

    const { auth, hasPermission } = useContext(AuthContext); // Context üzerinden auth bilgilerine ulaşın.
    const { permissions } = auth;
    const authLoading = false;

    const fetchCards = async () => {
        try {
            setCardsLoading(true);
            setCreditError(null);
            setBankError(null);

            const [creditResponse, bankResponse] = await Promise.allSettled([
                getAllCreditCards(),
                getAllBankCards()
            ]);

            if (creditResponse.status === "fulfilled") {
                console.log("kredi kartları:", creditResponse.value.data);
                const fetchedCreditCards = creditResponse.value.data.map(card =>
                ({
                    ...card,
                    cardType: 2
                }));
                setCreditCards(fetchedCreditCards);

            } else {
                setCreditError(
                    creditResponse.reason.response?.data?.Message || "Bilinmeyen bir hata oluştu."
                );
                setCreditCards([]);
            }


            if (bankResponse.status === "fulfilled") {
                console.log("banka kartları:", bankResponse.value.data);
                const fetchedBankCards = bankResponse.value.data.map(card =>
                ({
                    ...card,
                    cardType: 1
                }));
                setBankCards(fetchedBankCards);

            } else {
                setBankError(
                    bankResponse.reason.response?.data?.Message || "Bilinmeyen bir hata oluştu."
                );
                setBankCards([]);
            }
        } catch (err) {
            setCreditError("Veriler yüklenirken genel bir hata oluştu.");
            setBankError("Veriler yüklenirken genel bir hata oluştu.");
            console.error('Genel hata:', err);
        } finally {
            setCardsLoading(false);
        }
    };

    useEffect(() => {


        if (!authLoading) {
            if (
                permissions.some(p =>
                    p.controllerName === 'CreditCardController' && p.actionName === 'GetAll'
                ) ||
                permissions.some(p =>
                    p.controllerName === 'BankCardController' && p.actionName === 'GetAll'
                )) {
                fetchCards();
            } else {
                alert('Bu işlemi gerçekleştirmek için yetkiniz bulunmamaktadır.');
                setCardsLoading(false);
            }
        }
    }, [permissions, authLoading]);
    

     
    const handleDeleteCreditCard = async (id) => {
        if (!window.confirm('Bu kredi kartını silmek istediğinize emin misiniz?')) {
            return;
        }

        try {
            await deleteCreditCardById(id);
            setCreditCards(prevCards => prevCards.filter(card => card.id !== id));
            alert('Kredi kartı başarıyla silindi!');
        } catch (err) {
            console.error('Kredi kartını silerken hata oluştu:', err);
            alert('Kredi kartını silerken bir hata oluştu.');
        }
    };

    const handleDeleteBankCard = async (id) => {
        if (!window.confirm('Bu banka kartını silmek istediğinize emin misiniz?')) {
            return;
        }

        try {
            await deleteBankCardById(id);
            setBankCards(prevCards => prevCards.filter(card => card.id !== id));
            alert('Banka kartı başarıyla silindi!');
        } catch (err) {
            console.error('Banka kartını silerken hata oluştu:', err);
            alert('Banka kartını silerken bir hata oluştu.');
        }
    };

    if (authLoading || cardsLoading) {
        return <p>Yükleniyor...</p>;
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
                                        {hasPermission('CreditCardController', 'GetById') && (
                                            <button
                                                className="edit-button"
                                                onClick={() => navigate(`/edit/credit/${card.id}`)}
                                            >
                                                Düzenle
                                            </button>
                                        )}
                                        {hasPermission('CreditCardController', 'DeleteById') && (
                                            <button
                                                className="delete-button"
                                                onClick={() => handleDeleteCreditCard(card.id)}
                                            >
                                                Sil
                                            </button>
                                        )}
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
                                        {hasPermission('BankCardController', 'GetById') && (
                                            <button
                                                className="edit-button"
                                                onClick={() => navigate(`/edit/bank/${card.id}`)}
                                            >
                                                Düzenle
                                            </button>
                                        )}
                                        {hasPermission('BankCardController', 'DeleteById') && (
                                            <button
                                                className="delete-button"
                                                onClick={() => handleDeleteBankCard(card.id)}
                                            >
                                                Sil
                                            </button>
                                        )}
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
