using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.BenhNhan;
using PhongMachTu.Common.DTOs.Request.NguoiDung;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.Helpers;
using PhongMachTu.Common.Security;
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
    public interface IBenhNhanService
    {
        Task<ResponeMessage> RegisterAsync(Request_RegisterDTO data);
        Task<ResponeMessage> HienThiDanhSachBenhNhanAsync();
    }

    public class BenhNhanService : IBenhNhanService
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BenhNhanService(INguoiDungRepository nguoiDungRepository, IUnitOfWork unitOfWork)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> RegisterAsync(Request_RegisterDTO data)
        {

            if(!Validation.IsAlphanumeric(data.TenTaiKhoan))
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên tài khoản không được có kí tự đặc biệt");
            }
            if (!Validation.IsAlphanumeric(data.MatKhau))
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Mật khẩu không được có kí tự đặc biệt");
            }
            var checkEmail = (await _nguoiDungRepository.FindAsync(u => u.Email == data.Email)).FirstOrDefault();
            if (checkEmail != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Email đã có người sử dụng");
            }

            var checkSDT = (await _nguoiDungRepository.FindAsync(u => u.SoDienThoai == data.SoDienThoai)).FirstOrDefault();
            if (checkSDT != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Số điện thoại đã có người sử dụng");
            }
            
            var checkUserName = (await _nguoiDungRepository.FindAsync(u => u.TenTaiKhoan == data.TenTaiKhoan)).FirstOrDefault();
            if (checkUserName != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên tài khoản đã tồn tại");
            }
            //dữ liệu từ request
            var nguoiDung = new NguoiDung()
            {
                HoTen = data.HoTen,
                Email = data.Email,
                SoDienThoai = data.SoDienThoai,
                TenTaiKhoan = data.TenTaiKhoan,
                MatKhau = EncryptionHelper.Encrypt(data.MatKhau)
            };

            //dữ liệu default
            nguoiDung.Image = "no_img.png";
            nguoiDung.DiaChi = "Chưa cập nhật";
            nguoiDung.GioiTinh = "Khác";
            nguoiDung.NgaySinh = null;
            nguoiDung.ChuyenMonId = null;
            nguoiDung.VaiTroId = 3;
            nguoiDung.IsLock = false;   

            await _nguoiDungRepository.AddAsync(nguoiDung);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Đăng ký thành công");
        }

        public async Task<ResponeMessage> HienThiDanhSachBenhNhanAsync()
        {
            // Lấy danh sách người dùng
            var nguoiDungs = await _nguoiDungRepository.GetAllAsync();

            // Lọc danh sách bệnh nhân (VaiTroId = 3 là bệnh nhân)
            var benhNhans = nguoiDungs
                .Where(u => u.VaiTroId == 3)
                .Select(u => new BenhNhanDTO
                {
                    HoTen = u.HoTen,
                    GioiTinh = u.GioiTinh,
                    Tuoi = u.NgaySinh.HasValue ? TinhTuoi(u.NgaySinh.Value) : null,
                    SoDienThoai = u.SoDienThoai,
                    DiaChi = u.DiaChi
                })
                .ToList();

            if (benhNhans == null || !benhNhans.Any())
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không có bệnh nhân nào được tìm thấy.");
            }



            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(benhNhans);
            return new ResponeMessage(HttpStatusCode.Ok, responseJson);
        }

        // Hàm tính tuổi
        private int TinhTuoi(DateTime ngaySinh)
        {
            var today = DateTime.Today;
            var age = today.Year - ngaySinh.Year;
            if (ngaySinh.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
