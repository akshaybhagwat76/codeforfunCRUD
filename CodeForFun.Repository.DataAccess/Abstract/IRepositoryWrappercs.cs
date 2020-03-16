using System;
using System.Collections.Generic;
using System.Text;

namespace CodeForFun.Repository.DataAccess.Abstract
{
	public interface IRepositoryWrapper
	{
		ICategory Category { get; }
		ICustomer Customer { get; }
		IProduct Product { get; }
		IProductDetails ProductDetails { get; }
		IProductsToCustomers ProductsToCustomers { get; }
	}
}
