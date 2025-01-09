using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone.PhieuKhamBenh
{

    public class Respone_PhieukhamBenhDTO
    {

        public class Respone_ChiTietKhamBenhDTO
        {
            public int Id { get; set; }

            public string? TenBenhLy { get; set; }

            public int GiaKham { get; set; }

        }

        public class Respone_ChiTietDonThuocDTO
        {
            public int ChiTietKhamBenhId { get; set; }
            public int ThuocId { get; set; }
            public string? TenThuoc { get; set; }
            public int SoLuong { get; set; }
            public int DonGia { get; set; }
        }


        public string? HinhAnhBenhNhan { get; set; }
        public string? HoTenBenhNhan { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public DateTime NgayTao { get; set; }
        public string? TenBacSiKham { get; set; }
        public List<Respone_ChiTietKhamBenhDTO> ?ChiTietKhamBenhs { get; set; }
        public List<Respone_ChiTietDonThuocDTO>? ChiTietDonThuocs { get; set; }

        public Respone_PhieukhamBenhDTO()
        {
            ChiTietKhamBenhs = new List<Respone_ChiTietKhamBenhDTO>();
            ChiTietDonThuocs = new List<Respone_ChiTietDonThuocDTO> ();
        }

    }
}
