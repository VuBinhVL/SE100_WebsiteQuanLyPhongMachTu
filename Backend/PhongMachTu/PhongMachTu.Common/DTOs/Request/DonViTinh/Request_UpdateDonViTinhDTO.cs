﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.DTOs.Request.DonViTinh
{
	public class Request_UpdateDonViTinhDTO
	{
		public int? Id { get; set; }

		public string? TenDonViTinh { get; set; }
	}
}
