using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Domain.Entities;

public class Existencia : Producto
{
    public string Codigo { get; set; }

    public UnidadMedida UnidadMedida { get; set; }
}
