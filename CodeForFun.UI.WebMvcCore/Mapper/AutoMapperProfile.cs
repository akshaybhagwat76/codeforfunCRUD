using AutoMapper;
using CodeForFun.Repository.Entities.Concrete;
using CodeForFun.UI.WebMvcCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForFun.UI.WebMvcCore.Mapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Category, CategoryViewModel>();
			CreateMap<Product, ProductViewModel>()
				 .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
				  .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.Category.Id));
			CreateMap<ProductsToCustomer, ProductsToCustomerViewModel>();

			CreateMap<ProductViewModel, Product>()
				 .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId));
			CreateMap<CategoryViewModel, Category>();
		}
	}

}
