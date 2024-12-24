using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("ChiTietKhamBenhs")]
	public class ChiTietKhamBenh
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int PhieuKhamBenhId {  get; set; }
		[Required]
		[ForeignKey(nameof(PhieuKhamBenhId))]
		public PhieuKhamBenh? PhieuKhamBenh { get; set; }

		[Required]
		public int BenhLyId { get; set; }
		[Required]
		[ForeignKey(nameof(BenhLyId))]
		public BenhLy? BenhLy { get; set; }

		[Required]
		public int GiaKham {  get; set; }

		[MaxLength(2000)]
		public string? GhiChu {  get; set; }
		public IEnumerable<ChiTietDonThuoc> ?ChiTietDonThuocs {  get; set; }
		public IEnumerable<ChupChieu> ? ChupChieus {  get; set; }


	}
}
