using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IVaiTroRepository : IRepository<VaiTro>
	{

	}
	public class VaiTroRepository : RepositoryBase<VaiTro>, IVaiTroRepository
	{
		public VaiTroRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
