using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForFun.Repository.Business.Abstract.Services;
using CodeForFun.Repository.Entities.Concrete;
using CodeForFun.UI.WebMvcCore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeForFun.UI.WebMvcCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsToCustomerController : ControllerBase
    {
		private readonly IProductsToCustomer _productsToCustomer;
		private readonly ICustomerService _CustomerService;
		private readonly IProductService _productService;

		public ProductsToCustomerController(IProductsToCustomer productsToCustomer, ICustomerService CustomerService, IProductService productService)
		{
			_productsToCustomer = productsToCustomer;
			_CustomerService = CustomerService;
			_productService = productService;
		}

		// GET: api/ProductsToCustomer
		[HttpGet]
		[Route("GetAll")]
		public Task<List<ProductsToCustomer>> Get()
		{
			return _productsToCustomer.GetAllWithInclude(x => x.Customer, x => x.Product);
		}

		[HttpPost]
		public ActionResult Post(ProductsToCustomerViewModel productsToCustomer)
		{
			try
			{
				var customer = _CustomerService.GetByName(productsToCustomer.CustomerName).Result.Id;
				var product = _productService.GetByName(productsToCustomer.ProductName).Result.Id;

				var productToCustomer = new ProductsToCustomer
				{
					CustomerId = customer,
					ProductId = product
				};

				_productsToCustomer.AddAsync(productToCustomer);

			}catch(Exception ex)
			{
				return BadRequest("Some Error while add Products To Customer");
			}
			return Ok();
		}

	}
}