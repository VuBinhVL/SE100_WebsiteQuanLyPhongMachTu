using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IDonViTinhRepository : IRepository<DonViTinh>
	{

	}
	public class DonViTinhRepository : RepositoryBase<DonViTinh>, IDonViTinhRepository
	{
		public DonViTinhRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
