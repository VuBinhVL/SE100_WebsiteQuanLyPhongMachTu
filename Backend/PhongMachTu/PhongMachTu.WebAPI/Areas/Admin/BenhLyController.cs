using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.BenhLy;
using PhongMachTu.Common.DTOs.Request.NhomBenh;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-benh-ly")]
    [ApiController]
    public class BenhLyController : ControllerBase
    {
        private readonly IBenhLyService _benhLyService;
        public BenhLyController(IBenhLyService benhLyService)
        {
            _benhLyService = benhLyService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var rs = await _benhLyService.GetAllAsync();
            if (!rs.Any())
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = "Không có dữ liệu" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddBenhLyAsync(Request_AddBenhLyDTO? request)
        {
            var rs = await _benhLyService.AddBenhLy(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetBenhLyByIdAsync(int? id)
        {
            var rs = await _benhLyService.GetByIdAsync(id ?? -1);
            if (rs == null)
            {
                return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy bệnh lý có id là {id}" });
            }

            return StatusCode(HttpStatusCode.Ok, rs);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateBenhLyAsync(Request_UpdateBenhLyDTO? request)
        {
            var rs = await _benhLyService.UpdateBenhLy(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }



        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBenhLyAsync(int? id)
        {
            var rs = await _benhLyService.DeleteBenhLy(id ?? -1);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
    }
}
