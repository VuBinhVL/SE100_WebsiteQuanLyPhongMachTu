using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IChucNangRepository : IRepository<ChucNang>
	{

	}
	public class ChucNangRepository : RepositoryBase<ChucNang>, IChucNangRepository
	{
		public ChucNangRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
