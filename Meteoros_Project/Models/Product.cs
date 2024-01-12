using System.ComponentModel.DataAnnotations.Schema;

namespace Meteoros_Project.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string pictureSrc { get; set; }
        public int Price { get; set; }
        public int discount { get; set; }
        public int TotalSales { get; set; }

        public ICollection<CustomerProduct> CustomerProducts { get; set; }

    }
}
