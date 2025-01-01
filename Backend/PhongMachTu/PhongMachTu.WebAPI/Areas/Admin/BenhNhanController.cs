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
        private readonly INguoiDungService _nguoiDungService;
        public BenhNhanController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }
        [HttpGet("hien-thi-danh-sach-benh-nhan")]
        public async Task<IActionResult> HienThiDanhSachBenhNhanAsync()
        {
            try
            {
                var rs = await _nguoiDungService.HienThiDanhSachBenhNhanAsync();
                return StatusCode(StatusCodes.Status200OK, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
