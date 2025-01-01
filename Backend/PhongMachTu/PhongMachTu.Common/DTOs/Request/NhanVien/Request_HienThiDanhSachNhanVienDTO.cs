using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.NhanVien
{
    public class Request_HienThiDanhSachNhanVienDTO
    {
        public List<NhanVienDTO> NhanVienList { get; set; } = new List<NhanVienDTO>();
    
        
    }
    public class NhanVienDTO
    {
        public int Id { get; set; }
        public string? HoTen { get; set; }
        public string? GioiTinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? ChuyenMon { get; set; }
    }
}
