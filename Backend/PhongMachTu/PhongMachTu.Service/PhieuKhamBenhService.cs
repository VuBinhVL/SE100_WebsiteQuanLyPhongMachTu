using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuKhamBenh;
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
    public interface IPhieuKhamBenhService
    {
        Task<IEnumerable<PhieuKhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync();
        Task<ResponeMessage> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data);
    }
    public class PhieuKhamBenhService : IPhieuKhamBenhService
    {
        private readonly IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        private readonly ILichKhamRepository _lichKhamRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PhieuKhamBenhService(IPhieuKhamBenhRepository phieuKhamBenhRepository, ILichKhamRepository lichKhamRepository, IUnitOfWork unitOfWork)
        {
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
            _lichKhamRepository = lichKhamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data)
        {
            var findLichKham = await _lichKhamRepository.GetSingleByIdAsync(data.LichKhamId);
            if (findLichKham == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound,"Không tìm thấy lịch khám đã chọn");
            }

            var findPhieuKhamBenh = (await _phieuKhamBenhRepository.FindAsync(p=>p.LichKhamId == data.LichKhamId)).FirstOrDefault();
            if (findPhieuKhamBenh != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Phiếu khám bệnh cho lịch khám này đã tồn tại");
            }

            var pkb = new PhieuKhamBenh
            {
                NgayTao = DateTime.Now,
                LichKhamId = data.LichKhamId,
            };

            await _phieuKhamBenhRepository.AddAsync(pkb);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Tạo phiếu khám bệnh thành công");
        }

        public async Task<IEnumerable<PhieuKhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync()
        {
            var listPhieuKhamBenh = await _phieuKhamBenhRepository.GetListPhieuKhamBenhDTOsAsync();

            return listPhieuKhamBenh;
        }



    }
}
