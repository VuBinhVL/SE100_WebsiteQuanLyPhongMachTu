using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ICaKhamRepository: IRepository<CaKham>
	{
	}

	public class CaKhamRepository : RepositoryBase<CaKham>, ICaKhamRepository
	{
		public CaKhamRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
