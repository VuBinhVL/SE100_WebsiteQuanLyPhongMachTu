using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.BenhLy
{
    public class Request_HienThiChiTietBenhLyDTO
    {
        public string? TenBenhLy { get; set; }
        public string? Images { get; set; }
        public int GiaThamKhao { get; set; }
        public string? TrieuTrung { get; set; }
        public bool IsHaveAppointment { get; set; }
    }
}
