using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IChupChieuRepository : IRepository<ChupChieu>
	{

	}
	public class ChupChieuRepository : RepositoryBase<ChupChieu>, IChupChieuRepository
	{
		public ChupChieuRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
