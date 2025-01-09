using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.Service;
using System.Net.Http;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-ca-kham")]
    [ApiController]
    public class CaKhamController : ControllerBase
    {
        private readonly ICaKhamService _caKhamService;
        public CaKhamController(ICaKhamService caKhamService)
        {
            _caKhamService = caKhamService;
        }
        //[HttpGet("")]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var rs = await _caKhamService.GetAllAsync();
        //    if (!rs.Any())
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, new { message = "Không có dữ liệu" });
        //    }

        //    return StatusCode(StatusCodes.Status200OK, rs);
        //}
        [HttpPost("add")]
        public async Task<IActionResult> AddCaKhamAsync(Request_AddCaKhamDTO? request)
        {
            var rs = await _caKhamService.AddCaKham(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
        [HttpGet("detail")]
        public async Task<IActionResult> GetCaKhamByIdAsync(int? id)
        {
            var rs = await _caKhamService.GetByIdAsync(id ?? -1);
            if (rs == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = $"Không tìm thấy ca khám có id là {id}" });
            }

            return StatusCode(StatusCodes.Status200OK, rs);
        }
        [HttpPut("edit")]
        public async Task<IActionResult> UpdateCaKhamAsync(Request_UpdateCaKhamDTO? request)
        {
            var rs = await _caKhamService.UpdateCaKham(request);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCaKhamAsync(int? id)
        {
            var rs = await _caKhamService.DeleteCaKham(id ?? -1);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
        [HttpGet("")]
        public async Task<IActionResult> HienThiDanhSachCaKhamPhiaAdminAsync()
        {
            try
            {
                var rs = await _caKhamService.HienThiDanhSachCaKhamPhiaAdmin();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpPut("dang-ky-ca-kham")]
        public async Task<IActionResult> DangKyCaKhamAsync(Request_DangKyCaKhamChoBacSiDTO request)
        {
            try
            {
                var rs = await _caKhamService.DangKyCaKhamChoBacSiAsync( request, HttpContext);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }


        [HttpGet("my-self")]
        public async Task<IActionResult> GetCaKhamsMySelfAsync()
        {
            try
            {
                var rs = await _caKhamService.GetCaKhamsMySelfAsync(HttpContext);
                if (rs == null)
                {
                    return Unauthorized();
                }
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

    }
}



