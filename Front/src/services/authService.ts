import axios from "axios";

const API_URL = "https://localhost:7133/api"; // Substitua pela URL do seu endpoint

// Função para fazer login
export const login = async (username: string, password: string) => {
  try {
    const response = await axios.post(`${API_URL}/User/Login`, {
      username,
      password,
    });
    return response.data;
  } catch (error) {
    console.error("Erro ao fazer login:", error);
    throw error;
  }
};


// Função para fazer logout
export const logout = async () => {
  try {
    const response = await axios.post(`${API_URL}/logout`);
    return response.data;
  } catch (error) {
    console.error("Erro ao fazer logout:", error);
    throw error;
  }
};

// Função para registrar um novo usuário
export const register = async (username: string, password: string) => {
  try {
    const response = await axios.post(`${API_URL}/User`, {
      username,
      password
    });
    return response.data;
  } catch (error) {
    console.error("Erro ao registrar:", error);
    throw error;
  }
};

export const getCategory = async () => {
  try {
    const response = await axios.get(`${API_URL}/Category`);
    if (response && response.data && Array.isArray(response.data))
      return response;
    else throw new Error("Invalid data format");
  } catch (error) {
    console.error("Invalid data format", error);
    throw error;
  }
}

export const postCategory = async (name: string) => {
  try {
    const response = await axios.post(`${API_URL}/Category`, { name });
    if (response && response.data && Array.isArray(response.data))
      return response;
    else throw new Error("Invalid data format");
  } catch (error) {
    console.error("Invalid data format", error);
    throw error;
  }
}

export const putCategory = async (id: number, name: string) => {
  try {
    const response = await axios.put(`${API_URL}/Category/${id}` , { name });
    if (response && response.data && Array.isArray(response.data))
      return response;
    else throw new Error("Invalid data format");
  } catch (error) {
    console.error("Invalid data format", error);
    throw error;
  }
}

export const deleteCategory = async (id: number) => {
  try {
    const response = await axios.delete(`${API_URL}/Category/${id}`);
    if (response && response.data && Array.isArray(response.data))
      return response;
  } catch (error) {
    console.error("Invalid data format", error);
  }
}