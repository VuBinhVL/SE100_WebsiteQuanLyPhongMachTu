using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ILoaiXetNghiemRepository : IRepository<LoaiXetNghiem>
	{

	}
	public class LoaiXetNghiemRepository : RepositoryBase<LoaiXetNghiem>, ILoaiXetNghiemRepository
	{
		public LoaiXetNghiemRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
