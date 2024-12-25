using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.BenhLy
{
    public class Request_UpdateBenhLyDTO
    {
        public int? Id { get; set; }
        public string? TenBenhLy { get; set; }

        public string? TrieuChung { get; set; }

        public int? GiaThamKhao { get; set; }
    
        public string? Images { get; set; }
    }
}
