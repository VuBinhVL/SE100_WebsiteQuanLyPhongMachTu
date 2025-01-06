using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChiTietHoSoBenhAn
{
    public class Request_HienThiChiTietHoSoBenhAnDTO
    {
        
        public string HoTenBacSi { get; set; }
        public string TenBenhLy { get; set; }
        public DateTime NgayKham { get; set; }
        public int TongTien { get; set; }
    }   
}
