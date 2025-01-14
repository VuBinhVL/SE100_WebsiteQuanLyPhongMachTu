﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuKhamBenh;
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
        [HttpGet("")]
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

        [HttpPost("add")]
        public async Task<IActionResult> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data)
        {
            try
            {
                var rs = await _phieuKhamBenhService.AddPhieuKhamBenhAsync(data);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpGet("detail")]
        public async Task<IActionResult> DetailPhieuKhamBenhAsync(int id)
        {
            try
            {
                var rsp = await _phieuKhamBenhService.DetailPhieuKhamBenhAsync(id);
                if (rsp == null)
                {
                    return NotFound();
                }
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpGet("my-self")]
        public async Task<IActionResult> GetListPhieuKhamBenhsMySelfAsync()
        {
            try
            {
                var rs = await _phieuKhamBenhService.GetListPhieuKhamBenhsByUserCurAsync(HttpContext);
                if (rs == null)
                {
                    return Unauthorized();
                }
                var data = rs
                        .Select(pkb => new
                        {
                            pkb.Id,
                            pkb.TenBenhNhan,
                            pkb.NgayTao,
                            pkb.SoDienThoai,
                            pkb.TenTrangThaiPKB
                        }).ToList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpGet("xac-nhan-thanh-toan")]
        public async Task<IActionResult> XacNhanThanhToanAsync(int id)
        {
            try
            {
                var rs = await _phieuKhamBenhService.XacNhanThanhToanAsync(id);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

    }
}
