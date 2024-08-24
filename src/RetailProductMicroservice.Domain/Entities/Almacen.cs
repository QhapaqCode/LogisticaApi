using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Domain.Entities;

public class Almacen
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Direccion { get; set; }

    public TipoAlmacen TipoAlmacen { get; set; }

    public EstadoEntidad EstadoEntidad { get; set; }
}
