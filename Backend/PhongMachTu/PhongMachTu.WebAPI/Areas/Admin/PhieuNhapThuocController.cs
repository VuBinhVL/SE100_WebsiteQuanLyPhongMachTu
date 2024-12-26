using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuNhapThuoc;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/quan-li-phieu-nhap-thuoc")]
    [ApiController]
    public class PhieuNhapThuocController : ControllerBase
    {
        private readonly IPhieuNhapThuocService _phieuNhapThuocService;
        public PhieuNhapThuocController(IPhieuNhapThuocService phieuNhapThuocService)
        {
            _phieuNhapThuocService = phieuNhapThuocService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var rs = await _phieuNhapThuocService.GetAllAsync();
            if (!rs.Any())
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = "Không có dữ liệu" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPhieuNhapThuocAsync(Request_AddPhieuNhapThuocDTO? request)
        {
            var rs = await _phieuNhapThuocService.AddPhieuNhapThuoc(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetPhieuNhapThuocByIdAsync(int? id)
        {
            var rs = await _phieuNhapThuocService.GetByIdAsync(id ?? -1);
            if (rs == null)
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy phiếu nhập thuốc có id là {id}" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdatePhieuNhapThuocAsync(Request_UpdatePhieuNhapThuocDTO? request)
        {
            var rs = await _phieuNhapThuocService.UpdatePhieuNhapThuoc(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePhieuNhapThuocAsync(int id)
        {
            var rs = await _phieuNhapThuocService.DeletePhieuNhapThuoc(id);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
    }
}
