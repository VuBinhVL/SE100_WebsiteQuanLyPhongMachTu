using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-phieu-kham-benh")]
    [ApiController]
    public class PhieuKhamBenhController : ControllerBase
    {
        private readonly IPhieuKhamBenhService _phieuKhamBenhService;
        public PhieuKhamBenhController(IPhieuKhamBenhService phieuKhamBenhService)
        {
            _phieuKhamBenhService = phieuKhamBenhService;
        }
        [HttpGet("hien-thi-danh-sach-phieu-kham-benh")]
        public async Task<IActionResult> HienThiDanhSachPhieuKhamBenh()
        {
            try
            {
                var rs = await _phieuKhamBenhService.GetListPhieuKhamBenhDTOsAsync();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }



    }
}
