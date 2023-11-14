using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Manero_BanckEnd.Services;

public class ProductService
{
    private readonly ProductRepo _productRepo;

    public ProductService(ProductRepo productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<ServiceResponse> CreateProductAsync(ProductCreateRequest request)
    {
        try
        {
            if (!await _productRepo.ExistAsync(x => x.ArticleNumber == request.ArticleNumber))
            {
                Product product = await _productRepo.CreateAsync(request);
                if (product != null)
                {
                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.CREATED,
                        Message = "Product was crated successfully",
                        Result = product
                    };
                }


            }
            else return new ServiceResponse
            {
                Status = ResponseStatusCode.EXIST,
                Message = "Already exist",
                Result = null

            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return new ServiceResponse
        {
            Status = ResponseStatusCode.ERROR,
            Message = "Failed",
            Result = null

        };
    }

    public async Task<ServiceResponse> GetProductsAsync()
    {
        try
        {
            IEnumerable<Product> products = (await _productRepo.GetAsync()).Select(productEntity => (Product)productEntity);
            return new ServiceResponse
            { 
                Status = ResponseStatusCode.OK,
                Result = products
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return new ServiceResponse
        {
            Status = ResponseStatusCode.ERROR,
            Message = "Something went wrong",
            Result = null
        };

    }

    public async Task <ServiceResponse> GetFeaturedProducts(string category)
    {
        try
        {
            // Get the products from the database that match the category and are featured
            var featuredProducts = (await _productRepo.GetAsync()).Select(p => p.Category == category ).ToList();

            
            var response = new ServiceResponse
            {
                Status = ResponseStatusCode.OK,
                Result = featuredProducts,
                Message = "Featured products retrieved successfully",
            };

            return response;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message) ; }
        return new ServiceResponse
        {
            Status = ResponseStatusCode.ERROR,
            Message = "Something wrong geting featured products",
            Result = null
        };
    }
}
