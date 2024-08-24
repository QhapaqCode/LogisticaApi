using RetailProductMicroservice.Domain.ValueObjects;

namespace RetailProductMicroservice.Domain.Entities;

public class Movimiento
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public DireccionMovimiento Direccion { get; set; }

    public decimal Cantidad { get; set; }

    public UnidadMedida UnidadMedida { get; set; }

    public int AlmacenId { get; set; }

    public Almacen Almacen { get; set; }

    public string Descripcion { get; set; }

    public MotivoMovimiento Motivo { get; set; }

    public int? AnaquelId { get; set; }

    public Anaquel Anaquel { get; set; }

    public int ProductoId { get; set; }

    public Producto Producto { get; set; }

    public EstadoEntidad EstadoEntidad { get; set; }
}
