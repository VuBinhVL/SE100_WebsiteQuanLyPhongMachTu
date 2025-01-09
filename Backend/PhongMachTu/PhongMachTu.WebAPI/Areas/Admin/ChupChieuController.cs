using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChupChieu;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-chup-chieu")]
    [ApiController]
    public class ChupChieuController : ControllerBase
    {

        private readonly IChupChieuService _chupChieuService;
        public ChupChieuController(IChupChieuService chupChieuService)
        {
            _chupChieuService = chupChieuService;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteChiTietKhamBenhAsync(int id)
        {
            try
            {
                var rsp = await _chupChieuService.DeleteChupChieuByIdAsync(id);
                return StatusCode(rsp.HttpStatusCode, new { message = rsp.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateChupChieuByIdAsync(Request_UpdateChupChieuDTO data)
        {
            try
            {
                var rsp = await _chupChieuService.UpdateChupChieuByIdAsync(data);
                return StatusCode(rsp.HttpStatusCode, new { message = rsp.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
