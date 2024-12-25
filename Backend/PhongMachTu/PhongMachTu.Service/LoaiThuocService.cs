using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.DonViTinh;
using PhongMachTu.Common.DTOs.Request.LoaiThuoc;
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
    public interface ILoaiThuocService
    {
        Task<List<LoaiThuoc>> GetAllAsync();
        Task<ResponeMessage> AddLoaiThuoc(Request_AddLoaiThuocDTO loaithuoc);
        Task<LoaiThuoc> GetByIdAsync(int id);
        Task<ResponeMessage> UpdateLoaiThuoc(Request_UpdateLoaiThuocDTO? request);
        Task<ResponeMessage> DeleteLoaiThuoc(int id);
    }
    public class LoaiThuocService: ILoaiThuocService
    {
        private readonly ILoaiThuocRepository _loaiThuocRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IThuocRepository _thuocRepository;
        public LoaiThuocService(ILoaiThuocRepository loaiThuocRepository, IUnitOfWork unitOfWork, IThuocRepository thuocRepository)
        {
            _loaiThuocRepository = loaiThuocRepository;
            _unitOfWork = unitOfWork;
            _thuocRepository = thuocRepository;
        }

        public async Task<ResponeMessage> AddLoaiThuoc(Request_AddLoaiThuocDTO loaithuoc)
        {
            if (loaithuoc == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findLoaiThuoc = (await _loaiThuocRepository.GetAllAsync()).Where(d => d.TenLoaiThuoc.Trim().ToLower() == loaithuoc.TenLoaiThuoc.Trim().ToLower()).FirstOrDefault();
            if (findLoaiThuoc != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên loại thuốc này đã có rồi");
            }

            await _loaiThuocRepository.AddAsync(new LoaiThuoc()
            {
                TenLoaiThuoc = loaithuoc.TenLoaiThuoc
            });
            await _unitOfWork.CommitAsync();

            return new ResponeMessage(HttpStatusCode.Ok, "Thêm loại thuốc thành công");
        }

        public async Task<ResponeMessage> DeleteLoaiThuoc(int id)
        {
            var findLoaiThuoc = await _loaiThuocRepository.GetSingleByIdAsync(id);
            if (findLoaiThuoc == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy loại thuốc cần xóa");
            }

            var findThuocsByLoaiThuocId = (await _thuocRepository.GetAllAsync()).Where(p => p.LoaiThuocId == id).FirstOrDefault();
            if (findThuocsByLoaiThuocId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa loại thuốc này vì có thuốc {findThuocsByLoaiThuocId.TenThuoc} đang thuộc về");
            }
            _loaiThuocRepository.Delete(findLoaiThuoc);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa loại thuốc thành công");
        }

        public async Task<List<LoaiThuoc>> GetAllAsync()
        {
            return (await _loaiThuocRepository.GetAllAsync()).ToList();
        }

        public async Task<LoaiThuoc> GetByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            return await _loaiThuocRepository.GetSingleByIdAsync(id);

        }

        public async Task<ResponeMessage> UpdateLoaiThuoc(Request_UpdateLoaiThuocDTO? request)
        {
            if (request == null || request.Id == null || string.IsNullOrEmpty(request.TenLoaiThuoc))
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findLoaiThuocbyId = await _loaiThuocRepository.GetSingleByIdAsync(request.Id ?? -1);
            if (findLoaiThuocbyId == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findLoaiThuoc = (await _loaiThuocRepository.GetAllAsync()).Where(d => d.TenLoaiThuoc.Trim().ToLower() == request.TenLoaiThuoc.Trim().ToLower()).FirstOrDefault();
            if (findLoaiThuoc != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên loại thuốc này đã có rồi");
            }

            findLoaiThuocbyId.TenLoaiThuoc = request.TenLoaiThuoc;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa tên loại thuốc thành công");
        }
    }
}
