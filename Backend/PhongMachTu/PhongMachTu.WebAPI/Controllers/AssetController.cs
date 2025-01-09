using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.Helpers;

namespace PhongMachTu.WebAPI.Controllers
{
    [Route("api/asset")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        public AssetController()
        {
            if (!Directory.Exists(Utils.GetPathUpload()))
            {
                Directory.CreateDirectory(Utils.GetPathUpload());
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { message = "Bạn chưa chọn file." });
                }

                var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Utils.GetPathUpload(), fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { fileName = fileName });
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

        [HttpGet("view-image/{fileName}")]
        public IActionResult ViewImage(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Utils.GetPathUpload(), fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Không tìm thấy hình ảnh");
                }

                var fileExtension = Path.GetExtension(fileName).ToLower();
                string contentType = fileExtension switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".bmp" => "image/bmp",
                    _ => "application/octet-stream" // Dùng mặc định nếu không xác định được
                };

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, contentType);
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError, HttpStatusCode.HeThongGapSuCo);
            }
        }

    }
}
