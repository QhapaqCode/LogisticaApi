import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import almacenService from '../services/almacenService';

const AlmacenUpdate = () => {
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

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAlmacen({ ...almacen, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (almacenId) {
            almacenService.updateAlmacen(almacenId, almacen).then(() => {
                navigate('/');
            });
        }
    };

    return (
        <div>
            <h2>Actualizar Almacén</h2>
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
                <button type="submit">Actualizar</button>
            </form>
        </div>
    );
};

export default AlmacenUpdate;