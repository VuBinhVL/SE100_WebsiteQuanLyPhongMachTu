using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.CaKham
{
    public class Request_AddCaKhamDTO
    {
        [Required]
        [MaxLength(300)]
        public string TenCaKham { get; set; }

        [Required]
        public TimeSpan ThoiGianBatDau { get; set; }
        [Required]
        public TimeSpan ThoiGianKetThuc { get; set; }

        [Required]
        public DateTime NgayKham { get; set; }

        [Required]
        public int SoLuongBenhNhanToiDa { get; set; }
        [Required]
        public int? BacSiId { get; set; }
    }
}
