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
        Task<ResponeMessage> GetListPhieuKhamBenhDTOsAsync();
    }
    public class PhieuKhamBenhService : IPhieuKhamBenhService
    {
        private readonly IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        public PhieuKhamBenhService(IPhieuKhamBenhRepository phieuKhamBenhRepository)
        {
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
        }
        public async Task<ResponeMessage> GetListPhieuKhamBenhDTOsAsync()
        {
            var listPhieuKhamBenh = await _phieuKhamBenhRepository.GetListPhieuKhamBenhDTOsAsync();
            if (listPhieuKhamBenh == null || !listPhieuKhamBenh.Any())
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy danh sách phiếu khám bệnh.");
            }

            // Chuyển đổi danh sách trực tiếp sang JSON
            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(listPhieuKhamBenh);

            // Trả về ResponeMessage với dữ liệu JSON
            return new ResponeMessage(HttpStatusCode.Ok, responseJson);
        }

    }
}
