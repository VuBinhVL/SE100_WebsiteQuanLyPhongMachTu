using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.DonViTinh;
using PhongMachTu.Common.DTOs.Request.LoaiThuoc;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-loai-thuoc")]
    [ApiController]
    public class LoaiThuocController : ControllerBase
    {
        private readonly ILoaiThuocService _loaiThuocService;
        public LoaiThuocController(ILoaiThuocService loaiThuocService)
        {
            _loaiThuocService = loaiThuocService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var rs = await _loaiThuocService.GetAllAsync();
            if (!rs.Any())
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = "Không có dữ liệu" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddLoaiThuocAsync(Request_AddLoaiThuocDTO? request)
        {
            var rs = await _loaiThuocService.AddLoaiThuoc(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }


        [HttpGet("getbyid")]
        public async Task<IActionResult> GetLoaiThuocByIdAsync(int? id)
        {
            var rs = await _loaiThuocService.GetByIdAsync(id ?? -1);
            if (rs == null)
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy loại thuốc có id là {id}" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }


        [HttpPut("edit")]
        public async Task<IActionResult> UpdateLoaiThuochAsync(Request_UpdateLoaiThuocDTO? request)
        {
            var rs = await _loaiThuocService.UpdateLoaiThuoc(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }



        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLoaiThuochAsync(int? id)
        {
            var rs = await _loaiThuocService.DeleteLoaiThuoc(id ?? -1);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
    }
}
