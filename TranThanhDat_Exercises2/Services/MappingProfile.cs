using AutoMapper;
using TranThanhDat_Exercises2.Models;
using TranThanhDat_Exercises2.Data.Entities;

namespace TranThanhDat_Exercises2.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel,Category>();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
          

        }
    }
}
