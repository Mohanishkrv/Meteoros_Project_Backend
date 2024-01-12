using Meteoros_Project.Data;
using Meteoros_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Meteoros_Project.Service
{
    public class Sales : ISales
    {
        private readonly AppDbContext DbContext;

        public Sales(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public int GetTotalSales(Product product)
        {
            var count = DbContext.CustomersProducts.Count(cp => cp.ProductId == product.Id);
            return count;
        }

        public List<Product> GetTop10Products()
        {
            var top10Products = DbContext.Products
                .Include(p => p.CustomerProducts)
                .OrderByDescending(p => GetTotalSales(p))
                .Take(10)
                .ToList();

            return top10Products;
        }
    }
}
