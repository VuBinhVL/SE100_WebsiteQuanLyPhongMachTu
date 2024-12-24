using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("LoaiThuocs")]
	public class LoaiThuoc
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(300)]
		public string? TenLoaiThuoc { get; set; }
	}
}
