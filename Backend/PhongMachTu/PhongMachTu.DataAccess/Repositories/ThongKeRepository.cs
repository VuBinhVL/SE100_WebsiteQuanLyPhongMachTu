
using Microsoft.EntityFrameworkCore;
using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.Common.DTOs.Request.ThongKe;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
    public interface IThongKeRepository 
    {
        Task<Request_HienThiThongKeDTO> HienThiThongKeAsync(DateTime startDay, DateTime endDay);
        
    }

    public class ThongKeRepository : IThongKeRepository
    {
        private readonly PhongMachTuContext _context;
        public ThongKeRepository(IDbFactory dbFactory, PhongMachTuContext context) 
        {
            _context = context;

        }

        public async Task<Request_HienThiThongKeDTO> HienThiThongKeAsync(DateTime startDay, DateTime endDay)
        {

            var tongTienXetNghiem = await _context.ChiTietXetNghiems
                .Where(ctxn => ctxn.ChiTietKhamBenh != null &&
                               ctxn.ChiTietKhamBenh.PhieuKhamBenh != null &&
                               ctxn.ChiTietKhamBenh.PhieuKhamBenh.NgayTao >= startDay &&
                               ctxn.ChiTietKhamBenh.PhieuKhamBenh.NgayTao <= endDay)
                .SumAsync(ctxn => ctxn.GiaXetNghiem);
            var tongTienChupChieu = await _context.ChupChieus
                .Where(cc => cc.ChiTietKhamBenh != null &&
                             cc.ChiTietKhamBenh.PhieuKhamBenh != null &&
                             cc.ChiTietKhamBenh.PhieuKhamBenh.NgayTao >= startDay &&
                             cc.ChiTietKhamBenh.PhieuKhamBenh.NgayTao <= endDay)
                .SumAsync(cc => cc.Gia);
            var tongTienThuoc = await _context.ChiTietDonThuocs
                .Where(ctdt => ctdt.ChiTietKhamBenh != null &&
                               ctdt.ChiTietKhamBenh.PhieuKhamBenh != null &&
                               ctdt.ChiTietKhamBenh.PhieuKhamBenh.NgayTao >= startDay &&
                               ctdt.ChiTietKhamBenh.PhieuKhamBenh.NgayTao <= endDay)
                .SumAsync(ctdt => ctdt.DonGia * ctdt.SoLuong);
            var tongTienKhamBenh = await _context.ChiTietKhamBenhs
                .Where(ctkb => ctkb.PhieuKhamBenh != null &&
                               ctkb.PhieuKhamBenh.NgayTao >= startDay &&
                               ctkb.PhieuKhamBenh.NgayTao <= endDay)
                .SumAsync(ctkb => ctkb.GiaKham);
            var tongDoanhThu = tongTienXetNghiem + tongTienChupChieu + tongTienThuoc + tongTienKhamBenh;
            var tongSoThuoc = await _context.ChiTietPhieuNhapThuocs
                .Where(ct => ct.PhieuNhapThuoc != null &&
                             ct.PhieuNhapThuoc.NgayNhap >= startDay &&
                             ct.PhieuNhapThuoc.NgayNhap <= endDay)
                .Select(ct => ct.ThuocId)
                .Distinct()
                .CountAsync();
            var tongSoLuotKham = await _context.PhieuKhamBenhs
                .Where(pk => pk.NgayTao >= startDay && pk.NgayTao <= endDay)
                .CountAsync();
            var tongSoBenhNhan = await _context.PhieuKhamBenhs
                .Where(pk => pk.NgayTao >= startDay && pk.NgayTao <= endDay)
                .Select(pk => pk.LichKham.BenhNhanId)
                .Distinct()
                .CountAsync();

            return new Request_HienThiThongKeDTO
            {
                TongDoanhThu = tongDoanhThu,
                TongSoThuoc = tongSoThuoc,
                TongSoBenhNhan = tongSoBenhNhan,
                TongSoLuotKham = tongSoLuotKham
            };
        }
    }
}
