using RetailProductMicroservice.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailProductMicroservice.Domain.Entities;

public class Movimiento
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public DireccionMovimiento Direccion { get; set; }

    public decimal Cantidad { get; set; }

    public UnidadMedida UnidadMedida { get; set; }

    public int? AlmacenId { get; set; }

    [ForeignKey("AlmacenId")]
    public Almacen? Almacen { get; set; }

    public string Descripcion { get; set; }

    public MotivoMovimiento Motivo { get; set; }

    public int? AnaquelId { get; set; }

    [ForeignKey("AnaquelId")]
    public Anaquel? Anaquel { get; set; }

    public int ProductoId { get; set; }

    [ForeignKey("ProductoId")]
    public Producto? Producto { get; set; }

    public EstadoEntidad EstadoEntidad { get; set; }
}
