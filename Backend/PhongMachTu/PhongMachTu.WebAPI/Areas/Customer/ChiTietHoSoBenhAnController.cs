using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Customer
{
    [Area("CUSTOMER")]
    [Route("api/quan-li-chi-tiet-ho-so-benh-an")]
    [ApiController]
    public class ChiTietHoSoBenhAnController : ControllerBase
    {
        private readonly IChiTietHoSoBenhAnService _chiTietHoSoBenhAnService;
        public ChiTietHoSoBenhAnController(IChiTietHoSoBenhAnService chiTietHoSoBenhAnService)
        {
            _chiTietHoSoBenhAnService = chiTietHoSoBenhAnService;
        }
        [HttpGet("hien-thi-chi-tiet-ho-so-benh-an")]
        public async Task<IActionResult> HienThiChiTietKhamBenhAsync()
        {
            try
            {
                var rs = await _chiTietHoSoBenhAnService.HienThiChiTietHoSoBenhAnAsync(HttpContext);
                return StatusCode(rs.HttpStatusCode, rs.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
