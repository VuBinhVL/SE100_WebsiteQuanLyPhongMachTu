using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Service;

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

    }
}
