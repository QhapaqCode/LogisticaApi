﻿using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Domain.Entities;

public class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public TipoProducto TipoProducto { get; set; }

    public string Marca { get; set; }

    public EstadoEntidad EstadoEntidad { get; set; }
}
