using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IThuocRepository : IRepository<Thuoc>
	{

	}
	public class ThuocRepository : RepositoryBase<Thuoc>, IThuocRepository
	{
		public ThuocRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
