using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietKhamBenh;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-chi-tiet-kham-benh")]
    [ApiController]

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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteChiTietKhamBenhAsync(int id)
        {
            try
            {
                var rsp = await _chiTietKhamBenhService.DeleteChiTietKhamBenhAsync(id);
                return StatusCode(rsp.HttpStatusCode, new { message = rsp.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpPost("add-or-update")]
        public async Task<IActionResult> AddOrUpdateChiTietKhamBenhAsync(Request_AddOrUpdateChiTietKhamBenhDTO data)
        {
            try
            {
                var rsp = await _chiTietKhamBenhService.AddOrUpdateChiTietKhamBenhAsync(data);
                if (rsp.ResponeMessage.HttpStatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest(new { message = rsp.ResponeMessage.Message });
                }

                return Ok(new { message = rsp.ResponeMessage.Message, idAdd = rsp.IdAdd });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpPost("update-ghi-chu")]
        public async Task<IActionResult> UpdateGhiChuForChiTietKhamBenhAsync(Request_UpdateGhiChuForChiTietKhamBenhDTO data)
        {
            try
            {
                var rsp = await _chiTietKhamBenhService.UpdateGhiChuForChiTietKhamBenhAsync(data);
                return StatusCode(rsp.HttpStatusCode, new { message = rsp.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        
    }
}


