using Org.BouncyCastle.Asn1.Ocsp;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.LichKhamAdmin;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface ILichKhamService
    {
        Task<IEnumerable<LichKhamDTO>> HienThiDanhSachLichKhamPhiaAdmin();
    }
    public class LichKhamService : ILichKhamService
    {
        private readonly ILichKhamRepository _lichKhamRepository;
        public LichKhamService(ILichKhamRepository lichKhamRepository)
        {
            _lichKhamRepository = lichKhamRepository;
        }
        public async Task<IEnumerable<LichKhamDTO>> HienThiDanhSachLichKhamPhiaAdmin()
        {
            return await _lichKhamRepository.GetListLichKhamDTOsAsync();

           

        }
    }
}
