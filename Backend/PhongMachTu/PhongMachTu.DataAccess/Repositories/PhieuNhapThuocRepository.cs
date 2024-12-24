using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IPhieuNhapThuocRepository : IRepository<PhieuNhapThuoc>
	{

	}
	public class PhieuNhapThuocRepository : RepositoryBase<PhieuNhapThuoc>, IPhieuNhapThuocRepository
	{
		public PhieuNhapThuocRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
