using BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace eCommerce.ProductsMicroService.API.APIEndpoins
{
    public static class ProductsAPIEmdpoints 
    {
        public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
        {

            // GET  /api/products
            app.MapGet("/api/products" ,async (IProductsService productsService) =>
            {
                List<ProductResponse?> products = await productsService.GetProducts();

                return Results.Ok(products);
            });

            // GET  /api/products/search/Product-id/0000-0000
            app.MapGet("/api/products/search/Product-id/{ProductID:guid}", async (IProductsService productService, Guid ProductID) =>
            {
                ProductResponse? product = await productService.GetProductByCondition(temp => temp.ProductId == ProductID);

                return Results.Ok(product);
            });

            
            // GET /api/products/search/xxxxxxxxxxxxxxxxxxxxx
            app.MapGet("/api/products/search/{searchstring}", async (IProductsService productService, string searchstring) =>
            {
                List<ProductResponse?> productsByProductName = await productService.GetProductsByCondition(temp => temp.ProductName != null && temp.ProductName.Contains(searchstring, StringComparison.OrdinalIgnoreCase));

                List<ProductResponse?> productsByCategory = await productService.GetProductsByCondition(temp => temp.Category != null && temp.Category.Contains(searchstring, StringComparison.OrdinalIgnoreCase));

                var products = productsByProductName.Union(productsByCategory);

                return Results.Ok(products);
            });

            // POST /api/products
            app.MapPost("/api/products", async (IProductsService productsService, IValidator<ProductAddRequest> productAddRequestValidator, ProductAddRequest productAddRequest) =>
            {
                // Validation
                ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);
                if(!validationResult.IsValid) 
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }

                ProductResponse?  addedProductResponse = await productsService.AddProduct(productAddRequest);

                if (addedProductResponse != null)
                    return Results.Created(@$"/api/products/search/product-id/{addedProductResponse.ProductId}", addedProductResponse);
                else
                    return Results.Problem("Error in adding product");
            });

            // PUT /api/products
            app.MapPut("/api/products", async (IProductsService productsService, IValidator<ProductUpdtaeRequest> productUpdateRequestValidator, ProductUpdtaeRequest productUpdateRequest) =>
            {
                // Validation
                ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }

                ProductResponse? updatedProductResponse = await productsService.UpdateProduct(productUpdateRequest);

                if (updatedProductResponse != null)
                    return Results.Ok(updatedProductResponse);
                else
                    return Results.Problem("Error in adding product");
            });

            // DELETE /api/products
            app.MapDelete("/api/products/{ProductID:guid}", async (IProductsService productsService, Guid ProductID) =>
            {
                bool isDeleted = await productsService.DeleteProduct(ProductID);

                if (isDeleted)
                    return Results.Ok(true);
                else
                    return Results.Problem("Error in Deleting product");
            });


            return app;
        }

    }
}
