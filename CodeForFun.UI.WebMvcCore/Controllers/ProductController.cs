using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeForFun.Repository.Business.Abstract.Services;
using CodeForFun.Repository.Entities.Concrete;
using CodeForFun.UI.WebMvcCore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeForFun.UI.WebMvcCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;
		private readonly ICategoryService _categoryService;

		public ProductController(IProductService productService,ICategoryService categoryService,IMapper mapper)
		{
			_productService = productService;
			_categoryService = categoryService;
			_mapper = mapper;
		}

		// GET: api/Product
		[HttpGet]
		[Route("GetAll")]
		public async Task<List<ProductViewModel>> Get()
		{
			var products = await _productService.GetAllWithInclude(x => x.Category);
			return _mapper.Map<List<ProductViewModel>>(products);
		}

		[HttpGet]
		public Task<Product> Get(int id)
		{
			var product = _productService.GetAsync(id);

			if (product == null)
				return null;

			return product;
		}

		// POST: api/Product
		[HttpPost]
		public IActionResult Post(ProductViewModel product)
		{
			var category = _categoryService.GetByName(product.CategoryName);

			var newProduct = new Product()
			{
				CategoryId = category.Id,
				Code = product.Code,
				DateRegister = product.DateRegister,
				IsActive = product.IsActive,
				UnitPrice = product.UnitPrice,
				Name = product.Name
			};

			_productService.AddAsync(newProduct);

			return Ok();
		}

		[HttpPut]
		public IActionResult Put([FromBody] ProductViewModel product)
		{
			if (product == null)
				return BadRequest("Product is Null");

				_productService.UpdateAsync(_mapper.Map<Product>(product));
			

			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(int productId)
		{
			var getProd = _productService.GetAsync(productId);

			if (getProd.Result != null)
			{
				_productService.DeleteAsync(getProd.Result);
			}

			return Ok();
		}
	}
}
