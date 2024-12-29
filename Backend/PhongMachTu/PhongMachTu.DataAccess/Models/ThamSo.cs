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
        [Required]
        public int SoLanHuyLichKhamToiDaChoPhep { get; set; }

        [Required]
        public double HeSoBan {  get; set; }
    }
}
