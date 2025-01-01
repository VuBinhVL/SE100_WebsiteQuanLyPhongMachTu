using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("PhieuKhamBenhs")]
	public class PhieuKhamBenh
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime NgayTao { get; set; }

		[Required]
		public int LichKhamId {  get; set; }
		[Required]
		[ForeignKey(nameof(LichKhamId))]
		public LichKham ?LichKham { get; set; }
		public IEnumerable<ChiTietKhamBenh> ?ChiTietKhamBenhs {  get; set; }
	}
}
