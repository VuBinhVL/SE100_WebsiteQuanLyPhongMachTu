using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IChiTietDonThuocRepository:IRepository<ChiTietDonThuoc>
	{

	}
	public class ChiTietDonThuocRepository : RepositoryBase<ChiTietDonThuoc>, IChiTietDonThuocRepository
	{
		public ChiTietDonThuocRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
