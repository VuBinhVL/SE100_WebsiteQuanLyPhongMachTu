using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.Thuoc;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-thuoc")]
    [ApiController]
    public class ThuocController : ControllerBase
    {
        private readonly IThuocService _thuocService;
        public ThuocController(IThuocService thuocService)
        {
            _thuocService = thuocService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var rs = await _thuocService.GetAllAsync();
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
                var rs = await _thuocService.GetByIdAsync(id);
                return StatusCode(HttpStatusCode.Ok, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateThuocAsync([FromBody] Request_UpdateThuocDTO request)
        {
            try
            {
                var rs = await _thuocService.UpdateThuoc(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("hien-thi-danh-sach-thuoc")]
        public async Task<IActionResult> HienThiDanhSachThuocAsync()
        {
            try
            {
                var rs = await _thuocService.HienThiDanhSachThuoc();
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
