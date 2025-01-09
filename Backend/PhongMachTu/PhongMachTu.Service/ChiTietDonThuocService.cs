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
    public interface IChiTietDonThuocService
    {
        Task<ResponeMessage> DeleteChiTietDonThuocAsync(int chiTietKhamBenhId, int thuocId);
    }
    public class ChiTietDonThuocService : IChiTietDonThuocService
    {
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IChiTietDonThuocRepository _chiTietDonThuocRepository;
        public readonly IUnitOfWork _unitOfWork;
        public ChiTietDonThuocService(IChiTietKhamBenhRepository chiTietKhamBenhRepository, IChiTietDonThuocRepository chiTietDonThuocRepository, IUnitOfWork unitOfWork)
        {
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponeMessage> DeleteChiTietDonThuocAsync(int chiTietKhamBenhId, int thuocId)
        {
            var findChiTietDonThuoc = (await _chiTietDonThuocRepository.FindAsync(t=>t.ChiTietKhamBenhId==chiTietKhamBenhId && t.ThuocId==thuocId)).FirstOrDefault();
            if (findChiTietDonThuoc == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest,"Không tìm thấy chi tiết đơn thuốc đã chọn");
            }

            var findCTKB = await _chiTietKhamBenhRepository.GetSingleWithIncludesAsync(c => c.Id == chiTietKhamBenhId, c => c.PhieuKhamBenh, c => c.PhieuKhamBenh.LichKham);
            if(findCTKB.PhieuKhamBenh.LichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Kham)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Chỉ có thể xóa khi trạng thái lịch khám là đang khám");
            }

            _chiTietDonThuocRepository.Delete(findChiTietDonThuoc);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa chi tiết đơn thuốc thành công");
        }
    }
}
