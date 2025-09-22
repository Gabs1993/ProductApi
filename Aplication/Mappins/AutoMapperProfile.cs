using Application.DTOs.CategoryDTO;
using Application.DTOs.ProductDTO;
using Application.DTOs.UserDTO;
using AutoMapper;
using Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappins
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product
            CreateMap<Product, ReadProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();

            // Category
            CreateMap<Category, ReadCategoryDTO>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            // User
            CreateMap<Users, ReadUserDTO>().ReverseMap();
            CreateMap<CreateUserDTO, Users>();
            CreateMap<UpdateUserDTO, Users>();
        }
    }
}
