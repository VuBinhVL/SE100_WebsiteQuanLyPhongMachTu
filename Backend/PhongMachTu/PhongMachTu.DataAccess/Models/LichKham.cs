using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("LichKhams")]
	public class LichKham
	{
		[Key]
		public int Id {  get; set; }
		[Required]
		public int SoThuTu {  get; set; }

		[Required]
		public int TrangThaiLichKhamId {  get; set; }
		[Required]
		[ForeignKey(nameof(TrangThaiLichKhamId))]
		public TrangThaiLichKham? TrangThaiLichKham { get; set; }
	}
}
