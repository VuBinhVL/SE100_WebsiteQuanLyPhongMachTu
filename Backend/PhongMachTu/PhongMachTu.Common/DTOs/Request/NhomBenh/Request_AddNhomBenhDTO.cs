using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.NhomBenh
{
    public class Request_AddNhomBenhDTO
    {
        [Required]
        public string? TenNhomBenh { get; set; }
    }
}
