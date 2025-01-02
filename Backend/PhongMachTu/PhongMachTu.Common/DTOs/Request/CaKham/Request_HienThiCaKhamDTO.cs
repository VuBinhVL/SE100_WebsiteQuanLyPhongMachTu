using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.CaKham
{
    public class Request_HienThiCaKhamDTO
    {
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public DateTime NgayKham { get; set; }
        public int SoLuongBenhNhanToiDa { get; set; }
        public int SoLuongBenhNhanDaDanKi { get; set; }
        public string TenBacSi { get; set; }
        public string TenChuyenMon { get; set; }
        public string? Image { get; set; }
        public int Id { get; set; }
    }
}
