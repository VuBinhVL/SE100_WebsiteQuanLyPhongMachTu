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
	public interface INguoiDungRepository : IRepository<NguoiDung>
	{
		Task<IEnumerable<NguoiDung>> GetNhanViensWithChuyenMonAsync();

    }
	public class NguoiDungRepository : RepositoryBase<NguoiDung>, INguoiDungRepository
	{
        private readonly PhongMachTuContext _context;
        public NguoiDungRepository(IDbFactory dbFactory,PhongMachTuContext context) : base(dbFactory)
		{
            _context = context;
		}
        public async Task<IEnumerable<NguoiDung>> GetNhanViensWithChuyenMonAsync()
        {
            return await _context.NguoiDungs
                .Include(nd => nd.ChuyenMon) // Include để lấy thông tin từ bảng NhomBenhs
                .Where(nd => nd.VaiTroId == 2) // Chỉ lấy VaiTroId = 2
                .ToListAsync();
        }

    }
}
