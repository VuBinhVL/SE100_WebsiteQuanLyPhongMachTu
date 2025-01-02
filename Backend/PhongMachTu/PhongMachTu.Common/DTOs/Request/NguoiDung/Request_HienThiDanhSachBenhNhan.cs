using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.NguoiDung
{
    public class Request_HienThiDanhSachBenhNhan
    {
        public List<BenhNhanDTO> BenhNhanList { get; set; } = new List<BenhNhanDTO>();

    }
    public class BenhNhanDTO
    {
       public int id { get; set; }
        public string? HoTen { get; set; }
        public string? GioiTinh { get; set; }
        public int? Tuoi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
    }   
}
