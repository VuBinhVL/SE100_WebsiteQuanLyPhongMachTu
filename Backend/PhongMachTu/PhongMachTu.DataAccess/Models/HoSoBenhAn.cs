using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{

	[Table("HoSoBenhAns")]
	public class HoSoBenhAn
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime NgayTao { get; set; }

		[Required]
		public int BenhNhanId {  get; set; }
		[Required]
		[ForeignKey(nameof(BenhNhanId))]
		public NguoiDung?BenhNhan { get; set; }

		[Required]
		public int NhomBenhId {  get; set; }
		[Required]
		[ForeignKey(nameof(NhomBenhId))]
		public NhomBenh ?NhomBenh { get; set; }

		public IEnumerable<ChiTietHoSoBenhAn>? ChiTietHoSoBenhAn {  get; set; }
	}
}
