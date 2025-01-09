using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietDonThuoc;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-chi-tiet-don-thuoc")]
    [ApiController]
    public class ChiTietDonThuocController : ControllerBase
    {
        private readonly IChiTietDonThuocService _chiTietDonThuocService;
        public ChiTietDonThuocController(IChiTietDonThuocService chiTietDonThuocService)
        {
            _chiTietDonThuocService = chiTietDonThuocService;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteChiTietKhamBenhAsync(int chiTietKhamBenhId, int thuocId)
        {
            try
            {
                var rsp = await _chiTietDonThuocService.DeleteChiTietDonThuocAsync(chiTietKhamBenhId,thuocId);
                return StatusCode(rsp.HttpStatusCode, new { message = rsp.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }



        [HttpPost("add-or-update")]
        public async Task<IActionResult> AddOrUpdateChiTietDonThuocAsync(Request_AddOrUpdateChiTietDonThuocDTO data)
        {
            try
            {
                var rsp = await _chiTietDonThuocService.AddOrUpdateChiTietDonThuocAsync(data);
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



