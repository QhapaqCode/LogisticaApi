import React from 'react';
import { Link } from 'react-router-dom';

const TopBar = () => {
  return (
	<div className="top-bar">
	  <Link to="/almacen-form">Crear Nuevo Almacén</Link>
	  <Link to="/almacen-update">Actualizar Almacén</Link>
	  <Link to="/almacen-delete">Eliminar Almacén</Link>
	  <Link to="/almacen-list">Listar Almacenes</Link>
	</div>
  );
};

export default TopBar;