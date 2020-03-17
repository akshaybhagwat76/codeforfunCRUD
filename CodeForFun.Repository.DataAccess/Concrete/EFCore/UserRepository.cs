using CodeForFun.Core.DataAccess.EFCore;
using CodeForFun.Repository.DataAccess.Abstract;
using CodeForFun.Repository.DataAccess.DbContexts;
using CodeForFun.UI.WebMvcCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeForFun.Repository.DataAccess.Concrete.EFCore
{
	public class UserRepository:GenereticRepository<User, RepositoryContext>,IUser
	{
		public UserRepository(RepositoryContext context) : base(context) { }
}
}
