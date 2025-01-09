using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone.PhieuKhamBenh
{
    public class Respone_PhieukhamBenhDTO
    {
        public int Id { get; set; }
        public string ?TenBenhNhan { get; set; }
        public string ?TenBacSi { get; set; }
        public DateTime NgayTao { get; set; }
        public string? SoDienThoai { get; set; }
        public string? TenTrangThaiLichKham { get; set; }
    }
}
