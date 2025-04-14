using api_rest.Domain.Models;

namespace api_rest.Communication
{
    public class CategoryResponse : BaseResponse
    {
        public Category Category { get; set; }

        private CategoryResponse(bool success, string message, Category category) : base(success, message)
        {
            Category = category;
        }

        public CategoryResponse(Category category) : this(true, string.Empty, category)
        {
        }

        public CategoryResponse(string message) : this(true, message, null) { }
    }
}
