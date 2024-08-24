using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Domain.Entities;

public class Anaquel
{
    public int Id { get; set; }

    public string Codigo { get; set; }

    public int Fila { get; set; }

    public int Columna { get; set; }

    public int AlmacenId { get; set; }

    public Almacen Almacen { get; set; }

    public EstadoEntidad EstadoEntidad { get; set; }
}
