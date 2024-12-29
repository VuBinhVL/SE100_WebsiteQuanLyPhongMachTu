using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.CaKham;
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
    public interface ICaKhamService
    {
        Task<List<CaKham>> GetAllAsync();
        Task<ResponeMessage> AddCaKham(Request_AddCaKhamDTO caKham);
        Task<CaKham> GetByIdAsync(int id);
        Task<ResponeMessage> UpdateCaKham(Request_UpdateCaKhamDTO? request);
        Task<ResponeMessage> DeleteCaKham(int id);
    }
    public class CaKhamService : ICaKhamService
    {
        private readonly ICaKhamRepository _caKhamRepository;
        private readonly ILichKhamRepository _lichKhamRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INguoiDungRepository _bacSiRepository;

        public CaKhamService(ICaKhamRepository caKhamRepository, IUnitOfWork unitOfWork, INguoiDungRepository bacSiRepository, ILichKhamRepository lichKhamRepository)
        {
            _caKhamRepository = caKhamRepository;
            _unitOfWork = unitOfWork;
            _bacSiRepository = bacSiRepository;
            _lichKhamRepository = lichKhamRepository;
        }
        public async Task<ResponeMessage> AddCaKham(Request_AddCaKhamDTO caKham)
        {
            if (caKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findBacSiId = (await _bacSiRepository.GetAllAsync()).Where(d => d.Id == caKham.BacSiId).FirstOrDefault();
            // Kiểm tra BacSiId có tồn tại
            var bacSiExists = await _bacSiRepository.GetSingleByIdAsync(caKham.BacSiId ?? -1);
            if (bacSiExists == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Bác sĩ không tồn tại");
            }
            await _caKhamRepository.AddAsync(new CaKham()
            {
                TenCaKham = caKham.TenCaKham,
                ThoiGianBatDau = caKham.ThoiGianBatDau,
                ThoiGianKetThuc = caKham.ThoiGianKetThuc,
                NgayKham = caKham.NgayKham,
                SoLuongBenhNhanToiDa = caKham.SoLuongBenhNhanToiDa,
                BacSiId = caKham.BacSiId,

            });
            await _unitOfWork.CommitAsync();

            return new ResponeMessage(HttpStatusCode.Ok, "Thêm ca khám thành công");
        }

       
        public async Task<List<CaKham>> GetAllAsync()
        {
            return (await _caKhamRepository.GetAllAsync()).ToList();
        }

        public async Task<CaKham> GetByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            return await _caKhamRepository.GetSingleByIdAsync(id);
        }

        public async Task<ResponeMessage> UpdateCaKham(Request_UpdateCaKhamDTO? request)
        {
            if (request == null ||
     string.IsNullOrEmpty(request.TenCaKham) ||
     request.ThoiGianBatDau == default ||
     request.ThoiGianKetThuc == default ||
     request.NgayKham == default ||
     request.SoLuongBenhNhanToiDa <= 0 ||
     request.BacSiId == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }


            var findCaKham = await _caKhamRepository.GetSingleByIdAsync(request.Id ?? -1);
            if (findCaKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy ca khám cần sửa");
            }
            // Kiểm tra BacSiId có tồn tại
            var bacSiExists = await _bacSiRepository.GetSingleByIdAsync(request.BacSiId??-1);
            if (bacSiExists == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Bác sĩ không tồn tại");
            }

            findCaKham.TenCaKham = request.TenCaKham;
            findCaKham.BacSiId = request.BacSiId;
            findCaKham.ThoiGianBatDau = request.ThoiGianBatDau;
            findCaKham.ThoiGianKetThuc = request.ThoiGianKetThuc;
            findCaKham.NgayKham = request.NgayKham;
            findCaKham.SoLuongBenhNhanToiDa = request.SoLuongBenhNhanToiDa;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa ca khám thành công");
        }

        public async Task<ResponeMessage> DeleteCaKham(int id)
        {
            var findCaKham = await _caKhamRepository.GetSingleByIdAsync(id);
            if (findCaKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy ca khám cần xóa");
            }

            var findLichKhamByCaKhamId = (await _lichKhamRepository.GetAllAsync()).Where(p => p.Id == id).FirstOrDefault();
            if (findLichKhamByCaKhamId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa ca khám này vì có lịch khám có ID {findLichKhamByCaKhamId.Id} đang thuộc về");
            }
            _caKhamRepository.Delete(findCaKham);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa ca khám thành công");
        }

    }
}
