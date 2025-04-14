using api_rest.Domain.Models;
using api_rest.Resources;
using AutoMapper;

namespace api_rest.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<Product, ProductResource>()
                .ForMember(x => x.UnitOfMeasurement, opt => opt.MapFrom(src => src.UnitOfMeasurement.ToString()));

            CreateMap<User, UserResource>();
        }
    }
}
