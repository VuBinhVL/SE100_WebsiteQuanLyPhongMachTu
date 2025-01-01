using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IChiTietXetNghiemRepository : IRepository<ChiTietXetNghiem>
	{

	}
	public class ChiTietXetNghiemRepository : RepositoryBase<ChiTietXetNghiem>, IChiTietXetNghiemRepository
	{
		public ChiTietXetNghiemRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
