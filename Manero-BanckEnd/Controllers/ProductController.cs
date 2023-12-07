using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Manero_BanckEnd.Controllers
{
    [Route("api/products")]
    [ApiController]
    [UseApiKey]
    [Authorize(Policy = "JwtPolicy")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService; 
        public ProductController(ProductService productService) 
        {
            _productService = productService;   
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _productService.CreateProductAsync(request);
                switch (result.Status)
                {
                    case ResponseStatusCode.CREATED:
                        return Created("", result.Result);

                    case ResponseStatusCode.EXIST:
                        return Conflict(result.Message);

                    default:
                        return Problem(result.Message);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _productService.GetProductsAsync();
                return Ok(result.Result);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts(string category)
        {
            try
            {
                var featuredProducts = await _productService.GetFeaturedProducts(category);
                return Ok(featuredProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("take")]
        public async Task<IActionResult> GetAll(int take, string category)
        {
            try
            {
                var result = await _productService.TakeProductsAsync(take, category);
                return Ok(result.Result);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
        }

        [HttpGet("article/{articleNumber}")]
        public async Task<IActionResult> GetByArticleNumber(string articleNumber)
        {
            try
            {
                var product = await _productService.GetByArticleNumberAsync(articleNumber);

                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound($"Product with article number '{articleNumber}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
