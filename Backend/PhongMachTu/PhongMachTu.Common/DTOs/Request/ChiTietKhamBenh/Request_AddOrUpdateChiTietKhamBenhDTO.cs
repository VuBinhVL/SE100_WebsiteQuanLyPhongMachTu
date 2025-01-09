using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChiTietKhamBenh
{
    public class Request_AddOrUpdateChiTietKhamBenhDTO
    {
        public int Id { get;set; }//==-1 là insert

        [Required(ErrorMessage ="Chi tiết khám bệnh cần thuộc về 1 phiếu khám bệnh")]
        public int PhieuKhamBenhId { get; set; }

        [Required(ErrorMessage ="Bệnh lý không được để trống")]
        public int BenhLyId { get; set; }

        [Required(ErrorMessage ="Giá khám không được để trống")]
        public int GiaKham { get; set; }
    }
}
