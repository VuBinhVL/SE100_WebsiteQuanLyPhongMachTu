using PhongMachTu.Common.DTOs.Request.ThongKe;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface IThongKeService
    {
        Task<Request_HienThiThongKeDTO> HienThiThongKe(DateTime startDay, DateTime endDay);
        Task<List<Request_BieuDoDoanhThuDTO>> HienThiThongKeTheoThang(DateTime startDay, DateTime endDay);
    }
    public class ThongKeService : IThongKeService
    {
        private readonly IThongKeRepository _thongKeRepository;
        public ThongKeService(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }
        public async Task<Request_HienThiThongKeDTO> HienThiThongKe(DateTime startDay, DateTime endDay)
        {
            return await _thongKeRepository.HienThiThongKeAsync(startDay, endDay);
        }
        public async Task<List<Request_BieuDoDoanhThuDTO>> HienThiThongKeTheoThang(DateTime startDay, DateTime endDay)
        {
            return await _thongKeRepository.HienThiThongKeTheoThangAsync(startDay, endDay);
        }
    }
}
