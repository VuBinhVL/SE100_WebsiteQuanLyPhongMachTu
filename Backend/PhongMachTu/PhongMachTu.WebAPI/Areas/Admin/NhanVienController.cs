using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.NhanVien;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.DTOs.Respone.NhanVien;
using PhongMachTu.Common.Helpers;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.Service;
using PhongMachTu.WebAPI.Mapper;
using System.Diagnostics;

namespace PhongMachTu.WebAPI.Areas.Admin
{
    [Area("ADMIN")]
    [Route("api/admin/quan-li-nhan-vien")]
    [ApiController]
  //  [Authorize]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;
        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var list = new List<Respone_NhanVienDTO>();
                var rs = await _nhanVienService.GetAllAsync();
                foreach(var item in rs)
                {
                    list.Add(NhanVienMapper.Map_NguoiDungModel_To_Respone_NhanVienDTO(item));
                }
                return StatusCode(HttpStatusCode.Ok, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
           
        }

        [HttpGet("detail")]
      //  [Authorize(Policy = Const_ChucNang.Quan_Ly_Nhan_Vien_Edit)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            try
            {
                var rs = await _nhanVienService.GetNhanVienByIdAsync(id);
                if (rs == null)
                {
                    return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy nhân viên có id là {id}" });
                }
                else
                {
                    return StatusCode(HttpStatusCode.Ok, rs);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNhanVienAsync([FromBody] Request_AddNhanVienDTO request)
        {
            try
            {
                var rs = await _nhanVienService.AddNhanVienAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch(Exception ex) 
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }


        [HttpPut("update-thong-tin-ca-nhan")]
     //   [Authorize(Policy = Const_ChucNang.Quan_Ly_Nhan_Vien_Edit)]
        public async Task<IActionResult> UpdateThongTinCaNhanNhanVienAsync([FromBody] Request_UpdateThongTinCaNhanNhanVienDTO request)
        {
            try
            {
                var rs = await _nhanVienService.UpdateThongTinCaNhanNhanVienAsync(request);
                return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpDelete("delete")]
      //  [Authorize(Policy = Const_ChucNang.Quan_Ly_Nhan_Vien_Delete)]
        public async Task<IActionResult> DeleteNhanVienAsync(int? id)
        {
            var rs = await _nhanVienService.DeleteNhanVienByIdAsync(id ?? -1);
            return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
        }


        [HttpPut("phan-quyen")]
        public async Task<IActionResult>PhanQuyenAsync(Request_PhanQuyenDTO data)
        {
            try
            {
                var rs = await _nhanVienService.PhanQuyenAsync(data);
                return StatusCode(rs.HttpStatusCode,rs.Message);
            }
            catch(Exception e)
            {
                return BadRequest(new {message = "Có lỗi xảy ra từ hệ thống"});
            }
        }
        [HttpGet("hien-thi-thong-tin-ca-nhan-ben-phia-admin")]
        public async Task<IActionResult> HienThiThongTinCaNhanBenPhiaAdminAsync()
        {
            try
            {
                var rs = await _nhanVienService.HienThiThongTinCaNhanBenAdmin(HttpContext);
                return StatusCode(rs.HttpStatusCode, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("hien-thi-form-sua-thong-tin-ca-nhan")]
        public async Task<IActionResult> HienThiFormSuaThongTinCaNhanAsync()
        {
            try
            {
                var rs = await _nhanVienService.HienThiFormSuaThongTinCaNhan(HttpContext);
                return StatusCode(rs.HttpStatusCode, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
        [HttpGet("hien-thi-danh-sach-nhan-vien")]
        public async Task<IActionResult> HienThiDanhSachNhanVienAsync()
        {
            try
            {
                var rs = await _nhanVienService.HienThiDanhSachNhanVien();
                return StatusCode(rs.HttpStatusCode, rs);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }
    }
    
}
