using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChupChieu
{
    public class Request_UpdateChupChieuDTO
    {
        public int Id { get;set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "Kết luận không được để trống")]
        [MaxLength(1500,ErrorMessage ="Kết luận không được quá 1500 kí tự")]
        public string? KetLuan { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        public int Gia { get; set; }
    }
}
