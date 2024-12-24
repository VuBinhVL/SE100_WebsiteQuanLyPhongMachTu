using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IPhieuKhamBenhRepository : IRepository<PhieuKhamBenh>
	{

	}
	public class PhieuKhamBenhRepository : RepositoryBase<PhieuKhamBenh>, IPhieuKhamBenhRepository
	{
		public PhieuKhamBenhRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
