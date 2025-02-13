// apiService.js
import apiClient from './apiClient';
import endpoints from './apiEndpoint';

// Auth iþlemleri
export const login = async (data) => {
    return await apiClient.post(endpoints.auth.login, data);
};

export const logout = async (userName) => {
    return await apiClient.post(`${endpoints.auth.logout}?userName=${encodeURIComponent(userName)}`);
};

// BankCard iþlemleri
export const getAllBankCards = async () => {
    return await apiClient.get(endpoints.bankcards.getAll);
};

export const getBankCardById = async (id) => {
    return await apiClient.get(endpoints.bankcards.getById(id));
};

export const createBankCard = async (data) => {
    return await apiClient.post(endpoints.bankcards.create, data);
};

export const updateBankCardById = async (id, data) => {
    return await apiClient.put(endpoints.bankcards.updateById(id), data);
};

export const deleteBankCardById = async (id) => {
    return await apiClient.delete(endpoints.bankcards.deleteById(id));
};

// CreditCard iþlemleri (benzer þekilde)
export const getAllCreditCards = async () => {
    return await apiClient.get(endpoints.creditcards.getAll);
};

export const getCreditCardById = async (id) => {
    return await apiClient.get(endpoints.creditcards.getById(id));
};

export const createCreditCard = async (data) => {
    return await apiClient.post(endpoints.creditcards.create, data);
};

export const updateCreditCardById = async (id, data) => {
    return await apiClient.put(endpoints.creditcards.updateById(id), data);
};

export const deleteCreditCardById = async (id) => {
    return await apiClient.delete(endpoints.creditcards.deleteById(id));
};

// User iþlemleri
export const getAllUsers = async () => {
    return await apiClient.get(endpoints.user.getAll);
};

export const getUserById = async (id) => {
    return await apiClient.get(endpoints.user.getById(id));
};

export const createUser = async (data) => {
    return await apiClient.post(endpoints.user.create, data);
};

export const updateUser = async (id, data) => {
    return await apiClient.put(endpoints.user.update(id), data);
};

// RolPermissions iþlemleri
export const getRolPermissionsById = async (id) => {
    return await apiClient.get(endpoints.rolPermissions.getById(id));
};