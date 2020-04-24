using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForFun.Repository.Business.Abstract.Services;
using CodeForFun.Repository.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeForFun.UI.WebMvcCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsToCustomerController : ControllerBase
    {
		private readonly IProductsToCustomer _productsToCustomer;

		public ProductsToCustomerController(IProductsToCustomer productsToCustomer)
		{
			_productsToCustomer = productsToCustomer;
		}

		// GET: api/ProductsToCustomer
		[HttpGet]
		[Route("GetAll")]
		public Task<List<ProductsToCustomer>> Get()
		{
			return _productsToCustomer.GetAllWithInclude(x => x.Customer, x => x.Product);
		}

	}
}