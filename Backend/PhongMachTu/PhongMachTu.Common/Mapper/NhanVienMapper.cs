using PhongMachTu.Common.DTOs.Respone.NhanVien;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.Mapper
{
    public static class NhanVienMapper
    {
        public static Respone_NhanVienDTO Map_NguoiDungModel_To_Respone_NhanVienDTO(NguoiDung nguoiDung)
        {
            return new Respone_NhanVienDTO()
            {
                Id=nguoiDung.Id,
                HoTen=nguoiDung.HoTen,
                GioiTinh=nguoiDung.GioiTinh,
                TenChuyenMon=nguoiDung.ChuyenMon.TenNhomBenh,
                SoDienThoai=nguoiDung.SoDienThoai
            };
        }
    }
}
