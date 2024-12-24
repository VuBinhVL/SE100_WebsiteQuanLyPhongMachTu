using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("NguoiDungs")]
	public class NguoiDung
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(6)]
		[MaxLength(100)]
		public string? TenTaiKhoan {  get; set; }

		[Required]
		[MinLength(6)]
		[MaxLength(100)]
		public string? MatKhau { get; set; }

		[MaxLength(1000)]
		public string? Image {  get; set; }

		[Required]
		[MinLength(6)]
		[MaxLength(300)]
		public string? HoTen {  get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(300)]
		public string? Email {  get; set; }
		public string? DiaChi {  get; set; }
		public string? GioiTinh {  get; set; }

		[Required]
		[Phone]
		[MaxLength(30)]
		public string? SoDienThoai {  get; set; }
		public DateTime? NgaySinh { get; set; }

		[Required]
		public int VaiTroId {  get; set; }
		[ForeignKey(nameof(VaiTroId))]
		[Required]
		public VaiTro ?VaiTro { get; set; }

		public int ?ChuyenMonId { get; set; }//bệnh nhân thì null
		[ForeignKey(nameof(ChuyenMonId))]
		public NhomBenh ?ChuyenMon { get; set; }

		public IEnumerable<HoSoBenhAn> ?HoSoBenhAns { get; set; }
		public IEnumerable<SuChoPhep> ? SuChoPheps { get; set; }
	}
}
