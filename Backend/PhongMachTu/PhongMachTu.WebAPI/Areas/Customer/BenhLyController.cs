using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Customer
{
    [Area("CUSTOMER")]
    [Route("api/quan-li-benh-ly")]
    [ApiController]
    public class BenhLyController : ControllerBase
    {
        private readonly IBenhLyService _benhLyService;
        public BenhLyController(IBenhLyService benhLyService)
        {
            _benhLyService = benhLyService;
        }
        [HttpGet("hien-thi-bang-gia-benh-ly")]
        public async Task<IActionResult> HienThiBenhLyAsync()
        {
            try
            {
                var rs = await _benhLyService.HienThiBangGiaBenhLy();
                return StatusCode(rs.HttpStatusCode, rs.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("hien-thi-chi-tiet-benh-ly-o-dich-vu")]
        public async Task<IActionResult> HienThiChiTietBenhLyAsync()
        {
            try
            {
                var rs = await _benhLyService.HienThiChiTietBenhLy();
                return StatusCode(rs.HttpStatusCode, rs.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
