using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface IChucNangService
    {
        Task<IEnumerable<ChucNang>> GetAllChucNangAsync();
    }

    public class ChucNangService : IChucNangService
    {

        private readonly IChucNangRepository _chucNangRepository;
        public ChucNangService(IChucNangRepository chucNangRepository)
        {
            _chucNangRepository = chucNangRepository;
        }
        public async Task<IEnumerable<ChucNang>> GetAllChucNangAsync()
        {
            return await _chucNangRepository.GetAllAsync();
        }
    }
}
