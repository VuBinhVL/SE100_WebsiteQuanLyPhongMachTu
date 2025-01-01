using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	public class ChiTietHoSoBenhAn
	{
		[Required]
		public int HoSoBenhAnId {  get; set; }
		[Required]
		[ForeignKey(nameof(HoSoBenhAnId))]
		public HoSoBenhAn ?HoSoBenhAn { get; set; }

		[Required]
		public int ChiTietKhamBenhId {  get; set; }
		[Required]
		[ForeignKey(nameof(ChiTietKhamBenhId))]
		public ChiTietKhamBenh ? ChiTietKhamBenh { get; set; }
	}
}
