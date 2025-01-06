﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.HoSoBenhAn
{
    public class Request_HienThiHoSoBenhAnDTO
    {
        public int IdBN { get; set; }
        public int Id { get; set; }
        public string NhomBenh { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
