using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.DonViTinh;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
	public interface IDonViTinhService
	{
		Task<List<DonViTinh>> GetAllAsync();
		Task<ResponeMessage> AddDonViTinh(Request_AddDonViTinhDTO donViTinh);
		Task<DonViTinh> GetByIdAsync(int id);
		Task<ResponeMessage> UpdateDonViTinh(Request_UpdateDonViTinhDTO? request);
		Task<ResponeMessage> DeleteDonViTinh(int id);
	}

	public class DonViTinhService : IDonViTinhService
	{

		private readonly IDonViTinhRepository _donViTinhRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILoaiXetNghiemRepository _loaiXetNghiemRepository;
		public DonViTinhService(IDonViTinhRepository donViTinhRepository, IUnitOfWork unitOfWork, ILoaiXetNghiemRepository loaiXetNghiemRepository)
		{
			_donViTinhRepository = donViTinhRepository;
			_unitOfWork = unitOfWork;
			_loaiXetNghiemRepository = loaiXetNghiemRepository;
		}

		public async Task<ResponeMessage> AddDonViTinh(Request_AddDonViTinhDTO donViTinh)
		{
			if (donViTinh == null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
			}

			var findDVT = (await _donViTinhRepository.GetAllAsync()).Where(d => d.TenDonViTinh.Trim().ToLower() == donViTinh.TenDonViTinh.Trim().ToLower()).FirstOrDefault();
			if (findDVT != null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Tên đơn vị tính này đã có rồi");
			}

			await _donViTinhRepository.AddAsync(new DonViTinh()
			{
				TenDonViTinh = donViTinh.TenDonViTinh
			});
			await _unitOfWork.CommitAsync();

			return new ResponeMessage(HttpStatusCode.Ok, "Thêm đơn vị tính thành công");
		}

		public async Task<ResponeMessage> DeleteDonViTinh(int id)
		{
			var findDVT = await _donViTinhRepository.GetSingleByIdAsync(id);
			if (findDVT == null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy đơn vị tính cần xóa");
			}

			var findLoaiXetNghiemsByDVTId = (await _loaiXetNghiemRepository.GetAllAsync()).Where(p => p.DonViTinhId == id).FirstOrDefault();
			if (findLoaiXetNghiemsByDVTId != null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa đơn vị tính này vì có loại xét nghiệm {findLoaiXetNghiemsByDVTId.TenXetNghiem} đang thuộc về");
			}
			_donViTinhRepository.Delete(findDVT);
			await _unitOfWork.CommitAsync();
			return new ResponeMessage(HttpStatusCode.Ok, "Xóa đơn vị tính thành công");
		}

		public async Task<List<DonViTinh>> GetAllAsync()
		{
			return (await _donViTinhRepository.GetAllAsync()).ToList();
		}

		public async Task<DonViTinh> GetByIdAsync(int id)
		{
			if (id == -1)
			{
				return null;
			}
			return await _donViTinhRepository.GetSingleByIdAsync(id);

		}

		public async Task<ResponeMessage> UpdateDonViTinh(Request_UpdateDonViTinhDTO? request)
		{
			if (request == null || request.Id == null || string.IsNullOrEmpty(request.TenDonViTinh))
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
			}

			var findDVT = await _donViTinhRepository.GetSingleByIdAsync(request.Id ?? -1);
			if (findDVT == null)
			{
				return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
			}

			findDVT.TenDonViTinh = request.TenDonViTinh;
			await _unitOfWork.CommitAsync();
			return new ResponeMessage(HttpStatusCode.Ok, "Sửa tên đơn vị tính thành công");
		}
	}
}
