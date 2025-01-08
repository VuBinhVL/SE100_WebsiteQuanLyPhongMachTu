using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.Thuoc
{
    public class Request_HienThiDanhSachThuocDTO
    {
        public int Id { get; set; }
        public string? TenThuoc { get; set; }
        public string? Images { get; set; }
        public int SoLuongTon { get; set; }
        public int GiaNhap { get; set; }
        public DateTime NgaySanXuat { get; set; }
        public DateTime HanSuDung { get; set; }
        public int LoaiThuocId { get; set; }
        public string TenLoaiThuoc { get; set; }
    }
}
