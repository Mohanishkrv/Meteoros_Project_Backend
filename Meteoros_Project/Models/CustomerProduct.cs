using System.ComponentModel.DataAnnotations.Schema;

namespace Meteoros_Project.Models
{
    public class CustomerProduct
    {
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
