using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface INguoiDungRepository : IRepository<NguoiDung>
	{

	}
	public class NguoiDungRepository : RepositoryBase<NguoiDung>, INguoiDungRepository
	{
		public NguoiDungRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
