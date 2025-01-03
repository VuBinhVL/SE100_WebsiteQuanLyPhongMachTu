using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.DataAccess.Models;
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
        [HttpGet("bang-gia-benh-ly")]
        public async Task<IActionResult> HienThiBenhLyAsync()
        {
            try
            {
                var rs = await _benhLyService.HienThiBangGiaBenhLy();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("chi-tiet-benh-ly")]
        public async Task<IActionResult> HienThiChiTietBenhLyAsync(int benhLyId)
        {
            try
            {
                var rs = await _benhLyService.HienThiChiTietBenhLy(benhLyId);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
}
