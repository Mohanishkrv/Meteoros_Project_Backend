    using Meteoros_Project.Data;
using Meteoros_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meteoros_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext DbContext;

        public CustomerController(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetCustomersForProduct(Guid productId)
        {
            var customers = DbContext.CustomersProducts
                .Where(cp => cp.ProductId == productId)
                .Select(cp => cp.Customer)
                .ToList();
            foreach (var customer in customers)
            {
                customer.PhoneNumber = MaskPhoneNumber(customer.PhoneNumber);
            }

            return Ok(customers);
        }

        [HttpGet("product/{productId}/last")]
        public IActionResult GetLastCustomerForProduct(Guid productId)
        {
            var lastCustomer = DbContext.CustomersProducts
                .Where(cp => cp.ProductId == productId)
                .OrderByDescending(cp => cp.Customer.Id)
                .Select(cp => cp.Customer)
                .FirstOrDefault();

            if (lastCustomer == null)
            {
                return NotFound();
            }
            lastCustomer.PhoneNumber = MaskPhoneNumber(lastCustomer.PhoneNumber);

            return Ok(lastCustomer);
        }
        [HttpGet("customer/lastEnteredCustomer")]
        public IActionResult GetLastCustomerCreated() 
        {
            var lastEnteredCustomer = DbContext.Customers.OrderByDescending(c=>c.DateCreated).FirstOrDefault();
            lastEnteredCustomer.PhoneNumber = MaskPhoneNumber(lastEnteredCustomer.PhoneNumber);
            return Ok(lastEnteredCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody]AddCustomerRequest addCustomerRequest)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = addCustomerRequest.Name,
                PhoneNumber = addCustomerRequest.PhoneNumber,
                DateCreated = DateTime.Now
            };
            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();
            return Ok(customer);
        }

        private string MaskPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Substring(0, 6) + "XXXX";
        }
    }
}
