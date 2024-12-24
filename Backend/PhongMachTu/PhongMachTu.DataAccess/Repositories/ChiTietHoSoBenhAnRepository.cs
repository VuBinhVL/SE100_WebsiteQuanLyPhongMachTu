using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IChiTietHoSoBenhAnRepository:IRepository<ChiTietHoSoBenhAn>
	{

	}
	public class ChiTietHoSoBenhAnRepository : RepositoryBase<ChiTietHoSoBenhAn>, IChiTietHoSoBenhAnRepository
	{
		public ChiTietHoSoBenhAnRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
