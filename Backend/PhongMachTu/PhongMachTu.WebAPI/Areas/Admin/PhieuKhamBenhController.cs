using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuKhamBenh;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-phieu-kham-benh")]
    [ApiController]
    public class PhieuKhamBenhController : ControllerBase
    {
        private readonly IPhieuKhamBenhService _phieuKhamBenhService;
        public PhieuKhamBenhController(IPhieuKhamBenhService phieuKhamBenhService)
        {
            _phieuKhamBenhService = phieuKhamBenhService;
        }
        [HttpGet("")]
        public async Task<IActionResult> HienThiDanhSachPhieuKhamBenh()
        {
            try
            {
                var rs = await _phieuKhamBenhService.GetListPhieuKhamBenhDTOsAsync();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data)
        {
            try
            {
                var rs = await _phieuKhamBenhService.AddPhieuKhamBenhAsync(data);
                return StatusCode(rs.HttpStatusCode, rs.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpGet("detail")]
        public async Task<IActionResult> DetailPhieuKhamBenhAsync(int id)
        {
            try
            {
                var rsp = await _phieuKhamBenhService.DetailPhieuKhamBenhAsync(id);
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
