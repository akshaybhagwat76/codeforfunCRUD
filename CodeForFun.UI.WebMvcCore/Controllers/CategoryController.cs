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
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService CategoryService, IMapper mapper)
		{
			_categoryService = CategoryService;
			_mapper = mapper;
		}

		// GET: api/Category
		[HttpGet]
		[Route("GetAll")]
		public async Task<IEnumerable<CategoryViewModel>> Get()
		{
			var categories = await _categoryService.GetListAsync();

			return _mapper.Map<IEnumerable<CategoryViewModel>>(categories.Where(x=>x.Parent != null));
		}

		[HttpGet]
		public Task<Category> Get(int id)
		{
			var Category = _categoryService.GetAsync(id);

			if (Category == null)
				return null;

			return Category;
		}

		// POST: api/Category
		[HttpPost]
		public IActionResult Post([FromBody] Category Category)
		{
			_categoryService.AddAsync(Category);

			return Ok();
		}

		[HttpPut]
		public IActionResult Put([FromBody] Category Category)
		{
			if (Category == null)
				return BadRequest("Category is Null");

			var getProd = _categoryService.GetAsync(Category.Id);

			if (getProd != null)
			{
				_categoryService.UpdateAsync(Category);
			}

			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var getProd = _categoryService.GetAsync(id);

			if (getProd.Result != null)
			{
				_categoryService.DeleteAsync(getProd.Result);
			}

			return Ok();
		}
	}
}
