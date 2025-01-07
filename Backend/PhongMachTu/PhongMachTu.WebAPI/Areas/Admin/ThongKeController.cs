using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/dashboard")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeService _thongKeService;
        public ThongKeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }
        [HttpGet("thong-ke")]
        public async Task<IActionResult> HienThiThongKeAsync(DateTime startDay, DateTime endDay)
        {
            try
            {
                var rs = await _thongKeService.HienThiThongKe(startDay, endDay);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("bieu-do-doanh-thu")]
        public async Task<IActionResult> HienThiThongKeTheoThangAsync(DateTime startDay, DateTime endDay)
        {
            try
            {
                var rs = await _thongKeService.HienThiThongKeTheoThang(startDay, endDay);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
