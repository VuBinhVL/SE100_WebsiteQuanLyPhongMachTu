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
        Task<IEnumerable<Request_HienThiChiTietHoSoBenhAnDTO>> HienThiChiTietHoSoBenhAnAsync(HttpContext httpContext, int HoSoBenhAnID);
        Task<Request_HienThiPKBCuaChiTietHoSoBenhAnDTO> HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(HttpContext httpContext, int phieuKhamBenhId);
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
        public async Task<IEnumerable<Request_HienThiChiTietHoSoBenhAnDTO>> HienThiChiTietHoSoBenhAnAsync(HttpContext httpContext, int HoSoBenhAnID)
        {
            // Lấy thông tin người dùng từ HttpContext
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                throw new Exception("Người dùng không hợp lệ");
            }

            // Tìm hồ sơ bệnh án của người dùng
            var hoSoBenhAn = (await _hoSoBenhAnRepository.GetAllWithIncludeAsync(h => h.ChiTietHoSoBenhAn))
                .FirstOrDefault(h => h.Id == HoSoBenhAnID);

            if (hoSoBenhAn == null)
            {
                throw new Exception("Hồ sơ bệnh án không tồn tại");
            }

            // Lấy danh sách ChiTietHoSoBenhAn liên quan đến hồ sơ bệnh án
            var chiTietHoSoBenhAns = (await _chiTietHoSoBenhAnRepository.GetAllAsync()).Where(cthsba => cthsba.HoSoBenhAnId == hoSoBenhAn.Id);
            if (chiTietHoSoBenhAns == null || !chiTietHoSoBenhAns.Any())
            {
                throw new Exception("Không có chi tiết hồ sơ bệnh án nào liên quan");
            }

            // Lấy danh sách ChiTietKhamBenh từ ChiTietHoSoBenhAn
            var chiTietKhamBenhs = (await _chiTietKhamBenhRepository.GetAllWithIncludeAsync(c => c.BenhLy, c => c.PhieuKhamBenh.LichKham.CaKham.BacSi))
                .Where(ctkb => chiTietHoSoBenhAns.Any(cthsba => cthsba.ChiTietKhamBenhId == ctkb.Id))
                .ToList();

            if (!chiTietKhamBenhs.Any())
            {
                throw new Exception("Không có chi tiết khám bệnh nào liên quan");
            }

            // Chuyển đổi dữ liệu sang DTO
            var chiTietHoSoDTOs = chiTietKhamBenhs.Select(ctkb => new Request_HienThiChiTietHoSoBenhAnDTO
            {
                HoTenBacSi = ctkb.PhieuKhamBenh.LichKham.CaKham.BacSi?.HoTen ,
                TenBenhLy = ctkb.BenhLy?.TenBenhLy,
                NgayKham = ctkb.PhieuKhamBenh.NgayTao,
                GiaKham = ctkb.GiaKham
            }).ToList();

            return chiTietHoSoDTOs;
        }


        public async Task<Request_HienThiPKBCuaChiTietHoSoBenhAnDTO> HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(HttpContext httpContext, int phieuKhamBenhId)
        {
            // Lấy thông tin người dùng từ HttpContext
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                throw new Exception("Người dùng không hợp lệ");
            }

            // Tìm hồ sơ bệnh án của người dùng
            var hoSoBenhAn = (await _hoSoBenhAnRepository.GetAllAsync())
                .FirstOrDefault(h => h.BenhNhanId == nguoiDung.Id);

            if (hoSoBenhAn == null)
            {
                throw new Exception("Hồ sơ bệnh án không tồn tại");
            }

            // Tìm chi tiết khám bệnh của hồ sơ bệnh án
            var chiTietKhamBenh = (await _chiTietKhamBenhRepository.GetAllAsync())
                .FirstOrDefault(ctkb => ctkb.PhieuKhamBenhId == phieuKhamBenhId);

            if (chiTietKhamBenh == null)
            {
                throw new Exception("Chi tiết khám bệnh không tồn tại");
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

        

            return chiTietHoSoDTO;
        }

    }
}
