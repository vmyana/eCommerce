using System.Linq.Expressions;
using AutoMapper;
using eCommerce.Product.Core.DTOs;
using eCommerce.Product.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace eCommerce.Product.Core.Services;

public class ProductService(
    IValidator<ProductAddDto> productAddValidator,
    IValidator<ProductUpdateDto> productUpdateValidator,
    IMapper mapper,
    IProductRepository productRepository) : IProductService
{
    public async Task<ProductResponseDto?> AddProduct(ProductAddDto productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        var validationResult = await productAddValidator.ValidateAsync(productAddRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        var addedProduct = await productRepository.AddProduct(mapper.Map<Infrastructure.Models.Product>(productAddRequest));

        if (addedProduct == null)
        {
            return null;
        }

        var addedProductResponse = mapper.Map<ProductResponseDto>(addedProduct);

        return addedProductResponse;
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        var existingProduct = await productRepository.GetProductByCondition(temp => temp.ProductID == productID);

        if (existingProduct == null)
        {
            return false;
        }

        bool isDeleted = await productRepository.DeleteProduct(productID);
        return isDeleted;
    }

    public async Task<ProductResponseDto?> GetProductByCondition(Expression<Func<Infrastructure.Models.Product, bool>> conditionExpression)
    {
        var product = await productRepository.GetProductByCondition(conditionExpression);
        if (product == null)
        {
            return null;
        }

        var productResponse = mapper.Map<ProductResponseDto>(product);
        return productResponse;
    }

    public async Task<List<ProductResponseDto?>> GetProducts()
    {
        var products = await productRepository.GetProducts();
        var productResponses = mapper.Map<IEnumerable<ProductResponseDto>>(products);
        return productResponses.ToList();
    }

    public async Task<List<ProductResponseDto?>> GetProductsByCondition(Expression<Func<Infrastructure.Models.Product, bool>> conditionExpression)
    {
        var products = await productRepository.GetProductsByCondition(conditionExpression);
        var productResponses = mapper.Map<IEnumerable<ProductResponseDto>>(products);
        return productResponses.ToList();
    }

    public async Task<ProductResponseDto?> UpdateProduct(ProductUpdateDto productUpdateRequest)
    {
        var existingProduct = await productRepository.GetProductByCondition(temp => temp.ProductID == productUpdateRequest.ProductID);

        if(existingProduct == null)
        {
            throw new ArgumentException("Invalid Product ID");
        }

        ValidationResult validationResult = await productUpdateValidator.ValidateAsync(productUpdateRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        var product = mapper.Map<Infrastructure.Models.Product>(productUpdateRequest);
        var updatedProduct = await productRepository.UpdateProduct(product);
        var updatedProductResponse = mapper.Map<ProductResponseDto>(updatedProduct);
        return updatedProductResponse;
    }
}
