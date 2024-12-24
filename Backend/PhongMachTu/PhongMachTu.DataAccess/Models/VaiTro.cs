using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{

	[Table("VaiTros")]
	public class VaiTro
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(300)]
		public string? TenVaiTro { get; set; }

		[Required]
		[MaxLength(1000)]
		public string? UrlsDefault {  get; set; }
	}
}
