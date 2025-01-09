using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-chi-tiet-xet-nghiem")]
    [ApiController]
    public class ChiTietXetNghiemController : ControllerBase
    {
        private readonly IChiTietXetNghiemService _chiTietXetNghiemService;
        public ChiTietXetNghiemController(IChiTietXetNghiemService chiTietXetNghiemService)
        {
            _chiTietXetNghiemService = chiTietXetNghiemService;
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteChiTietXetNghiemAsync(int chiTietKhamBenhId, int loaiXetNghiemId)
        {
            try
            {
                var rsp = await _chiTietXetNghiemService.DeleteChiTietXetNghiemAsync(chiTietKhamBenhId, loaiXetNghiemId);
                return StatusCode(rsp.HttpStatusCode, new { message = rsp.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
