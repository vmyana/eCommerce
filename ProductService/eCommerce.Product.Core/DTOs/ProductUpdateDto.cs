using System;
using eCommerce.Product.Core.DTOs.Enums;

namespace eCommerce.Product.Core.DTOs;

public class ProductUpdateDto
{
    public Guid ProductID { get; set; }

    public string ProductName { get; set; }

    public CategoryOptions Category { get; set; }

    public double? UnitPrice { get; set; }

    public int? QuantityInStock { get; set; }
}
