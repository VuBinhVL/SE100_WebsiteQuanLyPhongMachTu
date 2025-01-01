using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ISuChoPhepRepository : IRepository<SuChoPhep>
	{

	}
	public class SuChoPhepRepository : RepositoryBase<SuChoPhep>, ISuChoPhepRepository
	{
		public SuChoPhepRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
