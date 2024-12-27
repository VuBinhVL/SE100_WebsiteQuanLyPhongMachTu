using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.BenhNhan
{
    public class Request_RegisterDTO
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [MinLength(6, ErrorMessage = "Họ tên ít nhất 6 kí tự")]
        [MaxLength(300, ErrorMessage = "Họ tên không được vượt quá 300 kí tự")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(30, ErrorMessage = "Số điện thoại không được vượt quá 30 kí tự")]
        public string? SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(300, ErrorMessage = "Email không được vượt quá 300 kí tự")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Tên tài khoản không được để trống")]
        [MinLength(6,ErrorMessage ="Tên tài khoản ít nhất 6 kí tự")]
        [MaxLength(100,ErrorMessage ="Tên tài khoản không được vượt quá 100 kí tự")]
        public string? TenTaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu ít nhất 6 kí tự")]
        [MaxLength(100, ErrorMessage = "Mật khẩu không được vượt quá 100 kí tự")]
        public string? MatKhau { get; set; }
    }
}
