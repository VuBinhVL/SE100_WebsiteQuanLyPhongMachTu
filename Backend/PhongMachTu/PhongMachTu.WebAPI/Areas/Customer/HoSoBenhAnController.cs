using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.HoSoBenhAn;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Customer
{
    [Area("CUSTOMER")]
    [Route("api/quan-li-ho-so-benh-an")]
    [ApiController]
    public class HoSoBenhAnController : ControllerBase
    {
        private readonly IHoSoBenhAnService _hoSoBenhAnService;
        public HoSoBenhAnController(IHoSoBenhAnService hoSoBenhAnService)
        {
            _hoSoBenhAnService = hoSoBenhAnService;
        }
        [HttpGet("hien-thi-ho-so-benh-an")]
        public async Task<IActionResult> HienThiHoSoBenhAnAsync()
        {
            try
            {
                var rs = await _hoSoBenhAnService.HienThiHoSoBenhAnAsync(HttpContext);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
