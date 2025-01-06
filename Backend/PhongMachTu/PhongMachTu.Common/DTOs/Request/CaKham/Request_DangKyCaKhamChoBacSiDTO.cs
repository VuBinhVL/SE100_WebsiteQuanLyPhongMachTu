using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.CaKham
{
    public class Request_DangKyCaKhamChoBacSiDTO
    {
        [Required(ErrorMessage = "Bạn chưa chọn ca khám")]
        public int CaKhamId { get; set; }
    }
}
