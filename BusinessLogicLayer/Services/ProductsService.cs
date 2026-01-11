using AutoMapper;
using BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
        private readonly IValidator<ProductUpdtaeRequest> _productUpdateRequestValidator;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IValidator<ProductAddRequest> productAddRequestValidator,
            IValidator<ProductUpdtaeRequest> productUpdateRequestValidator,
            IMapper mapper,
            IProductsRepository productsRepository)
        {
            _productAddRequestValidator = productAddRequestValidator;
            _productUpdateRequestValidator = productUpdateRequestValidator;
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
            if (productAddRequest == null)
            { 
                throw new ArgumentNullException(nameof(productAddRequest));
            }

            //Validate the product using Fluent Validation
            ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ",
                    validationResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }

            // Attempt to add product 
            Product productInput = _mapper.Map<Product>(productAddRequest);
            Product? addedProduct = await _productsRepository.AddProduct(productInput);
            if (addedProduct == null)
            { 
                return null;
            }

            ProductResponse addedProductResponse = _mapper.Map<ProductResponse>(addedProduct);
            
            return addedProductResponse;
        }

        public async Task<bool> DeleteProduct(Guid productID)
        {
            Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductId == productID);

            if (existingProduct == null)
            {
                return false;
            }

            //Attemp to delete
            bool isDeleted = await _productsRepository.DeleteProduct(productID);
            return isDeleted;
        }

        public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            Product? product = await _productsRepository.GetProductByCondition(conditionExpression);
            
            if(product == null)
            {
                return null;
            }

            ProductResponse productResponse = _mapper.Map<ProductResponse>(product);
            return productResponse;
        }

        public async Task<List<ProductResponse?>> GetProducts()
        {
            IEnumerable<Product?> products = await _productsRepository.GetProducts();

            

            IEnumerable<ProductResponse?> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return productResponse.ToList();
        }

        public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
        {
            IEnumerable<Product?> products = await _productsRepository.GetProductsByCondition(conditionExpression);


            IEnumerable<ProductResponse?> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return productResponse.ToList();
        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdtaeRequest productUpdtaeRequest)
        {
            if(productUpdtaeRequest == null)
            {
                throw new ArgumentNullException(nameof(productUpdtaeRequest));
            }
            
            Product? product = await _productsRepository.GetProductByCondition(temp => temp.ProductId == productUpdtaeRequest.ProductId);

            if (product == null)
            {
                throw new ArgumentException("Invalid Product ID");
            }

            ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdtaeRequest);

            if (!validationResult.IsValid)
            {
                string errorMessage = string.Join(", ",validationResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(errorMessage);
            }

            // Map productUpdateRequest to Product
            Product productUpdateInput = _mapper.Map<Product>(productUpdtaeRequest);

            Product? updatedProduct = await _productsRepository.UpdateProduct(productUpdateInput);

            if (updatedProduct == null)
            { 
                return null;
            }

            // Map Product to ProductResponse 
            ProductResponse updatedProsuctResponse = _mapper.Map<ProductResponse>(updatedProduct);

            return updatedProsuctResponse;
        }
    }
}
