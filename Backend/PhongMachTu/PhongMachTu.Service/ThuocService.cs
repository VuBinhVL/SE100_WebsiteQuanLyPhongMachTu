using PhongMachTu.Common.DTOs.Request.Thuoc;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.DataAccess.Infrastructure;

namespace PhongMachTu.Service
{
	public interface IThuocService
	{
		Task<List<Thuoc>> GetAllAsync();

		Task<Request_HienThiChiTietThuocDTO> GetByIdAsync(int id);

		Task<ResponeMessage> UpdateThuoc(Request_UpdateThuocDTO? request);

		Task<ResponeMessage> DeleteThuoc(int id);

		Task<IEnumerable<Request_HienThiDanhSachThuocDTO>> HienThiDanhSachThuoc();
	}

	public class ThuocService : IThuocService
	{
		private readonly IThuocRepository _thuocRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILoaiThuocRepository _loaiThuocRepository;
		private readonly IChiTietPhieuNhapThuocRepository _chiTietPhieuNhapThuocRepository;
		private readonly IChiTietDonThuocRepository _chiTietDonThuocRepository;

		public ThuocService(IThuocRepository thuocRepository, IUnitOfWork unitOfWork, ILoaiThuocRepository loaiThuocRepository,
			IChiTietPhieuNhapThuocRepository chiTietPhieuNhapThuocRepository, IChiTietDonThuocRepository chiTietDonThuocRepository)
		{
			_thuocRepository = thuocRepository;
			_unitOfWork = unitOfWork;
			_loaiThuocRepository = loaiThuocRepository;
			_chiTietPhieuNhapThuocRepository = chiTietPhieuNhapThuocRepository;
			_chiTietDonThuocRepository = chiTietDonThuocRepository;
		}

		public async Task<List<Thuoc>> GetAllAsync()
		{
			return (await _thuocRepository.GetAllAsync()).ToList();
		}

		public async Task<Request_HienThiChiTietThuocDTO> GetByIdAsync(int id)
		{
			var findThuoc = await _thuocRepository.GetSingleByIdAsync(id);
			if (findThuoc == null)
			{
				throw new Exception("Không tìm thấy thuốc");
			}
			var findLoaiThuoc = await _loaiThuocRepository.GetSingleByIdAsync(findThuoc.LoaiThuocId);
			if (findLoaiThuoc == null)
			{
				throw new Exception("Không tìm thấy loại thuốc");
			}

			var rs = new Request_HienThiChiTietThuocDTO
			{
				Id = findThuoc.Id,
				TenThuoc = findThuoc.TenThuoc,
				Image = findThuoc.Images,
				SoLuongTon = findThuoc.SoLuongTon,
				GiaNhap =  findThuoc.GiaNhap,
				NgaySanXuat = findThuoc.NgaySanXuat,
				HanSuDung = findThuoc.HanSuDung,
				LoaiThuocId = findThuoc.LoaiThuocId,
				TenLoaiThuoc = findLoaiThuoc.TenLoaiThuoc

			};
			return rs;

        }

		public async Task<ResponeMessage> UpdateThuoc(Request_UpdateThuocDTO? request)
		{
			if (request == null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
			}

			var findThuoc = await _thuocRepository.GetSingleByIdAsync(request.Id);
			if (findThuoc == null)
			{
				return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy thuốc");
			}

			
			var findLoaiThuoc = await _loaiThuocRepository.GetSingleByIdAsync(request.LoaiThuocId);
			if (findLoaiThuoc == null)
			{
				return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy loại thuốc");
			}

			findThuoc.TenThuoc = request.TenThuoc;
			findThuoc.Images = request.Images;
			findThuoc.LoaiThuocId = request.LoaiThuocId;

			await _unitOfWork.CommitAsync();

			return new ResponeMessage(HttpStatusCode.Ok, "Sửa thông tin thuốc thành công");
		}

		public async Task<IEnumerable<Request_HienThiDanhSachThuocDTO>> HienThiDanhSachThuoc()
		{
			var thuocs = await _thuocRepository.GetAllAsync();
			var result = new List<Request_HienThiDanhSachThuocDTO>();
			foreach (var thuoc in thuocs)
			{
				var findTenLoaiThuoc = await _loaiThuocRepository.GetSingleByIdAsync(thuoc.LoaiThuocId);
				result.Add(new Request_HienThiDanhSachThuocDTO
				{
					Id = thuoc.Id,
					TenThuoc = thuoc.TenThuoc,
					Images = thuoc.Images,
					SoLuongTon = thuoc.SoLuongTon,
					GiaNhap = thuoc.GiaNhap,
					NgaySanXuat = thuoc.NgaySanXuat,
					HanSuDung = thuoc.HanSuDung,
					LoaiThuocId = thuoc.LoaiThuocId,
					TenLoaiThuoc = findTenLoaiThuoc.TenLoaiThuoc
				});
			}

			return result;
		}

		public async Task<ResponeMessage> DeleteThuoc(int id)
		{
			var findThuoc = await _thuocRepository.GetSingleByIdAsync(id);
			if (findThuoc == null)
			{
				return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy thuốc");
			}
			var findChiTietPhieuNhapThuoc = (await _chiTietPhieuNhapThuocRepository.GetAllAsync()).Where(ctpnt => ctpnt.ThuocId == id);
			if (findChiTietPhieuNhapThuoc != null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Thuốc đã được nhập vào phiếu nhập thuốc, không thể xóa");
			}
			var findChiTietDonThuoc = (await _chiTietDonThuocRepository.GetAllAsync()).Where(ctdt => ctdt.ThuocId == id);
			if (findChiTietDonThuoc != null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Thuốc đã được nhập vào đơn thuốc, không thể xóa");
			}
			_thuocRepository.Delete(findThuoc);
			await _unitOfWork.CommitAsync();
			return new ResponeMessage(HttpStatusCode.Ok, "Xóa thuốc thành công");
		}
	}
}