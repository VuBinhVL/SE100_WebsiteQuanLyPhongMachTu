using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-lich-kham")]
    [ApiController]
    public class LichKhamController : ControllerBase
    {
        private readonly ILichKhamService _lichKhamService;
        public LichKhamController(ILichKhamService lichkhamService)
        {
            _lichKhamService = lichkhamService;
        }
        [HttpGet("")]
        public async Task<IActionResult> HienThiDanhSachLichKhamPhiaAdminAsync(int CaKhamId)
        {
            var rs = await _lichKhamService.HienThiDanhSachLichKhamPhiaAdmin(CaKhamId);
            return Ok(rs);
        }
        [HttpGet("trang-thai-lich-kham")]
        public async Task<IActionResult> GetTrangThaiLichKhamAsync()
        {
            var rs = await _lichKhamService.GetTrangThaiLichKham();
            return Ok(rs);
        }
    }
}
