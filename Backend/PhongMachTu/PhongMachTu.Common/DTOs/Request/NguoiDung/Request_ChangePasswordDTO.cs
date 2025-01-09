using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.NguoiDung
{
    public class Request_ChangePasswordDTO
    {
        [Required(ErrorMessage ="Mật khẩu hiện tại không được để trống")]
        public string? MatKhauHienTai { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        [MinLength(6,ErrorMessage = "Mật khẩu phải ít nhất 6 kí tự")]
        [MaxLength(100, ErrorMessage = "Mật khẩu không quá 100 kí tự")]
        public string? MatKhauMoi { get; set; }

        [Required(ErrorMessage = "Mật khẩu nhập lại không được để trống")]
        public string? MatKhauNhapLai { get; set; }
    }
}
