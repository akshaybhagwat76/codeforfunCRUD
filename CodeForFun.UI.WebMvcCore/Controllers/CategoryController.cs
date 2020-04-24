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

			return _mapper.Map<IEnumerable<CategoryViewModel>>(categories.Where(x => x.Parent == null));
		}


		// GET: api/Category
		[HttpGet]
		[Route("GetAllWithParents")]
		public async Task<IEnumerable<CategoryViewModelWithParent>> GetAllWithParents()
		{
			var categories = _mapper.Map<IEnumerable<CategoryViewModelWithParent>>(await _categoryService.GetListAsync());

			return categories;
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
		public IActionResult Post([FromBody] CategoryViewModel category)
		{
			var cat = _categoryService.GetByName(category.Name);
			if (cat.Result != null)
			{
				return BadRequest("Category already Exist");
			}
			_categoryService.AddAsync(_mapper.Map<Category>(category));
			

			return Ok();
		}

		[HttpPut]
		public IActionResult Put([FromBody] CategoryViewModelWithParent category)
		{
			if (category == null)
				return BadRequest("Category is Null");

			Category element =  _categoryService.GetAsync(category.Id).Result;
			element.Name = category.Name;
			if (category.ParentName != null)
			{
				Category parent = _categoryService.GetByName(category.ParentName).Result;
				element.ParentId = parent.Id;

			}

			_categoryService.UpdateAsync(element);

			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(string categoryName)
		{
			var category = _categoryService.GetByName(categoryName);

			if (category.Result != null)
			{
				_categoryService.DeleteAsync(category.Result);
			}

			return Ok(categoryName);
		}
	}
}
