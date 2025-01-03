using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Models
{
	[Table("CaKhams")]
	public class CaKham
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(300)]
		public string? TenCaKham { get; set; }

		[Required]
		public TimeSpan ThoiGianBatDau { get; set; }
		[Required]
		public TimeSpan ThoiGianKetThuc { get; set; }

		[Required]
		public DateTime NgayKham { get; set; }

		[Required]
		public int SoLuongBenhNhanToiDa { get; set; }

		public int? BacSiId { get; set; }//null là chưa có ai đăng ký
		[ForeignKey(nameof(BacSiId))]
		public NguoiDung? BacSi { get; set; }
		public IEnumerable<LichKham> ?LichKhams { get; set; }

        [Required]
        public int NhomBenhId { get; set; }
        [Required]
        [ForeignKey(nameof(NhomBenhId))]
        public NhomBenh? NhomBenh { get; set; }

    }
}
