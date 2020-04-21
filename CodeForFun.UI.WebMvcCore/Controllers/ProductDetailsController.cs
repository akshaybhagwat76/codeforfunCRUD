﻿using System;
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
		private readonly IProductDetailsService _productDetailService;
		private readonly IProductService _productService;

		private readonly IMapper _mapper;
		private readonly ICategoryService _categoryService;

		public ProductDetailsController(IProductDetailsService productDetailService, IProductService productService, ICategoryService categoryService, IMapper mapper)
		{
			_productDetailService = productDetailService;
			_categoryService = categoryService;
			_productService = productService;
			_mapper = mapper;
		}

		// GET: api/Product
		[HttpGet]
		[Route("GetAll")]
		public async Task<List<ProductDetail>> Get()
		{
			var products = await _productDetailService.GetListAsync();
			if (products != null)
			{
				foreach (var item in products)
				{
					if (item.IdNavigation == null)
					{
						var product = await _productService.GetAsync(item.Id);
						item.IdNavigation = product;
					}
				}
			}
			return _mapper.Map<List<ProductDetail>>(products);
		}

		[HttpGet]
		public async Task<ProductDetail> Get(int id)
		{
			var product =await _productDetailService.GetAsync(id);

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

			_productDetailService.AddAsync(newProduct);

			return Ok();
		}

		[HttpPut]
		public IActionResult Put([FromBody] ProductDetailViewModel product)
		{
			if (product == null)
				return BadRequest("Product is Null");

			_productDetailService.UpdateAsync(new ProductDetail { Description = product.Description,Id=product.Id});


			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(int productId)
		{
			var getProd = _productDetailService.GetAsync(productId);

			if (getProd.Result != null)
			{
				_productDetailService.DeleteAsync(getProd.Result);
			}

			return Ok();
		}
	}
}
