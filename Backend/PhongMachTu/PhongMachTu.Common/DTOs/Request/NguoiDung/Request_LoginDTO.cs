using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.NguoiDung
{
    public class Request_LoginDTO
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string? TenTaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string? MatKhau { get; set; }
    }
}
