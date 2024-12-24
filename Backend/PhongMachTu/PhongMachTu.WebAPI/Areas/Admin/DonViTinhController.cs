using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.DonViTinh;
using PhongMachTu.Service;

namespace PhongMachTu.WebAPI.Areas.Admin
{
	[Area("ADMIN")]
	[Route("api/quanli-donvitinh")]
	[ApiController]
	public class DonViTinhController : ControllerBase
	{
		private readonly IDonViTinhService _donViTinhService;
		public DonViTinhController(IDonViTinhService donViTinhService)
		{
			_donViTinhService = donViTinhService;
		}

		[HttpGet("getall")]
		public async Task<IActionResult> GetAllAsync()
		{
			var rs = await _donViTinhService.GetAllAsync();
			if (!rs.Any())
			{
				return StatusCode(HttpStatusCode.NotFound, new { message = "Không có dữ liệu" });
			}

			return StatusCode(HttpStatusCode.Ok, rs);
		}


		[HttpPost("add")]
		public async Task<IActionResult> AddDonViTinhAsync(Request_AddDonViTinhDTO? request)
		{
			var rs = await _donViTinhService.AddDonViTinh(request);
			return StatusCode(rs.HttpStatusCode, new {message=rs.Message});
		}


		[HttpGet("getbyid")]
		public async Task<IActionResult> GetDonViTinhByIdAsync(int ? id)
		{
			var rs = await _donViTinhService.GetByIdAsync(id??-1);
			if (rs==null)
			{
				return StatusCode(HttpStatusCode.NotFound, new { message = $"Không tìm thấy đơn vị tính có id là {id}" });
			}

			return StatusCode(HttpStatusCode.Ok, rs);
		}


		[HttpPut("edit")]
		public async Task<IActionResult> UpdateDonViTinhAsync(Request_UpdateDonViTinhDTO? request)
		{
			var rs = await _donViTinhService.UpdateDonViTinh(request);
			return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
		}



		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteDonViTinhAsync(int ? id)
		{
			var rs = await _donViTinhService.DeleteDonViTinh(id??-1);
			return StatusCode(rs.HttpStatusCode, new { message = rs.Message });
		}
	}
}
