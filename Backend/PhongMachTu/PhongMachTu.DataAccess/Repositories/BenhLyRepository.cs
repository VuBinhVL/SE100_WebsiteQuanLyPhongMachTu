using Microsoft.EntityFrameworkCore;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IBenhLyRepository:IRepository<BenhLy>
	{
        IQueryable<BenhLy> Query();

    }
	public class BenhLyRepository : RepositoryBase<BenhLy>, IBenhLyRepository
	{
        private readonly PhongMachTuContext _context;
        public BenhLyRepository(IDbFactory dbFactory, PhongMachTuContext context ) : base(dbFactory)
		{
            _context = context;
        }
        public IQueryable<BenhLy> Query()
        {
            return _context.BenhLys.AsQueryable();
        }

    }
}
