using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.NhanVien
{
    public class Request_HienThiThongTinCaNhanBenAdminDTO
    {
       
        public string? HoTen { get; set; }
        public string? Image { get; set; }
        public string? VaiTro { get; set; }
        public string? ChuyenMon { get; set; }
        public string? SoDienThoai { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
       
    }
}
