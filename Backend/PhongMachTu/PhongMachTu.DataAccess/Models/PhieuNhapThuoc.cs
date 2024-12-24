using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("PhieuNhapThuocs")]
	public class PhieuNhapThuoc
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime NgayNhap { get; set; }

		public IEnumerable<ChiTietPhieuNhapThuoc> ?ChiTietPhieuNhapThuocs { get; set; }
	}
}
