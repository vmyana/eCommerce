using eCommerce.Product.Core.DTOs;
using eCommerce.Product.Core.Services;
using FluentValidation;

namespace eCommerce.Product.Api.ApiEndpoints;

public static class MapProductApiEndpoints
{
    public static IEndpointRouteBuilder ProductApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        //GET /api/products
        endpoints.MapGet("/api/products", async (IProductService productsService) =>
        {
            var products = await productsService.GetProducts();
            return Results.Ok(products);
        });


        //GET /api/products/search/product-id/00000000-0000-0000-0000-000000000000
        endpoints.MapGet("/api/products/search/product-id/{ProductID}", async (IProductService productsService, Guid ProductID) =>
        {
            var product = await productsService.GetProductByCondition(temp => temp.ProductID == ProductID);
            return Results.Ok(product);
        });

        //GET /api/products/search/xxxxxxxxxxxxxxxxxx
        endpoints.MapGet("/api/products/search/{SearchString}", async (IProductService productsService, string SearchString) =>
        {
            var productsByProductName = await productsService.GetProductsByCondition(temp => temp.ProductName != null && temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            var productsByCategory = await productsService.GetProductsByCondition(temp => temp.Category != null && temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            var products = productsByProductName.Union(productsByCategory);

            return Results.Ok(products);
        });


        //POST /api/products
        endpoints.MapPost("/api/products", async (IProductService productsService, IValidator<ProductAddDto> productAddRequestValidator, ProductAddDto productAddRequest) =>
        {
            var validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                .GroupBy(temp => temp.PropertyName)
                .ToDictionary(grp => grp.Key,
                    grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }

            var addedProductResponse = await productsService.AddProduct(productAddRequest);
            if (addedProductResponse != null)
                return Results.Created($"/api/products/search/product-id/{addedProductResponse.ProductID}", addedProductResponse);
            else
                return Results.Problem("Error in adding product");
        });


        //PUT /api/products
        endpoints.MapPut("/api/products", async (IProductService productsService, IValidator<ProductUpdateDto> productUpdateRequestValidator, ProductUpdateDto productUpdateRequest) =>
        {
            var validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                .GroupBy(temp => temp.PropertyName)
                .ToDictionary(grp => grp.Key,
                    grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }

            var updatedProductResponse = await productsService.UpdateProduct(productUpdateRequest);
            if (updatedProductResponse != null)
                return Results.Ok(updatedProductResponse);
            else
                return Results.Problem("Error in updating product");
        });


        //DELETE /api/products/xxxxxxxxxxxxxxxxxxx
        endpoints.MapDelete("/api/products/{ProductID:guid}", async (IProductService productsService, Guid ProductID) =>
        {
            bool isDeleted = await productsService.DeleteProduct(ProductID);
            if (isDeleted)
                return Results.Ok(true);
            else
                return Results.Problem("Error in deleting product");
        });

        return endpoints;
    }
}
