using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Infrastructure
{
	public interface IRepository<T> where T : class
	{
		Task<T> AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task<bool> DeleteByIdAsync(int id);
		Task<T?> GetSingleByIdAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
		Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
		Task<T?> GetSingleWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
		Task<(IEnumerable<T> Items, int Total, int Remaining)> GetPagedListAsync(Expression<Func<T, bool>>? predicate = null, int pageIndex = 0, int pageSize = 10, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);
	}


	public abstract class RepositoryBase<T> : IRepository<T> where T : class
	{
		#region Properties
		private PhongMachTuContext? dbContext;
		private readonly DbSet<T> dbSet;

		protected IDbFactory DbFactory { get; private set; }

		protected PhongMachTuContext DbContext => dbContext ??= DbFactory.Init();//nếu context null thì khởi tạo
		protected DbSet<T> DbSet => dbSet;
		#endregion

		protected RepositoryBase(IDbFactory dbFactory)
		{
			DbFactory = dbFactory;
			dbSet = DbContext.Set<T>();
		}

		#region Implementation

		public async Task<T> AddAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			return entity;
		}
		public void Update(T entity)
		{
			dbSet.Update(entity);
		}
		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}
		public async Task<bool> DeleteByIdAsync(int id)
		{
			var entity = await dbSet.FindAsync(id);
			if (entity == null) return false;

			dbSet.Remove(entity);
			return true;
		}

		//trả về null nếu không tìm thấy
		public async Task<T?> GetSingleByIdAsync(int id)
		{
			return await dbSet.FindAsync(id);
		}

		//trả về list empty nếu k có bản ghi nào
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await dbSet.ToListAsync();
		}


		//trả về list empty nếu k có bản ghi nào
		public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await dbSet.Where(predicate).ToListAsync();
		}

		//trả về list empty nếu k có bản ghi nào
		public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = dbSet;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return await query.ToListAsync();
		}


		//trả về list empty nếu k có bản ghi nào
		public async Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = dbSet;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return await query.Where(predicate).ToListAsync();
		}


		//trả về null nếu không tìm thấy
		public async Task<T?> GetSingleWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = dbSet;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return await query.FirstOrDefaultAsync(predicate);
		}

		//trả về list empty nếu k có bản ghi nào
		public async Task<(IEnumerable<T> Items, int Total, int Remaining)> GetPagedListAsync(Expression<Func<T, bool>>? predicate = null, int pageIndex = 0, int pageSize = 10, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
		{
			int skipCount = pageIndex * pageSize;

			IQueryable<T> query = dbSet;

			if (includes != null && includes.Length > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			var total = await query.CountAsync();

			var remaining = Math.Max(0, total - ((pageIndex + 1) * pageSize));

			query = skipCount == 0 ?
				query.Take(pageSize) :
				query.Skip(skipCount).Take(pageSize);

			return (await query.ToListAsync(), total, remaining);
		}
		#endregion
	}
}
