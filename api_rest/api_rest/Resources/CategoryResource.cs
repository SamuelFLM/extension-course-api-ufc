using api_rest.Domain.Models;

namespace api_rest.Resources
{
    public class CategoryResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Product> Products { get; set; }
    }
}
