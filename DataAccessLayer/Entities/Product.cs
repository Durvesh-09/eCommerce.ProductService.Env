using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category {  get; set; }
        public string? UnitPrice { get; set; }
        public string? QuantityInStock { get; set; }

    }
}
