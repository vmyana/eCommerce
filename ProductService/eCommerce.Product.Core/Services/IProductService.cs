using System.Linq.Expressions;
using eCommerce.Product.Core.DTOs;

namespace eCommerce.Product.Core.Services;

public interface IProductService
{
  Task<List<ProductResponseDto?>> GetProducts();

  Task<List<ProductResponseDto?>> GetProductsByCondition(Expression<Func<Infrastructure.Models.Product, bool>> conditionExpression);

  Task<ProductResponseDto?> GetProductByCondition(Expression<Func<Infrastructure.Models.Product, bool>> conditionExpression);

  Task<ProductResponseDto?> AddProduct(ProductAddDto productAddRequest);

  Task<ProductResponseDto?> UpdateProduct(ProductUpdateDto productUpdateRequest);

  Task<bool> DeleteProduct(Guid productID);
}
