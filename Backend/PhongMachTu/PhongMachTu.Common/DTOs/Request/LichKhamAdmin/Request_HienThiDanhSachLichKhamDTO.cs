using PhongMachTu.Common.DTOs.Request.CaKham;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.LichKhamAdmin
{
    public class Request_HienThiDanhSachLichKhamDTO
    {
        public List<LichKhamDTO> LichKhamList { get; set; } = new List<LichKhamDTO>();
    }
    public class LichKhamDTO
    {
        public int Id { get; set; }
        public int STT { get; set; }
        public string? TenBenhNhan { get; set; }
        public string? TenTrangThai { get; set; }
    }
}
