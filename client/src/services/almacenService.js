import axios from 'axios';
import config from '../config';

const API_URL = `${config.SERVER_URL}/api/Almacen`;

const getAllAlmacenes = () => {
  return axios.get(API_URL);
};

const getAlmacenById = (id) => {
  return axios.get(`${API_URL}/${id}`);
};

const createAlmacen = (almacen) => {
  return axios.post(API_URL, almacen);
};

const updateAlmacen = (id, almacen) => {
  return axios.put(`${API_URL}/${id}`, almacen);
};

const deleteAlmacen = (id) => {
  return axios.delete(`${API_URL}/${id}`);
};

const service = {
  getAllAlmacenes,
  getAlmacenById,
  createAlmacen,
  updateAlmacen,
  deleteAlmacen,
};

export default service;