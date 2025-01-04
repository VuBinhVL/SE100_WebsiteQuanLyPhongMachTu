using PhongMachTu.Common.DTOs.Request.ChiTietHoSoBenhAn;
using PhongMachTu.Common.DTOs.Request.NhanVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.BenhLy
{
    public class Request_HienThiChiTietBenhLyKhamDTO
    {
        public string TenBenhNhan { get; set; }
        public string TenBenhLy { get; set; }
        public string TenBacSi { get; set; }
        public DateTime NgayKham { get; set; }
        public string GhiChu { get; set; }

        public List<ChiTietDonThuocDTO> ChiTietDonThuoc { get; set; } = new List<ChiTietDonThuocDTO>();
        public int TongTienDonThuoc { get; set; }
        public List<XetNghiemDTO> KetQuaXetNghiem { get; set; } = new List<XetNghiemDTO>();
        public int TongTienXetNghiem { get; set; }
        public int TongTienTatCa { get; set; }
    }

    public class ChiTietDonThuocDTO
    {
        public string TenThuoc { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public int ThanhTien => DonGia * SoLuong;
    }
    public class XetNghiemDTO {
        public string LoaiXetNghiem { get; set; }
        public string DonViTinh { get; set; }
        public string KetQua { get; set; }
        public string DanhGia { get; set; }
        public int DonGia { get; set; }
    }
}
