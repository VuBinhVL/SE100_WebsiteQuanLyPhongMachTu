using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone.CaKham
{
    public class Respone_CaKhamMySelfDTO
    {
        public int Id { get; set; }
        public string? TenCaKham { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public DateTime NgayKham { get; set; }
    }
}
