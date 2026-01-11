using BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts
{
    public interface IProductsService
    {
        /// <summary>
        /// Retrieves the list of products from the products respository
        /// </summary>
        /// <returns>Returns list of ProductResponse object</returns>
        Task<List<ProductResponse?>> GetProducts();


        /// <summary>
        /// Retrieves list of products matching with give condition
        /// </summary>
        /// <param name="conditionExpression">Expression that represents condition to check</param>
        /// <returns>Returns matching products</returns>
        Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);

        /// <summary>
        /// Returns a single product that matches with give condition
        /// </summary>
        /// <param name="conditionExpression">Express that represents the condition to check</param>
        /// <returns>Returns matching product or null </returns>
        Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression);

        /// <summary>
        /// Add (inserts) product into the table using products repository
        /// </summary>
        /// <param name="productAddRequest">Product to insert</param>
        /// <returns>Product after inserting or null if unsuccessful</returns>
        Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

        /// <summary>
        /// Updates the existing prodct based on the ProductID
        /// </summary>
        /// <param name="productUpdtaeRequest">Product data to update</param>
        /// <returns>returns product object after sucessful update; otherwise null</returns>
        Task<ProductResponse?> UpdateProduct(ProductUpdtaeRequest productUpdtaeRequest);

        /// <summary>
        /// Deletes an existing product based on given product id
        /// </summary>
        /// <param name="productID">ProductID to search and delete</param>
        /// <returns>Returns true if the deletion is successful; otherwise false</returns>
        Task<bool> DeleteProduct(Guid productID);
    }
}
