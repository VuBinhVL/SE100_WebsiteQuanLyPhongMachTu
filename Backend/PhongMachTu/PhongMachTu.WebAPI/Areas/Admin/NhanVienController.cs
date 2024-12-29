using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.NhanVien;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.Helpers;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.Service;
using System.Diagnostics;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/quan-li-nhan-vien")]
    [ApiController]
  //  [Authorize]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;
        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var rs = (await _nhanVienService.GetAllAsync());
                return StatusCode(HttpStatusCode.Ok, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, new { message = HttpStatusCode.MsgHeThongGapSuCo });
            }
           
        }

        [HttpGet("detail")]
      //  [Authorize(Policy = Const_ChucNang.Quan_Ly_Nhan_Vien_Edit)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            try
            {
                var rs = await _nhanVienService.GetNhanVienByIdAsync(id);
                if (rs == null)
                {
                    return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy nhân viên có id là {id}" });
                }
                else
                {
                    return StatusCode(HttpStatusCode.Ok, rs);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, new { message = HttpStatusCode.MsgHeThongGapSuCo });
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNhanVienAsync([FromBody] Request_AddNhanVienDTO request)
        {
            try
            {
                var rs = await _nhanVienService.AddNhanVienAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch(Exception ex) 
            {
                return StatusCode(HttpStatusCode.InternalServerError, new { message = HttpStatusCode.MsgHeThongGapSuCo });
            }
        }


        [HttpPut("update-thong-tin-ca-nhan")]
     //   [Authorize(Policy = Const_ChucNang.Quan_Ly_Nhan_Vien_Edit)]
        public async Task<IActionResult> UpdateThongTinCaNhanNhanVienAsync([FromBody] Request_UpdateThongTinCaNhanNhanVienDTO request)
        {
            try
            {
                var rs = await _nhanVienService.UpdateThongTinCaNhanNhanVienAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, new { message = HttpStatusCode.MsgHeThongGapSuCo });
            }
        }

        [HttpDelete("delete")]
      //  [Authorize(Policy = Const_ChucNang.Quan_Ly_Nhan_Vien_Delete)]
        public async Task<IActionResult> DeleteNhanVienAsync(int? id)
        {
            var rs = await _nhanVienService.DeleteNhanVienByIdAsync(id ?? -1);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
    }
    
}
