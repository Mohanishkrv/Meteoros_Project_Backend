namespace Meteoros_Project.Models
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string pictureSrc { get; set; }
        public int Price { get; set; }
        public int discount { get; set; }
    }
}
