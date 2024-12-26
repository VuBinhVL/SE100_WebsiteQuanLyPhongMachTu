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
    }
}
