using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{


    [Table("ThamSos")]
    public class ThamSo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SoLanHuyLichKhamToiDaChoPhep { get; set; }

        [Required]
        public double HeSoBan {  get; set; }

        [Required]
        public int SoPhutNgungDangKyTruocKetThuc { get; set; }//ví dụ = 15, thời gian kết thúc ca khám là 9h thì từ 8h45 đổ lên k cho đăng ký nữa
    }
}
