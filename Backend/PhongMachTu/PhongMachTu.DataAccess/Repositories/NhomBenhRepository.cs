using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface INhomBenhRepository : IRepository<NhomBenh>
	{

	}
	public class NhomBenhRepository : RepositoryBase<NhomBenh>, INhomBenhRepository
	{
		public NhomBenhRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
