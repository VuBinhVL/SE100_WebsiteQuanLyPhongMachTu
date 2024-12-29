using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.PhieuNhapThuoc
{
    public class Request_UpdatePhieuNhapThuocDTO
    {
        public int? Id { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
