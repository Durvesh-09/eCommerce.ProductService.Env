using AutoMapper;
using BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers
{
    public class ProductUpdateRquestToProductMappingProfile : Profile
    {
        public ProductUpdateRquestToProductMappingProfile() 
        {
            CreateMap<ProductUpdtaeRequest, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName));
                
        }
    }
}
