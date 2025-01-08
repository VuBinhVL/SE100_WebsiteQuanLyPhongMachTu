using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.LichKhamAdmin
{
    public class Request_HienThiChiTietLichKhamDTO
    {
        public string TenBenhNhan { get; set; }
        public string GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayKham { get; set; }
        public string TenCaKham { get; set; }
        public int STT { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public string TenNhomBenh { get; set; }
        public string TrangThai { get; set; }
        public string TenBacSi { get; set; }
        public string TenChuyenMon { get; set; }
        public DateTime? NgaySinhBN  { get; set; }
    }
}
