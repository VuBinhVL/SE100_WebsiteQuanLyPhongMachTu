using Microsoft.EntityFrameworkCore;
using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.Common.DTOs.Request.LichKhamAdmin;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ILichKhamRepository : IRepository<LichKham>
	{
        Task<IEnumerable<LichKhamDTO>> GetListLichKhamDTOsAsync(int CaKhamId);
    }
	public class LichKhamRepository : RepositoryBase<LichKham>, ILichKhamRepository
	{
        private readonly PhongMachTuContext _context;
        public LichKhamRepository(IDbFactory dbFactory, PhongMachTuContext context) : base(dbFactory)
        {
            _context = context;
        }

        public async Task<IEnumerable<LichKhamDTO>> GetListLichKhamDTOsAsync(int CaKhamId)
        {
            return await _context.LichKhams
                .Include(ck => ck.CaKham) 
                .Include(ck => ck.BenhNhan) 
                .Include(ck => ck.TrangThaiLichKham)
                .Where(ck => ck.CaKhamId == CaKhamId)
                .Select(ck => new LichKhamDTO
                {
                    Id = ck.Id,
                    STT = ck.SoThuTu, 
                    TenBenhNhan = ck.BenhNhan.HoTen, 
                    TenTrangThai = ck.TrangThaiLichKham.TenTrangThai,
                })
                .ToListAsync();
        }

    }
}
