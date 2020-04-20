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
	public class ProductDetailsController : ControllerBase
	{
		private readonly IProductDetailsService _productService;
		private readonly IMapper _mapper;
		private readonly ICategoryService _categoryService;

		public ProductDetailsController(IProductDetailsService productService, ICategoryService categoryService, IMapper mapper)
		{
			_productService = productService;
			_categoryService = categoryService;
			_mapper = mapper;
		}

		// GET: api/Product
		[HttpGet]
		[Route("GetAll")]
		public async Task<List<ProductDetail>> Get()
		{
			var products = await _productService.GetListAsync();
			return _mapper.Map<List<ProductDetail>>(products);
		}

		[HttpGet]
		public async Task<ProductDetail> Get(int id)
		{
			var product =await _productService.GetAsync(id);

			if (product == null)
				return null;

			return product;
		}

		// POST: api/Product
		[HttpPost]
		public async Task<IActionResult> Post(ProductDetailViewModel product)
		{
			var newProduct = new ProductDetail()
			{
				Description = product.Description
			};

			_productService.AddAsync(newProduct);

			return Ok();
		}

		[HttpPut]
		public IActionResult Put([FromBody] ProductDetailViewModel product)
		{
			if (product == null)
				return BadRequest("Product is Null");

			_productService.UpdateAsync(_mapper.Map<ProductDetail>(product));


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
