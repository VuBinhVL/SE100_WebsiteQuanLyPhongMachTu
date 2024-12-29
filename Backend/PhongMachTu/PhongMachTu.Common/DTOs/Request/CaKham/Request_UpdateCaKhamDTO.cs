using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.CaKham
{
    public class Request_UpdateCaKhamDTO
    {
        public int? Id { get; set; }
        public string TenCaKham { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
      
        public TimeSpan ThoiGianKetThuc { get; set; }

  
        public DateTime NgayKham { get; set; }

    
        public int SoLuongBenhNhanToiDa { get; set; }
  
        public int? BacSiId { get; set; }
    }
}
