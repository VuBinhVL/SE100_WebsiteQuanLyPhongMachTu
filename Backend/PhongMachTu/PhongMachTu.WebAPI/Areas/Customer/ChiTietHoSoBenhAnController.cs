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
        [HttpGet("")]
        public async Task<IActionResult> HienThiChiTietKhamBenhAsync(int HoSoBenhAnID)
        {
            try
            {
                var rs = await _chiTietHoSoBenhAnService.HienThiChiTietHoSoBenhAnAsync(HttpContext, HoSoBenhAnID);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("chi-tiet-phieu-kham-benh/{phieuKhamBenhId}")]
        public async Task<IActionResult> HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(int phieuKhamBenhId)
        {
            try
            {
                var rs = await _chiTietHoSoBenhAnService.HienThiChiTietPhieuKhamBenhCuaChiTietHoSoBenhAnAsync(HttpContext, phieuKhamBenhId);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
