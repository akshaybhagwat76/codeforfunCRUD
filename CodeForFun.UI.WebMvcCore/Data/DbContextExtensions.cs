using CodeForFun.Repository.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeForFun.UI.WebMvcCore.Data
{
	public static class DbContextExtensions
	{
		public static IQueryable Query(this RepositoryContext context, string entityName) =>
	context.Query(context.Model.FindEntityType(entityName).ClrType);

		static readonly MethodInfo SetMethod = typeof(RepositoryContext).GetMethod(nameof(RepositoryContext.Set));

		public static IQueryable Query(this RepositoryContext context, Type entityType) =>
			(IQueryable)SetMethod.MakeGenericMethod(entityType).Invoke(context, null);
	}
}
