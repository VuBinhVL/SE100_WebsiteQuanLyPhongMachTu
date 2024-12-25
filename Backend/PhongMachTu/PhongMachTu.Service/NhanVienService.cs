﻿using PhongMachTu.Common.ConstValue;
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
using PhongMachTu.Common.DTOs.Respone.NhanVien;
using PhongMachTu.Common.Mapper;
using System.Security.Cryptography.X509Certificates;
namespace PhongMachTu.Service
{
    public interface INhanVienService
    {
        Task<ResponeMessage> AddNhanVienAsync(Request_AddNhanVienDTO data);
        Task<ResponeMessage> UpdateThongTinCaNhanNhanVienAsync(Request_UpdateThongTinCaNhanNhanVienDTO data);
        Task<List<Respone_NhanVienDTO>> GetAllAsync();
        Task<ResponeMessage> DeleteNhanVienByIdAsync(int? id);

    }

    public class NhanVienService : INhanVienService
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly ICaKhamRepository _caKhamRepository;
        private readonly ISuChoPhepRepository _suChoPhepRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NhanVienService(INguoiDungRepository nguoiDungRepository,ICaKhamRepository caKhamRepository,ISuChoPhepRepository suChoPhepRepository ,IUnitOfWork unitOfWork)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _caKhamRepository = caKhamRepository;
            _suChoPhepRepository = suChoPhepRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddNhanVienAsync(Request_AddNhanVienDTO data)
        {

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
                Image = data.Image == null ? "no_img.png" : data.Image,

                //dữ liệu generate
                TenTaiKhoan = JsonConvert.SerializeObject(new string[] { data.SoDienThoai, data.Email }).ToString(),
                MatKhau = EncryptionHelper.Encrypt("123456"),
                VaiTroId = 2
            };

            await _nguoiDungRepository.AddAsync(nhanVien);

            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Thêm nhân viên thành công");
        }

        public async Task<ResponeMessage> DeleteNhanVienByIdAsync(int? id)
        {
            NguoiDung findNhanVien = null;
            if (id == null || (findNhanVien = await _nguoiDungRepository.GetSingleByIdAsync(id ?? -1)) == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy nhân viên");
            }

            //xem xét ràng buộc khóa ngoại
            var findCakham = await _caKhamRepository.FindAsync(c=>c.BacSiId==id);
            if (findCakham.Any())
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không thể xóa nhân viên này do trước đó đã từng khám bệnh");
            }

            //xóa sự cho phép
            var findSuChoPheps = await _suChoPhepRepository.FindAsync(s=>s.NguoiDungId==id);
            foreach( var s in findSuChoPheps)
            {
                _suChoPhepRepository.Delete(s);
            }

            _nguoiDungRepository.Delete(findNhanVien);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa nhân viên thành công");

        }

        public async Task<List<Respone_NhanVienDTO>> GetAllAsync()
        {
            List<Respone_NhanVienDTO> list = new List<Respone_NhanVienDTO>();
            var rs = await _nguoiDungRepository.GetAllWithIncludeAsync(u => u.ChuyenMon);
            foreach (var r in rs)
            {
                list.Add(NhanVienMapper.Map_NguoiDungModel_To_Respone_NhanVienDTO(r));
            }
            return list;
        }

        public async Task<ResponeMessage> UpdateThongTinCaNhanNhanVienAsync(Request_UpdateThongTinCaNhanNhanVienDTO data)
        {

            var findNhanVien = await _nguoiDungRepository.GetSingleByIdAsync(data.Id);
            if (findNhanVien == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy nhân viên");
            }

            var checkEmail = (await _nguoiDungRepository.FindAsync(u => u.Email == data.Email && u.Id != data.Id)).FirstOrDefault();
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
                findNhanVien.Image = data.Image;
            }
            findNhanVien.TenTaiKhoan = JsonConvert.SerializeObject(new string[] { data.SoDienThoai, data.Email }).ToString();

            _nguoiDungRepository.Update(findNhanVien);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa thông tin nhân viên thành công");
        }
    }
}
