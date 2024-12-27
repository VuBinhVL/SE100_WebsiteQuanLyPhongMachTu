using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.PhieuNhapThuoc
{
    public class Request_AddPhieuNhapThuocDTO
    {
       
        [Required]
        public DateTime NgayNhap { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public int DonGia { get; set; }
        [Required]
        [MaxLength(300)]
        public string? TenThuoc { get; set; }
        [Required]
        public DateTime HanSuDung { get; set; }
        [MaxLength(500)]
        public string? Images { get; set; }
        [Required]
        public int LoaiThuocId { get; set; }
    }
}
