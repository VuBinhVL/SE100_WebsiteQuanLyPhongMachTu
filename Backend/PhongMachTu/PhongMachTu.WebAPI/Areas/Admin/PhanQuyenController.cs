using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/phan-quyen")]
    [ApiController]
    public class PhanQuyenController : ControllerBase
    {
        private readonly IChucNangService _chucNangService;
        public PhanQuyenController(IChucNangService chucNangService)
        {
            _chucNangService = chucNangService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllChucNangAsync()
        {
            try
            {
                return Ok(await _chucNangService.GetAllChucNangAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new {message = "Có lỗi xảy ra từ phía máy chủ"});
            }
        }
    }
}
