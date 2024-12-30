using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone.NguoiDung
{
    public class Respone_Login
    {
        public int HttpStatusCode { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}
