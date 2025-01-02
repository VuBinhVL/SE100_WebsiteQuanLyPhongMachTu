using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Customer
{
    [Area("CUSTOMER")]
    [Route("api/quan-li-thong-tin-ca-nhan")]
    [ApiController]
    public class ThongTinCaNhanController : ControllerBase
    {
        private readonly INguoiDungService _nguoiDungService;
        public ThongTinCaNhanController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }
        [HttpGet("hien-thi-thong-tin-ca-nhan")]
        public async Task<IActionResult> HienThiThongTinCaNhanAsync()
        {
            try
            {
                var rs = await _nguoiDungService.HienThiThongTinNguoiDungAsync(HttpContext);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
