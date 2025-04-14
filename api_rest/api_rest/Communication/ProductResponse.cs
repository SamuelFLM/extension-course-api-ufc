using api_rest.Domain.Models;

namespace api_rest.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; set; }
        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public ProductResponse(Product category) : this(true, string.Empty, category)
        {
        }

        public ProductResponse(string message) : this(true, message, null) { }
    }
}
