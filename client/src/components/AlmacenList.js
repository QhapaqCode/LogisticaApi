import React, { useEffect, useState } from 'react';
import almacenService from '../services/almacenService';

const AlmacenList = () => {
  const [almacenes, setAlmacenes] = useState([]);

  useEffect(() => {
    almacenService.getAllAlmacenes().then(response => {
      setAlmacenes(response.data);
    });
  }, []);

  return (
    <div>
      <h2>Lista de Almacenes</h2>
      <ul>
        {almacenes.map(almacen => (
          <li key={almacen.id}>{almacen.nombre}</li>
        ))}
      </ul>
    </div>
  );
};

export default AlmacenList;
