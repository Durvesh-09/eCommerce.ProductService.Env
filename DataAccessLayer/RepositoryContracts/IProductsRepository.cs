using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.RepositoryContracts
{
    /// <summary>
    /// Represents a repository for managing 'Products' table 
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        /// Retrieves all products asynchronously 
        /// </summary>
        /// <returns>Returns all products from the table</returns>
        Task<IEnumerable<Product>> GetProducts();

        /// <summary>
        /// Returns all products based on specified condition asynchronously
        /// </summary>
        /// <param name="conditionExpression"></param>
        /// <returns>Returns a collection of matching products</returns>
        Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

        /// <summary>
        ///  Returns product based on specified condition asynchronously
        /// </summary>
        /// <param name="conditionExpression">The condition to filter the product</param>
        /// <returns>Returns a single product or null if not found</returns>
        Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

        /// <summary>
        /// Adds a new product into the products table asyncronously
        /// </summary>
        /// <param name="product">The product to be added</param>
        /// <returns>Returns athe added produc object or null if unsuccessful</returns>
        Task<Product?> AddProduct(Product product);

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="product">The product to be Updated</param>
        /// <returns>Returns the updated product object or null if not found</returns>
        Task<Product?> UpdateProduct(Product product);

        /// <summary>
        /// Delete the product asynchronously.
        /// </summary>
        /// <param name="productId">The productId to be deleted</param>
        /// <returns>Returns true if the deletetion is sucessful, false otherwise</returns>
        Task<bool> DeleteProduct(Guid productId);
    }
}
