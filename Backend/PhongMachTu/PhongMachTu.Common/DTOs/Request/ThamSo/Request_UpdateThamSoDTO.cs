using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ThamSo
{
    public class Request_UpdateThamSoDTO
    {
       
        public int SoLanHuyLichKhamToiDaChoPhep { get; set; }

        public double HeSoBan { get; set; }
        public int SoPhutNgungDangKyTruocKetThuc { get; set; }
    }
}
