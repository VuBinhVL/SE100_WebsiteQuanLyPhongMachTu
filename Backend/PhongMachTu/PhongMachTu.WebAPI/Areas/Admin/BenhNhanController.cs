using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.BenhNhan;
using PhongMachTu.Common.DTOs.Request.NhanVien;
using PhongMachTu.Common.DTOs.Respone.NhanVien;
using PhongMachTu.Service;
using PhongMachTu.WebAPI.Mapper;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-benh-nhan")]
    [ApiController]
    public class BenhNhanController : ControllerBase
    {
        private readonly IBenhNhanService _benhNhanService;
        public BenhNhanController(IBenhNhanService benhNhanService)
        {
            _benhNhanService = benhNhanService;
        }
        [HttpGet("hien-thi-danh-sach-benh-nhan")]
        public async Task<IActionResult> HienThiDanhSachBenhNhanAsync()
        {
            try
            {
                var rs = await _benhNhanService.HienThiDanhSachBenhNhanAsync();
                return StatusCode(StatusCodes.Status200OK, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpGet("detail")]
        public async Task<IActionResult> GetBenhNhanByIdAsync(int id)
        {
            try
            {
                var rs = await _benhNhanService.GetBenhNhanByIdAsync(id);
                if (rs == null)
                {
                    return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy bệnh nhân có id là {id}" });
                }
                else
                {
                    return StatusCode(HttpStatusCode.Ok, rs);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBenhNhanAsync([FromBody] Request_AddBenhNhanDTO request)
        {
            try
            {
                var rs = await _benhNhanService.AddBenhNhanAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpPut("update-thong-tin-benh-nhan")]
        public async Task<IActionResult> UpdateBenhNhanAsync([FromBody] Request_UpdateThongTinCaNhanBenhNhanDTO request)
        {
            try
            {
                var rs = await _benhNhanService.UpdateBenhNhanAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBenhNhanByIdAsync(int id)
        {
            try
            {
                var rs = await _benhNhanService.DeleteBenhNhanByIdAsync(id);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

    }
}
