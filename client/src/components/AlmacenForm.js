import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import almacenService from '../services/almacenService';
import TipoAlmacen from '../tipoAlmacen';

const AlmacenForm = ({ match, history }) => {
  const [almacen, setAlmacen] = useState({ nombre: '', tipo: TipoAlmacen.BODEGA });
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setAlmacen({ ...almacen, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    almacenService.createAlmacen(almacen).then(() => {
      navigate('/almacen-list');
    });
  };

  return (
    <div>
      <h2>Crear Nuevo Almacén</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nombre:</label>
          <input
            type="text"
            name="nombre"
            value={almacen.nombre}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Tipo:</label>
          <select name="tipo" value={almacen.tipo} onChange={handleChange}>
            <option value={TipoAlmacen.BODEGA}>Bodega</option>
            <option value={TipoAlmacen.TIENDA}>Tienda</option>
            <option value={TipoAlmacen.ALMACEN}>Almacen</option>
            <option value={TipoAlmacen.DEPOSITO}>Deposito</option>
          </select>
        </div>
        <button type="submit">Crear</button>
      </form>
    </div>
  );
};

export default AlmacenForm;
