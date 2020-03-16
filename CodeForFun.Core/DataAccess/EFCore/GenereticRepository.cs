using CodeForFun.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeForFun.Core.DataAccess.EFCore
{
	public class GenereticRepository<TEntity, TContext> : EfEntityRepository<TEntity, TContext>,
		IEntityRepository<TEntity>,IGenereticRepository<TEntity>
		where TEntity : class, IEntity, new()
		where TContext : DbContext, new()
	{
		private TContext _context;
		private DbSet<TEntity> _entities;

		public GenereticRepository(TContext context) : base(context)
		{
			_context = context ?? new TContext();
			_entities = _context.Set<TEntity>();
		}

		public async Task<List<TEntity>> GetAllWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			return await Include(includeProperties).ToListAsync();
		}

		public TEntity Get(Func<TEntity, bool> predicate)
		{
			return _entities.AsNoTracking().SingleOrDefault(predicate);
		}

		private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = _entities.AsQueryable();
			foreach (var include in includeProperties)
				query = query.Include(include);
			return query;
		}
	}
}
