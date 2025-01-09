using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-chi-tiet-kham-benh")]
    public class ChiTietKhamBenhController : ControllerBase
    {
        private readonly IChiTietKhamBenhService _chiTietKhamBenhService;
        public ChiTietKhamBenhController(IChiTietKhamBenhService chiTietKhamBenhService)
        {
            _chiTietKhamBenhService = chiTietKhamBenhService;
        }

        [HttpGet("detail")]
        public async Task<IActionResult> DetailChiTietKhamBenhAsync(int id)
        {
            try
            {
                var rsp = await _chiTietKhamBenhService.DetailChiTietKhamBenhAsync(id);
                if (rsp == null)
                {
                    return NotFound();
                }
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
