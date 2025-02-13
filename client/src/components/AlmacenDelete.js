import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import almacenService from '../services/almacenService';

const AlmacenDelete = () => {
  const [almacen, setAlmacen] = useState({ nombre: '' });
  const { id: almacenId } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (almacenId) {
      almacenService.getAlmacenById(almacenId).then(response => {
        setAlmacen(response.data);
      });
    }
  }, [almacenId]);

  const handleDelete = () => {
    if (almacenId) {
      almacenService.deleteAlmacen(almacenId).then(() => {
        navigate('/');
      });
    }
  };

  return (
    <div>
      <h2>Eliminar Almacén</h2>
      <p>¿Estás seguro de que deseas eliminar el almacén {almacen.nombre}?</p>
      <button onClick={handleDelete}>Eliminar</button>
    </div>
  );
};

export default AlmacenDelete;