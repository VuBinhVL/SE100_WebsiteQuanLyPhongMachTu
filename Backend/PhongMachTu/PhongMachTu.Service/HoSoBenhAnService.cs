using Microsoft.AspNetCore.Http;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.Common.DTOs.Request.HoSoBenhAn;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface IHoSoBenhAnService
    {
        Task<List<HoSoBenhAn>> GetAllAsync();
        Task<IEnumerable<Request_HienThiHoSoBenhAnDTO>> HienThiHoSoBenhAnAsync( HttpContext httpContext);
    }
    public class HoSoBenhAnService : IHoSoBenhAnService
    {
        private readonly IHoSoBenhAnRepository _hoSoBenhAnRepository;
        private readonly INguoiDungService _nguoiDungService;
        public HoSoBenhAnService(IHoSoBenhAnRepository hoSoBenhAnRepository, INguoiDungService nguoiDungService)
        {
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _nguoiDungService = nguoiDungService;
        }
        public async Task<List<HoSoBenhAn>> GetAllAsync()
        {
            return (await _hoSoBenhAnRepository.GetAllAsync()).ToList();
        }
        public async Task<IEnumerable<Request_HienThiHoSoBenhAnDTO>> HienThiHoSoBenhAnAsync(HttpContext httpContext)
        {
            // Lấy thông tin người dùng từ HttpContext
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);

            if (nguoiDung == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng.");
            }

            // Lấy danh sách hồ sơ bệnh án bao gồm cả thông tin nhóm bệnh và người dùng
            var allHoSoBenhAn = await _hoSoBenhAnRepository.GetAllWithIncludeAsync(h => h.NhomBenh, h => h.BenhNhan);

            // Lọc hồ sơ theo Id của bệnh nhân
            var findBenhNhan = allHoSoBenhAn
                .Where(d => d.BenhNhanId == nguoiDung.Id)
                .ToList();

            // Kiểm tra nếu không tìm thấy hồ sơ
            if (!findBenhNhan.Any())
            {
                throw new Exception("Không tìm thấy hồ sơ bệnh án cho người dùng.");
            }

            // Chuyển đổi danh sách hồ sơ sang DTO
            var result = findBenhNhan.Select(h => new Request_HienThiHoSoBenhAnDTO
            {
                IdBN = h.BenhNhanId,
                Id = h.Id,
                NhomBenh = h.NhomBenh?.TenNhomBenh ?? "Không xác định",
                NgayTao = h.NgayTao
            });

            return result;
        }




    }
}
