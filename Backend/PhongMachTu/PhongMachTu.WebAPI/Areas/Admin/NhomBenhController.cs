using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.DonViTinh;
using PhongMachTu.Common.DTOs.Request.NhomBenh;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-nhom-benh")]
    [ApiController]
    public class NhomBenhController : ControllerBase
    {
        private readonly INhomBenhService _nhomBenhService;
        public NhomBenhController(INhomBenhService nhomBenhService)
        {
            _nhomBenhService = nhomBenhService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var rs = await _nhomBenhService.GetAllAsync();
            if (!rs.Any())
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = "Không có dữ liệu" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddNhomBenhAsync(Request_AddNhomBenhDTO? request)
        {
            var rs = await _nhomBenhService.AddNhomBenh(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetNhomBenhByIdAsync(int? id)
        {
            var rs = await _nhomBenhService.GetByIdAsync(id ?? -1);
            if (rs == null)
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy nhóm bệnh có id là {id}" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateNhomBenhAsync(Request_UpdateNhomBenhDTO? request)
        {
            var rs = await _nhomBenhService.UpdateNhomBenh(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }



        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNhomBenhAsync(int? id)
        {
            var rs = await _nhomBenhService.DeleteNhomBenh(id ?? -1);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
    }
}
