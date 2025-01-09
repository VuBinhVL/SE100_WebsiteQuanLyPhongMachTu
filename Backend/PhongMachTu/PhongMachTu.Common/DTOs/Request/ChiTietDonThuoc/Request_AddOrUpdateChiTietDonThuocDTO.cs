using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChiTietDonThuoc
{
    public class Request_AddOrUpdateChiTietDonThuocDTO
    {
        public int ChiTietKhamBenhId { get; set; }
        public int ThuocId { get; set; }

        [Required(ErrorMessage ="Số lượng không được để trống")]
        public int SoLuong { get; set; }
    }
}
