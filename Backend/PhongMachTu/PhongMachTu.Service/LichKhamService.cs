using Org.BouncyCastle.Asn1.Ocsp;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.LichKhamAdmin;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Models;
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
        Task<IEnumerable<LichKhamDTO>> HienThiDanhSachLichKhamPhiaAdmin(int CaKhamId);
        Task<IEnumerable<TrangThaiLichKham>> GetTrangThaiLichKham();
    }
    public class LichKhamService : ILichKhamService
    {
        private readonly ILichKhamRepository _lichKhamRepository;
        private readonly ITrangThaiLichKhamRepository _trangThaiLichKhamRepository;
        public LichKhamService(ILichKhamRepository lichKhamRepository, ITrangThaiLichKhamRepository trangThaiLichKhamRepository)
        {
            _lichKhamRepository = lichKhamRepository;
            _trangThaiLichKhamRepository = trangThaiLichKhamRepository;
        }

        public async Task<IEnumerable<TrangThaiLichKham>> GetTrangThaiLichKham()
        {
            var listTrangThaiLichKham = await _trangThaiLichKhamRepository.GetAllAsync();
            return listTrangThaiLichKham;
        }

        public async Task<IEnumerable<LichKhamDTO>> HienThiDanhSachLichKhamPhiaAdmin(int CaKhamId)
        {
            return await _lichKhamRepository.GetListLichKhamDTOsAsync(CaKhamId);

           

        }
    }
}
