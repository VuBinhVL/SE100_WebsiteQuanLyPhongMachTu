using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.BenhNhan;
using PhongMachTu.Common.DTOs.Request.NguoiDung;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccessController : ControllerBase
    {

        private readonly IBenhNhanService _benhNhanService;
        private readonly INguoiDungService _nguoiDungService;
        public AccessController(IBenhNhanService benhNhanService, INguoiDungService nguoiDungService)
        {
            _benhNhanService = benhNhanService;
            _nguoiDungService = nguoiDungService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(Request_RegisterDTO data)
        {
            try
            {
                var rs = await _benhNhanService.RegisterAsync(data);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Request_LoginDTO data)
        {
            try
            {
                var rs = await _nguoiDungService.LoginAsync(data);
                if (rs.HttpStatusCode == HttpStatusCode.BadRequest)
                {
                    return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
                }
                return StatusCode(rs.HttpStatusCode, new { roleName = rs.Message, token = rs.Token });
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(Request_ForgotPasswordDTO data)
        {
            try
            {
                var rs = await _nguoiDungService.ForgotPasswordAsync(data);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
