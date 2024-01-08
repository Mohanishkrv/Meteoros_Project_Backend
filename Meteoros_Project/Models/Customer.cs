namespace Meteoros_Project.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<CustomerProduct> CustomerProducts { get; set; }
    }
}
