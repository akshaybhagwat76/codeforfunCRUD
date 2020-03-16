using CodeForFun.Repository.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeForFun.Repository.Business.Abstract.Services
{
	public interface ICategoryService:IService<Category>, IServiceForQueryable<Category>
	{
		public Category GetByName(string name);
	}
}
