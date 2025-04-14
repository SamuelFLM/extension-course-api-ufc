using System.ComponentModel.DataAnnotations;

namespace api_rest.Resources
{
    public class SaveCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
