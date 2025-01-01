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
        Task<ResponeMessage> HienThiDanhSachLichKhamPhiaAdmin();
    }
    public class LichKhamService : ILichKhamService
    {
        private readonly ILichKhamRepository _lichKhamRepository;
        public LichKhamService(ILichKhamRepository lichKhamRepository)
        {
            _lichKhamRepository = lichKhamRepository;
        }
        public async Task<ResponeMessage> HienThiDanhSachLichKhamPhiaAdmin()
        {
            var listLichKham = await _lichKhamRepository.GetListLichKhamDTOsAsync();
            if (listLichKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy danh sách lịch khám.");
            }
            var rs = new Request_HienThiDanhSachLichKhamDTO()
            {
                LichKhamList = listLichKham.ToList()
            };
            // Chuyển đổi kết quả sang JSON và trả về trong ResponeMessage
            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(rs);
            return new ResponeMessage(HttpStatusCode.Ok, responseJson);

        }
    }
}
