using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Domain.Entities;

public class Serializable : Producto
{
    public string Serie { get; set; }

    public EstadoProducto EstadoProducto { get; set; }
}
