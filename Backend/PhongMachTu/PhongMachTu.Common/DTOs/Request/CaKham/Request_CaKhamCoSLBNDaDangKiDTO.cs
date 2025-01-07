using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.CaKham
{
    public class Request_CaKhamCoSLBNDaDangKiDTO
    {
        public int Id { get; set; }
        public string SDT { get; set; }
        public string? TenCaKham { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public DateTime NgayKham { get; set; }
        public string? BacSiKham { get; set; }
        public string? TenNhomBenh { get; set; }
        public int SoLuongBenhNhanDaDangKi { get; set; }
    }
}
