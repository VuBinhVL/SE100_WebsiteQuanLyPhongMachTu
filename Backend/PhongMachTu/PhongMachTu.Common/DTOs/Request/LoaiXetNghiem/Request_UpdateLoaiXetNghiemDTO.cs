using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.LoaiXetNghiem
{
    public class Request_UpdateLoaiXetNghiemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên xét nghiệm không được để trống")]
        [MaxLength(300, ErrorMessage = "Tối đa 300 kí tự")]
        public string? TenXetNghiem { get; set; }

        [Required(ErrorMessage = "Giá tham khảo không được để trống")]
        public int GiaThamKhao { get; set; }

        [Required(ErrorMessage = "Đơn vị tính không được để trống")]
        public int DonViTinhId { get; set; }
    }
}
