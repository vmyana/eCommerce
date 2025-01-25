using System;
using System.Linq.Expressions;

namespace eCommerce.Product.Infrastructure.Repositories;

public interface IProductRepository
{
  Task<IEnumerable<Models.Product>> GetProducts();

  Task<IEnumerable<Models.Product?>> GetProductsByCondition(Expression<Func<Models.Product, bool>> conditionExpression);

  Task<Models.Product?> GetProductByCondition(Expression<Func<Models.Product, bool>> conditionExpression);

  Task<Models.Product?> AddProduct(Models.Product product);

  Task<Models.Product?> UpdateProduct(Models.Product product);

  Task<bool> DeleteProduct(Guid productID);
}
