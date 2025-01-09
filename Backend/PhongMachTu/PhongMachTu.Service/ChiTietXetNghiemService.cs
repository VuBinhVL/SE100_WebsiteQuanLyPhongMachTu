using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface IChiTietXetNghiemService
    {
        Task<ResponeMessage> DeleteChiTietXetNghiemAsync(int chiTietKhamBenhId, int loaiXetNghiemId);
    }
    public class ChiTietXetNghiemService : IChiTietXetNghiemService
    {
        private readonly IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChiTietXetNghiemService(IChiTietXetNghiemRepository chiTietXetNghiemRepository,IChiTietKhamBenhRepository chiTietKhamBenhRepository ,IUnitOfWork unitOfWork)
        {
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponeMessage> DeleteChiTietXetNghiemAsync(int chiTietKhamBenhId, int loaiXetNghiemId)
        {
            var findCTXN = (await _chiTietXetNghiemRepository.FindAsync(c => c.ChiTietKhamBenhId == chiTietKhamBenhId && c.LoaiXetNghiemId == loaiXetNghiemId)).FirstOrDefault();
            if(findCTXN == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest,"Không tìm thấy chi tiết xét nghiệm đã chọn");
            }

            var findCTKB = (await _chiTietKhamBenhRepository.FindWithIncludeAsync(c => c.Id == chiTietKhamBenhId, c => c.PhieuKhamBenh, c => c.PhieuKhamBenh.LichKham)).FirstOrDefault();
            if (findCTKB == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy chi tiết khám bệnh của chi tiết xét nghiệm đã chọn");
            }

            if (findCTKB.PhieuKhamBenh.LichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Kham)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Trạng thái lịch khám phải là đang khám mới được phép xóa");
            }
            _chiTietXetNghiemRepository.Delete(findCTXN);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa chi tiết xét nghiệm thành công");
        }
    }
}
