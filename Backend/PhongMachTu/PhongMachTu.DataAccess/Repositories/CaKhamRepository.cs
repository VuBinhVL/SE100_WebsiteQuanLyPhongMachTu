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
        IQueryable<CaKham> Query();
        Task<IEnumerable<CaKhamDTO>> GetCaKhamsWithTenBacSiAndTenNhomBenhAsync();
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
                .Where(ca => ca.BacSiId != null) // Lọc những ca khám đã được bác sĩ đăng ký
                .Select(ca => new Request_HienThiCaKhamDTO
                {
                    ThoiGianBatDau = ca.ThoiGianBatDau,
                    ThoiGianKetThuc = ca.ThoiGianKetThuc,
                    NgayKham = ca.NgayKham,
                    SoLuongBenhNhanToiDa = ca.SoLuongBenhNhanToiDa,
                    SoLuongBenhNhanDaDanKi = ca.LichKhams.Count(l => l.TrangThaiLichKhamId != 4), // Đếm số lượng bệnh nhân không bị hủy với trạng thái đã hủy là 4
                    TenBacSi = ca.BacSi != null ? ca.BacSi.HoTen : string.Empty, 
                    TenChuyenMon = ca.BacSi != null && ca.BacSi.ChuyenMon != null ? ca.BacSi.ChuyenMon.TenNhomBenh : null 
                })
                .ToListAsync();
        }
        public IQueryable<CaKham> Query()
        {
            return _context.CaKhams.AsQueryable();
        }

        public async Task<IEnumerable<CaKhamDTO>> GetCaKhamsWithTenBacSiAndTenNhomBenhAsync()
        {
            return await _context.CaKhams
                .Include(ck => ck.BacSi)             
                .Include(ck => ck.BacSi.ChuyenMon)   
                .Select(ck => new CaKhamDTO          
                {
                    TenCaKham = ck.TenCaKham,
                    ThoiGianBatDau = ck.ThoiGianBatDau,
                    ThoiGianKetThuc = ck.ThoiGianKetThuc,
                    NgayKham = ck.NgayKham,
                    BacSiKham = ck.BacSi.HoTen,             
                    TenNhomBenh = ck.BacSi.ChuyenMon.TenNhomBenh 
                })
                .ToListAsync();
        }



    }
}
