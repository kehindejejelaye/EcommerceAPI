﻿using Ecommerce.API.Contracts;
using Ecommerce.API.DTOs.Category;
using Ecommerce.API.DTOs.Product;
using Ecommerce.API.DTOs.ProductItems;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Services;

public class PropertyMappingService : IPropertyMappingService
{
    //private readonly Dictionary<string, PropertyMappingValue> _authorPropertyMapping =
    //    new(StringComparer.OrdinalIgnoreCase)
    //    {
    //        { "Id", new(new[] { "Id" }) },
    //        { "MainCategory", new(new[] { "MainCategory" }) },
    //        { "Age", new(new[] { "DateOfBirth" }, true) },
    //        { "Name", new(new[] { "FirstName", "LastName" }) }
    //    };

    private readonly Dictionary<string, PropertyMappingValue> _categoryPropertyMapping =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "Id", new(new[] { "Id" }) },
            { "Name", new(new[] { "Name" }) }
        };

    private readonly Dictionary<string, PropertyMappingValue> _productPropertyMapping =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "Id", new(new[] { "Id" }) },
            { "Name", new(new[] { "Name" }) },
            { "CategoryId", new(new[] { "CategoryId" }) }
        };

    private readonly Dictionary<string, PropertyMappingValue> _productItemPropertyMapping =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "Id", new(new[] { "Id" }) },
            { "SKU", new(new[] { "SKU" }) },
            { "QuantityInStock", new(new[] { "QuantityInStock" }) },
            { "Price", new(new[] { "Price" }) },
            { "ProductId", new(new[] { "ProductId" }) }
        };

    private readonly IList<IPropertyMapping> _propertyMappings =
        new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings = new List<IPropertyMapping>
        {
            new PropertyMapping<ReadCategoryDto, Category>(_categoryPropertyMapping),
            new PropertyMapping<ReadProductDto, Product>(_productPropertyMapping),
            new PropertyMapping<ReadProductItemDto, ProductItem>(_productItemPropertyMapping)
        };
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping
          <TSource, TDestination>()
    {
        // get matching mapping
        var matchingMapping = _propertyMappings
            .OfType<PropertyMapping<TSource, TDestination>>();

        if (matchingMapping.Count() == 1)
        {
            return matchingMapping.First().MappingDictionary;
        }

        throw new Exception($"Cannot find exact property mapping instance " +
            $"for <{typeof(TSource)},{typeof(TDestination)}");
    }

    public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
    {
        var propertyMapping = GetPropertyMapping<TSource, TDestination>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            return true;
        }

        // the string is separated by ",", so we split it.
        var fieldsAfterSplit = fields.Split(',');

        // run through the fields clauses
        foreach (var field in fieldsAfterSplit)
        {
            // trim
            var trimmedField = field.Trim();

            // remove everything after the first " " - if the fields 
            // are coming from an orderBy string, this part must be 
            // ignored
            var indexOfFirstSpace = trimmedField.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ?
                trimmedField : trimmedField.Remove(indexOfFirstSpace);

            // find the matching property
            if (!propertyMapping.ContainsKey(propertyName))
            {
                return false;
            }
        }
        return true;
    }
}

