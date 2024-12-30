using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface ICaKhamRepository: IRepository<CaKham>
	{
        Task<IEnumerable<Request_HienThiCaKhamDTO>> GetCaKhamDaDangKyAsync();
    }

	public class CaKhamRepository : RepositoryBase<CaKham>, ICaKhamRepository
	{
        private readonly PhongMachTuContext _context;
        public CaKhamRepository(IDbFactory dbFactory, PhongMachTuContext context) : base(dbFactory)
		{
            _context = context;

        }
        public async Task<IEnumerable<Request_HienThiCaKhamDTO>> GetCaKhamDaDangKyAsync()
        {
            return await _context.CaKhams
                .Where(ca => ca.BacSiId != null)
                .Select(ca => new Request_HienThiCaKhamDTO
                {
                    ThoiGianBatDau = ca.ThoiGianBatDau,
                    ThoiGianKetThuc = ca.ThoiGianKetThuc,
                    NgayKham = ca.NgayKham,
                    SoLuongBenhNhanToiDa = ca.SoLuongBenhNhanToiDa,
                    SoLuongBenhNhanDaDanKi = ca.LichKhams.Count(l => l.TrangThaiLichKhamId != 3), // Assuming 3 is for 'canceled' status
                    BacSiId = ca.BacSiId,
                    ChuyenMonId = ca.BacSi != null ? ca.BacSi.ChuyenMonId : null 
                })
                .ToListAsync();
        }
       
    }
}
