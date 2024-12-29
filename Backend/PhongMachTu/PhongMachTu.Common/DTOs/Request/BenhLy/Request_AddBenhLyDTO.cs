using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.BenhLy
{
    public class Request_AddBenhLyDTO
    {
       [Required]
        public string? TenBenhLy { get; set; }
        [Required]
        public string? TrieuChung { get; set; }
        [Required]
        public int GiaThamKhao { get; set; }
        [Required]
        public string? Images { get; set; }
        [Required]
        public int NhomBenhId { get; set; }

    }
}
