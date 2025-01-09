using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone.ChiTietXetNghiem
{
    public class Respone_AddOrUpdateChiTietXetNghiemDTO
    {
        public ResponeMessage? ResponeMessage { get; set; }
        public int ChiTietKhamBenhId { get; set; }
        public int LoaiXetNghiemId { get; set; }
        public int DonViTinhId { get; set; }
        public double KetQua { get; set; }
        public string? DanhGia { get; set; }
        public int GiaXetNghiem { get; set; }
    }
}
