using System.Linq.Expressions;
using eCommerce.Product.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eCommerce.Product.Infrastructure.Repositories;

public class ProductRepository(MySqlDbContext dbContext) : IProductRepository
{
    public async Task<Models.Product?> AddProduct(Models.Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Models.Product? existingProduct = await dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductID == productID);
        if (existingProduct == null)
        {
            return false;
        }

        dbContext.Products.Remove(existingProduct);
        int affectedRowsCount = await dbContext.SaveChangesAsync();
        return affectedRowsCount > 0;
    }

    public async Task<Models.Product?> GetProductByCondition(Expression<Func<Models.Product, bool>> conditionExpression)
    {
        return await dbContext.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Models.Product>> GetProducts()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Models.Product?>> GetProductsByCondition(Expression<Func<Models.Product, bool>> conditionExpression)
    {
        return await dbContext.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Models.Product?> UpdateProduct(Models.Product product)
    {
        Models.Product? existingProduct = await dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductID == product.ProductID);
        if (existingProduct == null )
        {
            return null;
        }

        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.Category = product.Category;

        await dbContext.SaveChangesAsync();

        return existingProduct;
    }
}
