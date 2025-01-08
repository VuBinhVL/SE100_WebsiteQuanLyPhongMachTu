using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone.ChiTietKhamBenh
{
    public class Respone_ChiTietKhamBenhDTO
    {
        public class Respone_ChupChieuDTO
        {
            public int Id { get; set; }
            public string? Images { get; set; }
            public string? KetLuan { get; set; }
            public int Gia { get; set; }
        }

        public class Respone_ChiTietXetNghiem
        {
            public int ChiTietKhamBenhId { get; set; }
            public int LoaiXetNghiemId { get; set; }
            public string? TenXetNghiem { get; set; }
            public string? TenDonViTinh { get; set; }
            public double KetQua { get; set; }
            public string? DanhGia { get; set; }
            public int GiaXetNghiem { get; set; }
        }

        public class Respone_ChiTietDonThuocDTO
        {
            public int ChiTietKhamBenhId { get; set; }
            public int ThuocId { get; set; }
            public string? TenThuoc { get; set; }
            public int SoLuong { get; set; }
            public int DonGia { get; set; }
        }


        public string? GhiChu { get; set; }
        public List<Respone_ChupChieuDTO> ?ChupChieus { get; set; }
        public List<Respone_ChiTietXetNghiem> ? ChiTietXetNghiems { get; set; }
        public List<Respone_ChiTietDonThuocDTO>? ChiTietDonThuocs { get; set; }

        public Respone_ChiTietKhamBenhDTO()
        {
            ChupChieus = new List<Respone_ChupChieuDTO>();
            ChiTietXetNghiems = new List<Respone_ChiTietXetNghiem>();
            ChiTietDonThuocs = new List<Respone_ChiTietDonThuocDTO>();
        }
    }
}
