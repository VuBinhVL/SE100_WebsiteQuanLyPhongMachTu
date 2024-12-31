using Microsoft.AspNetCore.Http;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietHoSoBenhAn;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface IChiTietHoSoBenhAnService
    {
        Task<ResponeMessage> HienThiChiTietHoSoBenhAnAsync(HttpContext httpContext);
        Task<ResponeMessage> HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(HttpContext httpContext, int phieuKhamBenhId);
    }
    public class ChiTietHoSoBenhAnService : IChiTietHoSoBenhAnService
    {
        private readonly IChiTietHoSoBenhAnRepository _chiTietHoSoBenhAnRepository;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IHoSoBenhAnRepository _hoSoBenhAnRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        private readonly IBenhLyRepository _benhLyRepository;
        private readonly IThuocRepository _thuocRepository;
        private readonly IChiTietDonThuocRepository _chiTietDonThuocRepository;
        public ChiTietHoSoBenhAnService(IChiTietHoSoBenhAnRepository chiTietHoSoBenhAnRepository, INguoiDungService nguoiDungService,
            IHoSoBenhAnRepository hoSoBenhAnRepository, IChiTietKhamBenhRepository chiTietKhamBenhRepository,
            IPhieuKhamBenhRepository phieuKhamBenhRepository, IBenhLyRepository benhLyRepository, IThuocRepository thuocRepository, IChiTietDonThuocRepository chiTietDonThuocRepository)
        {
            _chiTietHoSoBenhAnRepository = chiTietHoSoBenhAnRepository;
            _nguoiDungService = nguoiDungService;
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
            _benhLyRepository = benhLyRepository;
            _thuocRepository = thuocRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
        }
        public async Task<ResponeMessage> HienThiChiTietHoSoBenhAnAsync(HttpContext httpContext)
        {
            // Lấy thông tin người dùng từ HttpContext
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                return new ResponeMessage(HttpStatusCode.Unauthorized, "Người dùng không hợp lệ");
            }

            // Tìm hồ sơ bệnh án của người dùng
            var hoSoBenhAn = (await _hoSoBenhAnRepository.GetAllAsync())
                .FirstOrDefault(h => h.BenhNhanId == nguoiDung.Id);

            if (hoSoBenhAn == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Hồ sơ bệnh án không tồn tại");
            }

            // Lấy danh sách ChiTietKhamBenh liên quan qua bảng ChiTietHoSoBenhAn
            var chiTietKhamBenhs = (await _chiTietHoSoBenhAnRepository.GetAllAsync())
                .Where(cthba => cthba.HoSoBenhAnId == hoSoBenhAn.Id)
                .Select(cthba => cthba.ChiTietKhamBenh)
                .ToList();


            if (!chiTietKhamBenhs.Any())
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Không có chi tiết khám bệnh");
            }

            // Chuyển đổi dữ liệu sang DTO
            var chiTietHoSoDTOs = chiTietKhamBenhs.Select(c => new Request_HienThiChiTietHoSoBenhAnDTO
            {
                PhieuKhamBenhId = c.PhieuKhamBenhId,
                HoTenBacSi = c.PhieuKhamBenh.LichKham.CaKham.BacSi?.HoTen ?? "Chưa cập nhật",
                TenNhomBenh = hoSoBenhAn.NhomBenh?.TenNhomBenh ?? "Chưa xác định",
                NgayKham = c.PhieuKhamBenh.NgayTao,
                GiaKham = c.GiaKham
            }).ToList();

            // Chuyển đổi danh sách sang JSON
            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(chiTietHoSoDTOs);

            return new ResponeMessage(HttpStatusCode.Ok, responseJson);
        }

        public async Task<ResponeMessage> HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(HttpContext httpContext, int phieuKhamBenhId)
        {
            // Lấy thông tin người dùng từ HttpContext
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                return new ResponeMessage(HttpStatusCode.Unauthorized, "Người dùng không hợp lệ");
            }

            // Tìm hồ sơ bệnh án của người dùng
            var hoSoBenhAn = (await _hoSoBenhAnRepository.GetAllAsync())
                .FirstOrDefault(h => h.BenhNhanId == nguoiDung.Id);

            if (hoSoBenhAn == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Hồ sơ bệnh án không tồn tại");
            }

            // Tìm chi tiết khám bệnh của hồ sơ bệnh án
            var chiTietKhamBenh = (await _chiTietKhamBenhRepository.GetAllAsync())
                .FirstOrDefault(ctkb => ctkb.PhieuKhamBenhId == phieuKhamBenhId);

            if (chiTietKhamBenh == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Chi tiết khám bệnh không tồn tại");
            }

            // Lấy danh sách bệnh lý từ ChiTietKhamBenh và liên kết với BenhLy
            var benhLys = (await _chiTietKhamBenhRepository.GetAllAsync())
                .Where(ctkb => ctkb.PhieuKhamBenhId == phieuKhamBenhId)
                .Select(ctkb => ctkb.BenhLy.TenBenhLy)
                .ToList();

            // Lấy danh sách thuốc từ ChiTietDonThuoc liên kết với Thuoc
            var danhSachThuoc = (await _chiTietDonThuocRepository.GetAllAsync())
                .Where(ctdt => ctdt.ChiTietKhamBenh.PhieuKhamBenhId == phieuKhamBenhId)
                .Select(ctdt => new ThuocDTO
                {
                    TenThuoc = ctdt.Thuoc.TenThuoc,
                    SoLuongThuoc = ctdt.SoLuong,
                    DonGiaThuoc = ctdt.DonGia
                })
                .ToList();

            var findphieuKhamBenhbyID = await _phieuKhamBenhRepository.GetSingleByIdAsync(phieuKhamBenhId);
            var phieuKhamBenh = await _phieuKhamBenhRepository.GetAllAsync();
            var bacSiKham = phieuKhamBenh
                             .Where(p => p.Id == phieuKhamBenhId)
                             .Select(p => new
                             {
                                 BacSiHoTen = p.LichKham.CaKham.BacSi.HoTen,
                                 ChuyenMon = p.LichKham.CaKham.BacSi.ChuyenMon.TenNhomBenh
                             })
                             .FirstOrDefault();

            // Chuyển đổi dữ liệu sang DTO
            var chiTietHoSoDTO = new Request_HienThiPKBCuaChiTietHoSoBenhAnDTO
            {
                TenBenhNhan = hoSoBenhAn.BenhNhan.HoTen,
                GioiTinhBN = hoSoBenhAn.BenhNhan.GioiTinh,
                NgaySinhBN = hoSoBenhAn.BenhNhan.NgaySinh,
                GiaKham = chiTietKhamBenh.GiaKham,
                BenhLys = benhLys,
                GhiChu = chiTietKhamBenh.GhiChu,
                TenBacSi = bacSiKham.BacSiHoTen,
                NgayTaoPKB = findphieuKhamBenhbyID.NgayTao,
                TenNhomBenh = bacSiKham.ChuyenMon,
                DanhSachThuoc = danhSachThuoc
            };

            // Chuyển đổi danh sách sang JSON
            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(chiTietHoSoDTO);

            return new ResponeMessage(HttpStatusCode.Ok, responseJson);
        }

    }
}
