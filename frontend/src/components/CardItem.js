// src/components/CardItem.js
import React from 'react';
import { useNavigate } from 'react-router-dom';

const CardItem = ({ card }) => {
  const navigate = useNavigate();

  return (
    <tr>
      <td>{card.cardNumber}</td>
      <td>{card.cardHolderName}</td>
      <td>{card.bankName}</td>
      <td>{new Date(card.expirationDate).toLocaleDateString()}</td>
      <td>{card.cvv}</td>
      {/* Kredi Kartı ve Banka Kartı için farklı hücreler */}
      {card.cardType === 2 ? (
        <>
          <td>{card.creditLimit} TL</td>
          <td>{card.availableBalance} TL</td>
          <td>{card.minimumPayment} TL</td>
          <td>{card.interestRate} %</td>
          <td>{new Date(card.billingDate).toLocaleDateString()}</td>
          <td>{new Date(card.dueDate).toLocaleDateString()}</td>
          <td>{card.installments ? 'Var' : 'Yok'}</td>
        </>
      ) : (
        <>
          <td>{card.accountNumber}</td>
          <td>{card.iban}</td>
          <td>{card.balance} TL</td>
          <td>{card.withdrawalLimit} TL</td>
          <td>{card.isContactless ? 'Var' : 'Yok'}</td>
        </>
      )}
      <td>
        <button
          className="edit-button"
          onClick={() => navigate(`/edit/${card.id}`)}
        >
          Düzenle
        </button>
      </td>
    </tr>
  );
};

export default CardItem;
