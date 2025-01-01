using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChiTietHoSoBenhAn
{
    public class Request_HienThiPKBCuaChiTietHoSoBenhAnDTO
    {
        public string TenBenhNhan { get; set; }
        public string GioiTinhBN { get; set; }
        public DateTime? NgaySinhBN { get; set; }
        public int GiaKham { get; set; }
        public List<string> BenhLys { get; set; }
        public string GhiChu { get; set; }

        public string TenBacSi { get; set; }
        public DateTime NgayTaoPKB { get; set; }
        public string TenNhomBenh { get; set; }

        // Danh sách thuốc
        public List<ThuocDTO> DanhSachThuoc { get; set; }
    }

    // Đối tượng chứa thông tin chi tiết về thuốc
    public class ThuocDTO
    {
        public string TenThuoc { get; set; }
        public int SoLuongThuoc { get; set; }
        public int DonGiaThuoc { get; set; }
        public int ThanhTien
        {
            get
            {
                return SoLuongThuoc * DonGiaThuoc;
            }
        }
    }

}
