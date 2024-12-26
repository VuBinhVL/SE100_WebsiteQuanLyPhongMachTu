using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuNhapThuoc;
using PhongMachTu.Common.DTOs.Request.PhieuNhapThuoc;
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
    public interface IPhieuNhapThuocService
    {
        Task<List<PhieuNhapThuoc>> GetAllAsync();
        Task<ResponeMessage> AddPhieuNhapThuoc(Request_AddPhieuNhapThuocDTO phieuNhapThuoc);
        Task<PhieuNhapThuoc> GetByIdAsync(int id);
        Task<ResponeMessage> UpdatePhieuNhapThuoc(Request_UpdatePhieuNhapThuocDTO? request);
        Task<ResponeMessage> DeletePhieuNhapThuoc(int id);
    }
    public class PhieuNhapThuocService : IPhieuNhapThuocService
    {
        private readonly IPhieuNhapThuocRepository _phieuNhapThuocRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IThuocRepository _thuocRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IChiTietPhieuNhapThuocRepository _chiTietPhieuNhapThuocRepository;

        public PhieuNhapThuocService(IPhieuNhapThuocRepository phieuNhapThuocRepository, IUnitOfWork unitOfWork, IThuocRepository thuocRepository, INguoiDungRepository nguoiDungRepository,IChiTietPhieuNhapThuocRepository chiTietPhieuNhapThuocRepository)
        {
            _phieuNhapThuocRepository = phieuNhapThuocRepository;
            _unitOfWork = unitOfWork;
            _thuocRepository = thuocRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _chiTietPhieuNhapThuocRepository = chiTietPhieuNhapThuocRepository;
        }
        public async Task<ResponeMessage> AddPhieuNhapThuoc(Request_AddPhieuNhapThuocDTO phieuNhapThuoc)
        {
            if (phieuNhapThuoc == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }



            await _phieuNhapThuocRepository.AddAsync(new PhieuNhapThuoc()
            {
                NgayNhap = phieuNhapThuoc.NgayNhap
            });
            await _unitOfWork.CommitAsync();

            return new ResponeMessage(HttpStatusCode.Ok, "Thêm phiếu nhập thuốc thành công");
        }


        public async Task<List<PhieuNhapThuoc>> GetAllAsync()
        {
            return (await _phieuNhapThuocRepository.GetAllAsync()).ToList();
        }

        public async Task<PhieuNhapThuoc> GetByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            return await _phieuNhapThuocRepository.GetSingleByIdAsync(id);
        }

        public async Task<ResponeMessage> UpdatePhieuNhapThuoc(Request_UpdatePhieuNhapThuocDTO? request)
        {
            if (request == null || request.Id == null || request.NgayNhap == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

           

            var findPhieuNhapThuocbyId = await _phieuNhapThuocRepository.GetSingleByIdAsync(request.Id ?? -1);
            if (findPhieuNhapThuocbyId == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            findPhieuNhapThuocbyId.NgayNhap = request.NgayNhap;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa phiếu nhập thuốc thành công");
        }

        public async Task<ResponeMessage> DeletePhieuNhapThuoc(int id)
        {
            var findPhieuNhapThuoc = await _phieuNhapThuocRepository.GetSingleByIdAsync(id);
            if (findPhieuNhapThuoc == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy phiếu nhập thuốc cần xóa");
            }

            var findChiTietPhieuNhapThuocsByPhieuNhapThuocId = (await _chiTietPhieuNhapThuocRepository.GetAllAsync()).Where(p => p.PhieuNhapThuocId == id).FirstOrDefault();
            if (findChiTietPhieuNhapThuocsByPhieuNhapThuocId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa phiếu nhập thuốc này vì có chi tiết phiếu nhập thuộc có ID {findChiTietPhieuNhapThuocsByPhieuNhapThuocId.PhieuNhapThuocId} đang thuộc về");
            }

            

            
            _phieuNhapThuocRepository.Delete(findPhieuNhapThuoc);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa phiếu nhập thuốc thành công");
        }
    }
}

