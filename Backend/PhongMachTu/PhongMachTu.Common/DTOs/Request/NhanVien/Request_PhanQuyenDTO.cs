using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.NhanVien
{
    public class Request_PhanQuyenDTO
    {
        [Required(ErrorMessage = "Bạn chưa chọn nhân viên")]
        public int NhanVienId { get;set; }

        [Required(ErrorMessage="Có lỗi xảy ra")]
        public List<int> ?ChucNangIds { get; set; }
    }
}
