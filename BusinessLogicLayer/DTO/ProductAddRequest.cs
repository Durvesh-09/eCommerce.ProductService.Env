namespace BusinessLogicLayer.DTO
{
    public record ProductAddRequest(string Productname, CategoryOptions Category, double? UnitPrice, int? QuantityInStock)
    {
        public ProductAddRequest() : this(default, default, default, default)
        { 
        }
    }
}
