﻿namespace Ecommerce.API.DTOs.ProductItems;

public class UpdateProductItemDto
{
    public string SKU { get; set; }
    public string QuantityInStock { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ProductId { get; set; }
}