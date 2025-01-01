using Microsoft.EntityFrameworkCore;
using PhongMachTu.Common.DTOs.Request.LichKhamAdmin;
using PhongMachTu.Common.DTOs.Request.PhieuKhamBenh;
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
        Task<IEnumerable<PhieuKhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync();
    }
	public class PhieuKhamBenhRepository : RepositoryBase<PhieuKhamBenh>, IPhieuKhamBenhRepository
	{
        private readonly PhongMachTuContext _context;
        public PhieuKhamBenhRepository(IDbFactory dbFactory, PhongMachTuContext context) : base(dbFactory)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhieuKhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync()
        {
            return await _context.PhieuKhamBenhs
                .Include(ck => ck.LichKham)
                .ThenInclude(lk => lk.BenhNhan) // Include BenhNhan trong LichKham
                .Include(ck => ck.LichKham)
                .ThenInclude(lk => lk.CaKham)
                .Include(ck => ck.LichKham)
                .ThenInclude(lk => lk.TrangThaiLichKham)
                .Include(ck => ck.LichKham.CaKham.BacSi) // Include BacSi
                .Select(ck => new PhieuKhamBenhDTO
                {
                    TenBenhNhan = ck.LichKham.BenhNhan.HoTen,
                    TenBacSi = ck.LichKham.CaKham.BacSi.HoTen,
                    NgayTao = ck.NgayTao,
                    SoDienThoai = ck.LichKham.BenhNhan.SoDienThoai != null ? int.Parse(ck.LichKham.BenhNhan.SoDienThoai) : 0
                })
                .ToListAsync();
        }
    }
}
