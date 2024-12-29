using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone.NhanVien
{
    public class Respone_NhanVienDTO
    {
        public int Id { get; set; }
        public string? HoTen { get; set; }
        public string? GioiTinh { get; set; }
        public string? TenChuyenMon { get; set; }
        public string? SoDienThoai { get; set; }
    }
}
