using System.ComponentModel.DataAnnotations.Schema;

namespace Meteoros_Project.Models
{
    public class AddCustomerProductRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}
