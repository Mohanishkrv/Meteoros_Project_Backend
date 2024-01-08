using Meteoros_Project.Data;
using Meteoros_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Meteoros_Project.Service;

namespace Meteoros_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext DbContext;
        private readonly ISales SalesService;

        public ProductController(AppDbContext dbContext, ISales salesService)
        {
            DbContext = dbContext;
            SalesService = salesService;
        }

        [HttpGet("top10")]
        public IActionResult GetTop10Products()
        {
            try
            {
                var top10Products = DbContext.Products
                    .Include(p => p.CustomerProducts)
                    .OrderByDescending(p => p.TotalSales)
                    .Take(10)
                    .ToList();

                return Ok(top10Products);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while fetching top 10 products: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var product = await DbContext.Products
                .Include(p => p.CustomerProducts)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }
            product.TotalSales = SalesService.GetTotalSales(product);
            DbContext.Products.Update(product);
            await DbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]AddProductRequest addProductRequest)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                pictureSrc = addProductRequest.pictureSrc,
                Price = addProductRequest.Price,
                discount = addProductRequest.discount
            };
            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
            return Ok(product);
        }
    }
}
