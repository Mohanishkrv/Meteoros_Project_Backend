using Meteoros_Project.Data;
using Meteoros_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meteoros_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerProductController : Controller
    {
        private readonly AppDbContext DbContext;
        public CustomerProductController(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CustomerBuysProduct([FromBody]AddCustomerProductRequest addCustomerProductRequest)
        {
            var customerProduct = new CustomerProduct
            {
                CustomerId = addCustomerProductRequest.CustomerId,
                ProductId = addCustomerProductRequest.ProductId
            };
            await DbContext.CustomersProducts.AddAsync(customerProduct);
            await DbContext.SaveChangesAsync();
            return Ok(customerProduct);
        }
    }
}
