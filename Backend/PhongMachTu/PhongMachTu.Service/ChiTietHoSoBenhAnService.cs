using Microsoft.AspNetCore.Http;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietHoSoBenhAn;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Models;
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
        Task<Request_HienThiCTKBCuaChiTietHoSoBenhAnDTO> HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(HttpContext httpContext, int chiTietKhamBenhId);
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
        private readonly IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private readonly IChupChieuRepository _chupChieuRepository;
        public ChiTietHoSoBenhAnService(IChiTietHoSoBenhAnRepository chiTietHoSoBenhAnRepository, INguoiDungService nguoiDungService,
            IHoSoBenhAnRepository hoSoBenhAnRepository, IChiTietKhamBenhRepository chiTietKhamBenhRepository,
            IPhieuKhamBenhRepository phieuKhamBenhRepository, IBenhLyRepository benhLyRepository, IThuocRepository thuocRepository,
            IChiTietDonThuocRepository chiTietDonThuocRepository, IChiTietXetNghiemRepository chiTietXetNghiemRepository, IChupChieuRepository chupChieuRepository)
        {
            _chiTietHoSoBenhAnRepository = chiTietHoSoBenhAnRepository;
            _nguoiDungService = nguoiDungService;
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
            _benhLyRepository = benhLyRepository;
            _thuocRepository = thuocRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _chupChieuRepository = chupChieuRepository;
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
            //var hoSoBenhAn = (await _hoSoBenhAnRepository.GetAllWithIncludeAsync(h => h.ChiTietHoSoBenhAn))
            //    .FirstOrDefault(h => h.Id == HoSoBenhAnID);
            var hoSoBenhAn = (await _hoSoBenhAnRepository.GetSingleByIdAsync(HoSoBenhAnID));

            if (hoSoBenhAn == null)
            {
                throw new Exception("Hồ sơ bệnh án không tồn tại");
            }

            // Lấy danh sách ChiTietHoSoBenhAn liên quan đến hồ sơ bệnh án
            var chiTietHoSoBenhAns = (await _chiTietHoSoBenhAnRepository.GetAllAsync())
                .Where(cthsba => cthsba.HoSoBenhAnId == hoSoBenhAn.Id);

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

            // Lấy danh sách ChiTietDonThuoc liên quan
            var chiTietDonThuocs = (await _chiTietDonThuocRepository.GetAllAsync())
                .Where(ctdt => chiTietKhamBenhs.Any(ctkb => ctkb.Id == ctdt.ChiTietKhamBenhId))
                .ToList();

            // Lấy danh sách ChiTietXetNghiem liên quan
            var chiTietXetNghiems = (await _chiTietXetNghiemRepository.GetAllAsync())
                .Where(ctxn => chiTietKhamBenhs.Any(ctkb => ctkb.Id == ctxn.ChiTietKhamBenhId))
                .ToList();

            // Lấy danh sách ChupChieu liên quan
            var chupChieus = (await _chupChieuRepository.GetAllAsync())
                .Where(cc => chiTietKhamBenhs.Any(ctkb => ctkb.Id == cc.ChiTietKhamBenhId))
                .ToList();

            // Chuyển đổi dữ liệu sang DTO
            var chiTietHoSoDTOs = chiTietKhamBenhs.Select(ctkb =>
            {
                // Tính tổng tiền thuốc cho từng ChiTietKhamBenh
                var tongTienThuoc = chiTietDonThuocs
                    .Where(ctdt => ctdt.ChiTietKhamBenhId == ctkb.Id)
                    .Sum(ctdt => ctdt.SoLuong * ctdt.DonGia);

                // Tính tổng tiền xét nghiệm cho từng ChiTietKhamBenh
                var tongTienXetNghiem = chiTietXetNghiems
                    .Where(ctxn => ctxn.ChiTietKhamBenhId == ctkb.Id)
                    .Sum(ctxn => ctxn.GiaXetNghiem);

                // Tính tổng tiền chụp chiếu cho từng ChiTietKhamBenh
                var tongTienChupChieu = chupChieus
                    .Where(cc => cc.ChiTietKhamBenhId == ctkb.Id)
                    .Sum(cc => cc.Gia);

                // Tổng tiền cho DTO
                var tongTien = ctkb.GiaKham + tongTienThuoc + tongTienXetNghiem + tongTienChupChieu;

                return new Request_HienThiChiTietHoSoBenhAnDTO
                {
                    HoTenBacSi = ctkb.PhieuKhamBenh.LichKham.CaKham.BacSi?.HoTen,
                    TenBenhLy = ctkb.BenhLy?.TenBenhLy,
                    NgayKham = ctkb.PhieuKhamBenh.NgayTao,
                    TongTien = tongTien
                };
            }).ToList();

            return chiTietHoSoDTOs;
        }
        public async Task<Request_HienThiCTKBCuaChiTietHoSoBenhAnDTO> HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(HttpContext httpContext, int chiTietKhamBenhId)
        {
            // Lấy thông tin người dùng từ HttpContext
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                throw new Exception("Người dùng không hợp lệ");
            }

            // Lấy thông tin ChiTietKhamBenh
            var chiTietKhamBenh = (await _chiTietKhamBenhRepository.GetAllWithIncludeAsync(c => c.BenhLy, c => c.PhieuKhamBenh.LichKham.CaKham.BacSi))
                .Where(ctkb => ctkb.Id == chiTietKhamBenhId).FirstOrDefault();
            if (chiTietKhamBenh == null)
            {
                throw new Exception("Chi tiết khám bệnh không tồn tại");
            }

            // Lấy danh sách ChiTietDonThuoc liên quan
            var chiTietDonThuocs = (await _chiTietDonThuocRepository
                .GetAllWithIncludeAsync(ctdt => ctdt.Thuoc))
                .Where(ctdt => ctdt.ChiTietKhamBenhId == chiTietKhamBenh.Id);

            // Lấy danh sách ChiTietXetNghiem liên quan và bao gồm LoaiXetNghiem, DonViTinh
            var chiTietXetNghiems = (await _chiTietXetNghiemRepository
                .GetAllWithIncludeAsync(
                    ctxn => ctxn.LoaiXetNghiem,
                    ctxn => ctxn.LoaiXetNghiem!.DonViTinh
                ))
                .Where(ctxn => ctxn.ChiTietKhamBenhId == chiTietKhamBenh.Id);
            // Tạo danh sách thuốc DTO
            var danhSachThuoc = chiTietDonThuocs.Select(ctdt => new ThuocDTO
            {
                TenThuoc = ctdt.Thuoc?.TenThuoc,
                SoLuongThuoc = ctdt.SoLuong,
                DonGiaThuoc = ctdt.DonGia
            }).ToList();

            // Tạo danh sách xét nghiệm DTO
            var danhSachXetNghiem = chiTietXetNghiems.Select(ctxn => new XetNghiemDTO
            {
                TenXetNghiem = ctxn.LoaiXetNghiem?.TenXetNghiem,
                DVT = ctxn.LoaiXetNghiem?.DonViTinh?.TenDonViTinh,
                KetQua = ctxn.KetQua,
                DanhGia = ctxn.DanhGia,
                GiaXetNghiem = ctxn.GiaXetNghiem
            }).ToList();

            // Tạo DTO kết quả
            var result = new Request_HienThiCTKBCuaChiTietHoSoBenhAnDTO
            {
                TenBenhNhan = nguoiDung.HoTen,
                TenBacSi = chiTietKhamBenh.PhieuKhamBenh?.LichKham?.CaKham?.BacSi?.HoTen,
                TenBenhLy = chiTietKhamBenh.BenhLy?.TenBenhLy,
                NgayTaoPKB = chiTietKhamBenh.PhieuKhamBenh?.NgayTao ?? DateTime.MinValue,
                ChanDoan = chiTietKhamBenh.GhiChu,
                DanhSachThuoc = danhSachThuoc,
                DanhSachXetNghiem = danhSachXetNghiem
            };

            return result;
        }
    }
}
