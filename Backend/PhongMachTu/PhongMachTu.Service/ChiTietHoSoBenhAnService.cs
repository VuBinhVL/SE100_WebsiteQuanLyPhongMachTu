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
    }
    public class ChiTietHoSoBenhAnService : IChiTietHoSoBenhAnService
    {
        private readonly IChiTietHoSoBenhAnRepository _chiTietHoSoBenhAnRepository;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IHoSoBenhAnRepository _hoSoBenhAnRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        public ChiTietHoSoBenhAnService(IChiTietHoSoBenhAnRepository chiTietHoSoBenhAnRepository, INguoiDungService nguoiDungService, IHoSoBenhAnRepository hoSoBenhAnRepository, IChiTietKhamBenhRepository chiTietKhamBenhRepository, IPhieuKhamBenhRepository phieuKhamBenhRepository)
        {
            _chiTietHoSoBenhAnRepository = chiTietHoSoBenhAnRepository;
            _nguoiDungService = nguoiDungService;
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
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

    }
}
