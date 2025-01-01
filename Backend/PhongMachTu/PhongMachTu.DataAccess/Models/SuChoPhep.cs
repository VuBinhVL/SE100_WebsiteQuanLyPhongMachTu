using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("SuChoPheps")]
	public class SuChoPhep
	{
		[Required]
		public int ChucNangId {  get; set; }
		[Required]
		[ForeignKey(nameof(ChucNangId))]
		public ChucNang ?ChucNang { get; set; }

		[Required]
		public int NguoiDungId { get; set; }
		[Required]
		[ForeignKey(nameof(NguoiDungId))]
		public NguoiDung? NguoiDung { get; set; }
	}
}
