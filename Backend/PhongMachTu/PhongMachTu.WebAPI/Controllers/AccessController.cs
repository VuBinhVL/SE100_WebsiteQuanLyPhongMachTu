using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var rs = await _benhNhanService.RegisterAsync(data);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Request_LoginDTO data)
        {
            var rs = await _nguoiDungService.LoginAsync(data);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
    }
}
