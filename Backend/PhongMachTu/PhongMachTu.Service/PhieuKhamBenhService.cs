using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuKhamBenh;
using PhongMachTu.Common.DTOs.Respone;
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
    }
    public class PhieuKhamBenhService : IPhieuKhamBenhService
    {
        private readonly IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        public PhieuKhamBenhService(IPhieuKhamBenhRepository phieuKhamBenhRepository)
        {
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
        }
        public async Task<IEnumerable<PhieuKhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync()
        {
            var listPhieuKhamBenh = await _phieuKhamBenhRepository.GetListPhieuKhamBenhDTOsAsync();

            return listPhieuKhamBenh;
        }

    }
}
