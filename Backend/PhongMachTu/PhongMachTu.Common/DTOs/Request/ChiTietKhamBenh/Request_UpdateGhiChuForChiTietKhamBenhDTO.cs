using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.ChiTietKhamBenh
{
    public class Request_UpdateGhiChuForChiTietKhamBenhDTO
    {
        public int ChiTietKhamBenhId { get;set; }
        [MaxLength(2000,ErrorMessage ="Ghi chú không quá 2000 kí tự")]
        public string? GhiChu { get; set; }
    }
}
