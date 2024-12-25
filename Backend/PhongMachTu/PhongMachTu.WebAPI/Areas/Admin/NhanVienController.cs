using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.NhanVien;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.Service;
using System.Diagnostics;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/quan-li-nhan-vien")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;
        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
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
    }
    
}
