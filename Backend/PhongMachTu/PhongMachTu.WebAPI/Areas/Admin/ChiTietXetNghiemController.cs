using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietDonThuoc;
using PhongMachTu.Common.DTOs.Request.ChiTietXetNghiem;
using PhongMachTu.Common.DTOs.Respone.ChiTietXetNghiem;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-chi-tiet-xet-nghiem")]
    [ApiController]
    public class ChiTietXetNghiemController : ControllerBase
    {
        private readonly IChiTietXetNghiemService _chiTietXetNghiemService;
        public ChiTietXetNghiemController(IChiTietXetNghiemService chiTietXetNghiemService)
        {
            _chiTietXetNghiemService = chiTietXetNghiemService;
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteChiTietXetNghiemAsync(int chiTietKhamBenhId, int loaiXetNghiemId)
        {
            try
            {
                var rsp = await _chiTietXetNghiemService.DeleteChiTietXetNghiemAsync(chiTietKhamBenhId, loaiXetNghiemId);
                return StatusCode(rsp.HttpStatusCode, new { message = rsp.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }



        [HttpPost("add-or-update")]
        public async Task<IActionResult> AddOrUpdateChiTietXetNghiemAsync(Request_AddOrUpdateChiTietXetNghiemDTO data)
        {
            try
            {
                var rsp = await _chiTietXetNghiemService.AddOrUpdateChiTietXetNghiemAsync(data);
                if (rsp.ResponeMessage.HttpStatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest(new { message = rsp.ResponeMessage.Message });
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


