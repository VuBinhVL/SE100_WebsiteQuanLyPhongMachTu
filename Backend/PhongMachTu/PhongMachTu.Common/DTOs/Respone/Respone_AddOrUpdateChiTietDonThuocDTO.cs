using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Respone
{
    public class Respone_AddOrUpdateChiTietDonThuocDTO
    {
        public ResponeMessage ?ResponeMessage { get; set; }  
        public int ChiTietKhamBenhId { get; set; }
        public int ThuocId { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
    }
}
