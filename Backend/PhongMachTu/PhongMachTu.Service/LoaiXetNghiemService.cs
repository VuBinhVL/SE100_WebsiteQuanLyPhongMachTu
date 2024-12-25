using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface ILoaiXetNghiemService
    {
        Task<List<LoaiXetNghiem>> GetAllAsync();
    }

    public class LoaiXetNghiemService : ILoaiXetNghiemService
    {
        private readonly ILoaiXetNghiemRepository _loaiXetNghiemRepository;
        public LoaiXetNghiemService(ILoaiXetNghiemRepository loaiXetNghiemRepository)
        {
            _loaiXetNghiemRepository = loaiXetNghiemRepository;
        }

        public async Task<List<LoaiXetNghiem>> GetAllAsync()
        {
            var rs = await _loaiXetNghiemRepository.GetAllWithIncludeAsync(l=>l.DonViTinh);
            return rs.ToList();
        }
    }
}
