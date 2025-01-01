using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ILoaiThuocRepository : IRepository<LoaiThuoc>
	{

	}
	public class LoaiThuocRepository : RepositoryBase<LoaiThuoc>, ILoaiThuocRepository
	{
		public LoaiThuocRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
