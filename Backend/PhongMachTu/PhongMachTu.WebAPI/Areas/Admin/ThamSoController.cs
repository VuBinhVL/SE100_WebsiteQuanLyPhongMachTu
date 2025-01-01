using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ThamSo;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-tham-so")]
    [ApiController]
    public class ThamSoController : ControllerBase
    {
        private readonly IThamSoService _thamSoService;
        public ThamSoController(IThamSoService thamSoService)
        {
            _thamSoService = thamSoService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var rs = await _thamSoService.GetAllAsync();
                return StatusCode(HttpStatusCode.Ok, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var rs = await _thamSoService.GetByIdAsync(id);
                return StatusCode(HttpStatusCode.Ok, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateThamSoAsync([FromBody] Request_UpdateThamSoDTO request)
        {
            try
            {
                var rs = await _thamSoService.UpdateThamSo(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
