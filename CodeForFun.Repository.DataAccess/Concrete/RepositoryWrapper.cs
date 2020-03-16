﻿using CodeForFun.Core;
using CodeForFun.Core.DataAccess.EFCore;
using CodeForFun.Repository.DataAccess.Abstract;
using CodeForFun.Repository.DataAccess.Concrete.EFCore;
using CodeForFun.Repository.DataAccess.DbContexts;
using CodeForFun.Repository.DataAccess.EFCore;
using CodeForFun.Repository.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeForFun.Repository.DataAccess.Concrete
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private RepositoryContext _context;
		private ICategory _category;
		private ICustomer _customer;
		private IProduct _product;
		private IProductDetails _productDetails;
		private IProductsToCustomers _productsToCustomers;

		public RepositoryWrapper(RepositoryContext context = null)
		{
			_context = context ?? new RepositoryContext();
	}

		public ICategory Category => _category ?? new CategoryRepository(_context);

		public ICustomer Customer => _customer ?? new CustomerRepository(_context);

		public IProduct Product => _product ?? new ProductRepository(_context);

		public IProductDetails ProductDetails => _productDetails ?? new ProductDetailsRepository(_context);

		public IProductsToCustomers ProductsToCustomers =>
			_productsToCustomers ?? new ProductsToCustomersRepository(_context);
	}
}
