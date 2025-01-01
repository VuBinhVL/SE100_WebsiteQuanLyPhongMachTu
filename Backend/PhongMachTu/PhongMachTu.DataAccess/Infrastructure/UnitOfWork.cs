using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Infrastructure
{

	public interface IUnitOfWork
	{
		Task CommitAsync();
	}


	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDbFactory dbFactory;
		private PhongMachTuContext? dbContext;

		public UnitOfWork(IDbFactory dbFactory)
		{
			this.dbFactory = dbFactory;
		}
		public PhongMachTuContext DbContext => dbContext ?? (dbContext = dbFactory.Init());

		public async Task CommitAsync()
		{
			await DbContext.SaveChangesAsync();
		}

	}
}
