using api_rest.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace api_rest.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public short QuantityInPackage { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
