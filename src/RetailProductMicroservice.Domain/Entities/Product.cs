using System;

namespace RetailProductMicroservice.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentStock { get; set; }
        public string Status { get; set; }
    }
}