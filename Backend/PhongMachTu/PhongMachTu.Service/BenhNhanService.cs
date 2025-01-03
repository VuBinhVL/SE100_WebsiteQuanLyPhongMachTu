using Newtonsoft.Json;
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
        Task<IEnumerable<BenhNhanDTO>> HienThiDanhSachBenhNhanAsync();
        Task<ResponeMessage> AddBenhNhanAsync(Request_AddBenhNhanDTO data);
        Task<ResponeMessage> UpdateBenhNhanAsync(Request_UpdateThongTinCaNhanBenhNhanDTO data);
        Task<IEnumerable<NguoiDung>> GetAllAsync();
        Task<ResponeMessage> DeleteBenhNhanByIdAsync(int? id);
        Task<NguoiDung> GetBenhNhanByIdAsync(int? id);
    }

    public class BenhNhanService : IBenhNhanService
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHoSoBenhAnRepository _hoSoBenhAnRepository;
        private readonly ILichKhamRepository _lichKhamRepository;
        public BenhNhanService(INguoiDungRepository nguoiDungRepository, IUnitOfWork unitOfWork, IHoSoBenhAnRepository hoSoBenhAnRepository, ILichKhamRepository lichKhamRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _unitOfWork = unitOfWork;
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _lichKhamRepository = lichKhamRepository;
        }

        public async Task<ResponeMessage> RegisterAsync(Request_RegisterDTO data)
        {

            if (!Validation.IsAlphanumeric(data.TenTaiKhoan))
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

        public async Task<IEnumerable<BenhNhanDTO>> HienThiDanhSachBenhNhanAsync()
        {
            // Lấy danh sách người dùng
            var nguoiDungs = await _nguoiDungRepository.GetAllAsync();

            // Lọc danh sách bệnh nhân (VaiTroId = 3 là bệnh nhân)
            var benhNhans = nguoiDungs
                .Where(u => u.VaiTroId == 3)
                .Select(u => new BenhNhanDTO
                {
                    Email = u?.Email,
                    id= u.Id,
                    HoTen = u.HoTen,
                    GioiTinh = u.GioiTinh,
                    Tuoi = u.NgaySinh.HasValue ? TinhTuoi(u.NgaySinh.Value) : null,
                    SoDienThoai = u.SoDienThoai,
                    DiaChi = u.DiaChi
                })
                .ToList();




            
            return benhNhans;
        }

        // Hàm tính tuổi
        private int TinhTuoi(DateTime ngaySinh)
        {
            var today = DateTime.Today;
            var age = today.Year - ngaySinh.Year;
            if (ngaySinh.Date > today.AddYears(-age)) age--;
            return age;
        }

        public async Task<IEnumerable<NguoiDung>> GetAllAsync()
        {
            return (await _nguoiDungRepository.GetAllAsync()).Where(u => u.VaiTroId == 3);
        }
        public async Task<ResponeMessage> DeleteBenhNhanByIdAsync(int? id)
        {
            NguoiDung findBenhNhan = null;
            if (id == null || (findBenhNhan = await _nguoiDungRepository.GetSingleByIdAsync(id ?? -1)) == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy nhân viên");
            }
         
            var findHoSoBenhAn = await _hoSoBenhAnRepository.FindAsync(h => h.BenhNhanId == id);
            if (findHoSoBenhAn.Any())
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Bệnh nhân đã có hồ sơ bệnh án, không thể xóa");
            }
            var findLichKham = await _lichKhamRepository.FindAsync(h => h.BenhNhanId == id);
            if (findLichKham.Any())
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Bệnh nhân đã có lịch khám, không thể xóa");
            }
            _nguoiDungRepository.Delete(findBenhNhan);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa bệnh nhân thành công");
        }
        public async Task<NguoiDung> GetBenhNhanByIdAsync(int? id)
        {
            return (await _nguoiDungRepository.GetSingleWithIncludesAsync(u => u.Id == id));
        }

        public async Task<ResponeMessage> AddBenhNhanAsync(Request_AddBenhNhanDTO data)
        {
            // Kiểm tra ngày sinh không được lớn hơn thời điểm hiện tại
            if (data.NgaySinh != null && data.NgaySinh > DateTime.Now)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Ngày sinh không được lớn hơn thời điểm hiện tại");
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
            NguoiDung benhNhan = new NguoiDung()
            {
                //dữ liệu từ request
                HoTen = data.HoTen,
                GioiTinh = data.GioiTinh,
                NgaySinh = data.NgaySinh,
                ChuyenMonId = null,
                SoDienThoai = data.SoDienThoai,
                Email = data.Email,
                DiaChi = data.DiaChi,
                Image = data.Image == null ? "no_img.png" : data.Image,
                VaiTroId = 3,
                //dữ liệu generate 
                TenTaiKhoan = JsonConvert.SerializeObject(new string[] { data.SoDienThoai, data.Email }).ToString(),
                MatKhau = EncryptionHelper.Encrypt("123456"),
                IsLock = false
            };

            await _nguoiDungRepository.AddAsync(benhNhan);

            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Thêm bệnh nhân thành công");
        }

        public async Task<ResponeMessage> UpdateBenhNhanAsync(Request_UpdateThongTinCaNhanBenhNhanDTO data)
        {
            // Kiểm tra ngày sinh không được lớn hơn thời điểm hiện tại
            if (data.NgaySinh != null && data.NgaySinh > DateTime.Now)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Ngày sinh không được lớn hơn thời điểm hiện tại");
            }
            var benhNhan = await _nguoiDungRepository.GetSingleByIdAsync(data.Id);
            if (benhNhan == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy bệnh nhân");
            }
            // Kiểm tra email không trùng lặp với người khác
            var checkEmail = (await _nguoiDungRepository
                .FindAsync(u => u.Email == data.Email && u.Id != data.Id))
                .FirstOrDefault();
            if (checkEmail != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Email đã có người sử dụng");
            }

            // Kiểm tra số điện thoại không trùng lặp với người khác
            var checkSDT = (await _nguoiDungRepository
                .FindAsync(u => u.SoDienThoai == data.SoDienThoai && u.Id != data.Id))
                .FirstOrDefault();
            if (checkSDT != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Số điện thoại đã có người sử dụng");
            }
            benhNhan.HoTen = data.HoTen;
            benhNhan.GioiTinh = data.GioiTinh;
            benhNhan.NgaySinh = data.NgaySinh;
            benhNhan.SoDienThoai = data.SoDienThoai;
            benhNhan.Email = data.Email;
            benhNhan.DiaChi = data.DiaChi;
            benhNhan.Image = data.Image == null ? "no_img.png" : data.Image;
            if (data.Image != null)
            {
                benhNhan.Image = data.Image;
            }
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa thông tin bệnh nhân thành công");
        }
    }
}
