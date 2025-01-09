using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Customer
{
    [Area("CUSTOMER")]
    [Route("api/quan-li-ca-kham")]
    [ApiController]
    public class CaKhamController : ControllerBase
    {
        private readonly ICaKhamService _caKhamService;
       
        public CaKhamController(ICaKhamService caKhamService)
        {
            _caKhamService = caKhamService;
        }


        [HttpPost("dang-ky")]
        public async Task<IActionResult> DangKyCaKhamAsync(Request_DangKyCaKhamDTO data)
        {
            try
            {
                var rs = await _caKhamService.DangKyCaKhamAsync(data,HttpContext);
                return StatusCode(rs.HttpStatusCode, rs.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }


        [HttpGet("")]
        public async Task<IActionResult> HienThiCaKhamDaDangKyAsync()
        {
            try
            {
                var result = await _caKhamService.GetCaKhamDaDangKyAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

    }
}
