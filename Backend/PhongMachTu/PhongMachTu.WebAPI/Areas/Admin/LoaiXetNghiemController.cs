using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.BenhLy;
using PhongMachTu.Common.DTOs.Request.LoaiXetNghiem;
using PhongMachTu.Common.DTOs.Request.NhanVien;
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

        [HttpPost("add")]
        public async Task<IActionResult> AddLoaiXetNghiemAsync([FromBody] Request_AddLoaiXetNghiemDTO request)
        {
            try
            {
                var rs = await _loaiXetNghiemService.AddLoaiXetNghiemAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, new { message = HttpStatusCode.MsgHeThongGapSuCo });
            }
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateLoaiXetNghiemAsync([FromBody] Request_UpdateLoaiXetNghiemDTO request)
        {
            try
            {
                var rs = await _loaiXetNghiemService.UpdateLoaiXetNghiemAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, new { message = HttpStatusCode.MsgHeThongGapSuCo });
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLoaiXetNghiemAsync(int? id)
        {
            var rs = await _loaiXetNghiemService.DeleteLoaiXetNghiemByIdAsync(id ?? -1);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }

    }
}
