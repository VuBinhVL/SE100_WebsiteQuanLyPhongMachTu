using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChiTietHoSoBenhAn
{
    public class Request_HienThiCTKBCuaChiTietHoSoBenhAnDTO
    {
        public string TenBenhNhan { get; set; }
        public string TenBacSi { get; set; }
        public string TenBenhLy { get; set; }
        public DateTime NgayTaoPKB { get; set; }
        public string ChanDoan { get; set; }

        // Danh sách thuốc
        public List<ThuocDTO> DanhSachThuoc { get; set; }
        public int TongTienThuoc
        {
            get
            {
                return DanhSachThuoc.Sum(x => x.ThanhTien);
            }
        }
        public List<XetNghiemDTO> DanhSachXetNghiem { get; set; }
        public int TongTienXetNghiem
        {
            get
            {
                return DanhSachXetNghiem.Sum(x => x.GiaXetNghiem);
            }
        }
        public int TongTien
        {
            get
            {
                return TongTienThuoc + TongTienXetNghiem;
            }
        }
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

    // Đối tượng chứa thông tin chi tiết về xét nghiệm
    public class XetNghiemDTO
    {
        public string TenXetNghiem { get; set; }
        public string DVT { get; set; }
        public double KetQua { get; set; }
        public string DanhGia { get; set; }
        public int GiaXetNghiem { get; set; }
    }
}
