using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("Thuocs")]
	public class Thuoc
	{
		[Key]
		public int Id {  get; set; }
		[Required]
		[MaxLength(300)]
		public string? TenThuoc {  get; set; }
		[MaxLength(500)]
		public string? Images {  get; set; }
		[Required]
		public int SoLuongTon {  get; set; }
		[Required]
		public int GiaNhap {  get; set; }
        [Required]
        public DateTime NgaySanXuat { get; set; }
        [Required]
		public DateTime HanSuDung { get; set; }
        [Required]
		public int LoaiThuocId {  get; set; }
		[Required]
		[ForeignKey(nameof(LoaiThuocId))]
		public LoaiThuoc ?LoaiThuoc { get; set; }
	}
}
