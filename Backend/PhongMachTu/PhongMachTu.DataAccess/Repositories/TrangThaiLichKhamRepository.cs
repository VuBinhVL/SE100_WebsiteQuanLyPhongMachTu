using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ITrangThaiLichKhamRepository : IRepository<TrangThaiLichKham>
	{

	}
	public class TrangThaiLichKhamRepository : RepositoryBase<TrangThaiLichKham>, ITrangThaiLichKhamRepository
	{
		public TrangThaiLichKhamRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
