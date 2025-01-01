using PhongMachTu.Common.DTOs.Request.LichKhamAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.PhieuKhamBenh
{
    public class Request_HienThiDanhSachPhieuKhamBenhDTO
    {
        public List<PhieuKhamBenhDTO> PhieuKhamBenhList { get; set; } = new List<PhieuKhamBenhDTO>();
    }
    public class PhieuKhamBenhDTO
    {
       
        public string? TenBenhNhan { get; set; }
        public string? TenBacSi { get; set; }
        public DateTime? NgayTao { get; set; }
        public int SoDienThoai { get; set; }
    }
}
