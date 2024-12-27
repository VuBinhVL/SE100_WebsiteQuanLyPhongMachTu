using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.DTOs.Request.BenhNhan;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Controllers
{
    [ApiController]
    public class AccessController : ControllerBase
    {

        private readonly IBenhNhanService _benhNhanService;
        public AccessController(IBenhNhanService benhNhanService)
        {
            _benhNhanService = benhNhanService;
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> RegisterAsync(Request_RegisterDTO data)
        {
            var rs = await _benhNhanService.RegisterAsync(data);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }
    }
}
