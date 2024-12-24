using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Infrastructure
{
	public interface IDbFactory
	{
		PhongMachTuContext Init();
	}


	public class DbFactory : IDbFactory
	{
		private readonly DbContextOptions<PhongMachTuContext> options;
		private PhongMachTuContext? dbContext;

		public DbFactory(DbContextOptions<PhongMachTuContext> options)
		{
			this.options = options;
		}

		public PhongMachTuContext Init()
		{
			return dbContext ??= new PhongMachTuContext(options);
		}
	}
}
