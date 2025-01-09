using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChiTietXetNghiem
{
    public class Request_AddOrUpdateChiTietXetNghiemDTO
    {
        public int ChiTietKhamBenhId { get; set; }

        public int LoaiXetNghiemId { get; set; }

        [Required(ErrorMessage ="Kết quả không được để trống")]
        public double KetQua { get; set; }

        [MaxLength(1000,ErrorMessage ="Đánh giá không quá 1000 kí tự")]
        public string? DanhGia { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Giá xét nghiệm phải lớn hơn hoặc bằng 0.")]
        public int GiaXetNghiem { get; set; }
    }
}
