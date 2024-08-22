using System;

namespace RetailProductMicroservice.Domain.Entities
{
    public class Logistic
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string Reason { get; set; }
        public int SourceWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
    }
}