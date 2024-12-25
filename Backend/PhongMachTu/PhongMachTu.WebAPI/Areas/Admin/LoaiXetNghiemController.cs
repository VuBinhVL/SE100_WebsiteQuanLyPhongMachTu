using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/quan-li-loai-xet-nghiem")]
    [ApiController]
    public class LoaiXetNghiemController : ControllerBase
    {
        private readonly ILoaiXetNghiemService _loaiXetNghiemService;
        public LoaiXetNghiemController(ILoaiXetNghiemService loaiXetNghiemService)
        {
            _loaiXetNghiemService = loaiXetNghiemService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var rs = await _loaiXetNghiemService.GetAllAsync();
                return StatusCode(HttpStatusCode.Ok, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, new { message = HttpStatusCode.MsgHeThongGapSuCo });
            }

        }
    }
}
