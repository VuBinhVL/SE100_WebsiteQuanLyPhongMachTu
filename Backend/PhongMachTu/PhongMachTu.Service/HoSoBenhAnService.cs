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
        Task<Request_HienThiHoSoBenhAnDTO> HienThiHoSoBenhAnAsync( HttpContext httpContext);
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
        public async Task<Request_HienThiHoSoBenhAnDTO> HienThiHoSoBenhAnAsync(HttpContext httpContext)
        {
            // Lấy thông tin người dùng từ HttpContext
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);

            // Tìm hồ sơ bệnh án tương ứng với người dùng
            var allHoSoBenhAn = await _hoSoBenhAnRepository.GetAllWithIncludeAsync(h => h.NhomBenh); // Chờ để nhận danh sách

            var findBenhNhan = allHoSoBenhAn
                .Where(d => d.BenhNhanId == nguoiDung.Id)
                .FirstOrDefault();

            // Kiểm tra nếu không tìm thấy hồ sơ
            if (findBenhNhan == null)
            {
                throw new Exception("Không tìm thấy hồ sơ bệnh án cho người dùng.");
            }

            // Tạo DTO kết quả
            var rs = new Request_HienThiHoSoBenhAnDTO()
            {
                IdBN = findBenhNhan.BenhNhanId,
                Id = findBenhNhan.Id,
                NhomBenh = findBenhNhan.NhomBenh?.TenNhomBenh ?? "Chưa xác định", // Lấy tên nhóm bệnh
                NgayTao = findBenhNhan.NgayTao
            };

            return rs;
        }



    }
}
