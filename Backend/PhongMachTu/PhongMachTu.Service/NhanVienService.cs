using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.NhanVien;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhongMachTu.Common.Security;
using PhongMachTu.DataAccess.Infrastructure;
namespace PhongMachTu.Service
{
    public interface INhanVienService
    {
       Task<ResponeMessage> AddNhanVienAsync(Request_AddNhanVienDTO data);
       Task<ResponeMessage> UpdateThongTinCaNhanNhanVienAsync(Request_UpdateThongTinCaNhanNhanVienDTO data);
    }


    public class NhanVienService : INhanVienService
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NhanVienService(INguoiDungRepository nguoiDungRepository,IUnitOfWork unitOfWork)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddNhanVienAsync(Request_AddNhanVienDTO data)
        {

            var checkEmail = (await _nguoiDungRepository.FindAsync(u=>u.Email==data.Email)).FirstOrDefault();
            if(checkEmail != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest,"Email đã có người sử dụng");
            }

            var checkSDT = (await _nguoiDungRepository.FindAsync(u => u.SoDienThoai == data.SoDienThoai)).FirstOrDefault();
            if (checkSDT != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Số điện thoại đã có người sử dụng");
            }

            NguoiDung nhanVien = new NguoiDung()
            {
                //dữ liệu từ request
                HoTen = data.HoTen,
                GioiTinh = data.GioiTinh,
                NgaySinh = data.NgaySinh,
                ChuyenMonId = data.ChuyenMonId,
                SoDienThoai = data.SoDienThoai,
                Email = data.Email,
                DiaChi = data.DiaChi,
                Image = data.Image==null? "no_img.png":data.Image,

                //dữ liệu generate
                TenTaiKhoan = JsonConvert.SerializeObject(new string[] { data.SoDienThoai, data.Email }).ToString(),
                MatKhau = EncryptionHelper.Encrypt("123456"),
                VaiTroId = 2
            };

            await _nguoiDungRepository.AddAsync(nhanVien);

            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Thêm nhân viên thành công");
        }

        public async Task<ResponeMessage> UpdateThongTinCaNhanNhanVienAsync(Request_UpdateThongTinCaNhanNhanVienDTO data)
        {

            var findNhanVien = await _nguoiDungRepository.GetSingleByIdAsync(data.Id);
            if(findNhanVien == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy nhân viên");
            }

            var checkEmail = (await _nguoiDungRepository.FindAsync(u => u.Email == data.Email && u.Id!=data.Id)).FirstOrDefault();
            if (checkEmail != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Email đã có người sử dụng");
            }

            var checkSDT = (await _nguoiDungRepository.FindAsync(u => u.SoDienThoai == data.SoDienThoai && u.Id != data.Id)).FirstOrDefault();
            if (checkSDT != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Số điện thoại đã có người sử dụng");
            }

            //dữ liệu từ request
            findNhanVien.HoTen = data.HoTen;
            findNhanVien.GioiTinh = data.GioiTinh;
            findNhanVien.NgaySinh = data.NgaySinh;
            findNhanVien.ChuyenMonId = data.ChuyenMonId;
            findNhanVien.SoDienThoai = data.SoDienThoai;
            findNhanVien.Email = data.Email;
            findNhanVien.DiaChi = data.DiaChi;
            if (data.Image != null)
            {
                findNhanVien.Image=data.Image;
            }
            findNhanVien.TenTaiKhoan = JsonConvert.SerializeObject(new string[] { data.SoDienThoai, data.Email }).ToString();

            _nguoiDungRepository.Update(findNhanVien);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa thông tin nhân viên thành công");
        }
    }
}
