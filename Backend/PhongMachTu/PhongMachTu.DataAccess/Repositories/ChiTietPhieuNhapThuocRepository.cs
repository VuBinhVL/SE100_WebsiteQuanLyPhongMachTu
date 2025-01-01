using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IChiTietPhieuNhapThuocRepository : IRepository<ChiTietPhieuNhapThuoc>
	{

	}
	public class ChiTietPhieuNhapThuocRepository : RepositoryBase<ChiTietPhieuNhapThuoc>, IChiTietPhieuNhapThuocRepository
	{
		public ChiTietPhieuNhapThuocRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
