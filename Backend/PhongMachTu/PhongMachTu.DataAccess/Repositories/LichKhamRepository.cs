using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ILichKhamRepository : IRepository<LichKham>
	{

	}
	public class LichKhamRepository : RepositoryBase<LichKham>, ILichKhamRepository
	{
		public LichKhamRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
