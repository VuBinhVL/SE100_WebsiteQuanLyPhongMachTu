using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface ICaKhamService
    {
        Task<List<CaKham>> GetAllAsync();
        Task<ResponeMessage> AddCaKham(Request_AddCaKhamDTO caKham);
        Task<CaKham> GetByIdAsync(int id);
        Task<ResponeMessage> UpdateCaKham(Request_UpdateCaKhamDTO? request);
        Task<ResponeMessage> DeleteCaKham(int id);
        Task<ResponeMessage> DangKyCaKhamAsync(Request_DangKyCaKhamDTO data, HttpContext httpContext);
        Task<ResponeMessage> DangKyCaKhamChoBacSiAsync(Request_DangKyCaKhamChoBacSiDTO data, HttpContext httpContext);
        Task<IEnumerable<Request_HienThiCaKhamDTO>> GetCaKhamDaDangKyAsync();
        Task<IEnumerable<Request_CaKhamCoSLBNDaDangKiDTO>> HienThiDanhSachCaKhamPhiaAdmin();
    }
    public class CaKhamService : ICaKhamService
    {
        private readonly ICaKhamRepository _caKhamRepository;
        private readonly ILichKhamRepository _lichKhamRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INguoiDungRepository _bacSiRepository;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IThamSoRepository _thamSoRepository;
        private readonly IMailService _mailService;

        public CaKhamService(ICaKhamRepository caKhamRepository, IUnitOfWork unitOfWork, INguoiDungRepository bacSiRepository, ILichKhamRepository lichKhamRepository, INguoiDungService nguoiDungService, IThamSoRepository thamSoRepository, IMailService mailService)
        {
            _caKhamRepository = caKhamRepository;
            _unitOfWork = unitOfWork;
            _bacSiRepository = bacSiRepository;
            _lichKhamRepository = lichKhamRepository;
            _nguoiDungService = nguoiDungService;
            _thamSoRepository = thamSoRepository;
            _mailService = mailService;
        }
        public async Task<ResponeMessage> AddCaKham(Request_AddCaKhamDTO caKham)
        {
            if (caKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            //var findBacSiId = (await _bacSiRepository.GetAllAsync()).Where(d => d.Id == caKham.BacSiId).FirstOrDefault();
            //// Kiểm tra BacSiId có tồn tại
            //var bacSiExists = await _bacSiRepository.GetSingleByIdAsync(caKham.BacSiId ?? -1);
            //if (bacSiExists == null)
            //{
            //    return new ResponeMessage(HttpStatusCode.BadRequest, "Bác sĩ không tồn tại");
            //}

            await _caKhamRepository.AddAsync(new CaKham()
            {
                NhomBenhId = caKham.NhomBenhId,
                TenCaKham = caKham.TenCaKham,
                ThoiGianBatDau = caKham.ThoiGianBatDau,
                ThoiGianKetThuc = caKham.ThoiGianKetThuc,
                NgayKham = caKham.NgayKham,
                SoLuongBenhNhanToiDa = caKham.SoLuongBenhNhanToiDa,
                BacSiId = null,

            });
            await _unitOfWork.CommitAsync();

            return new ResponeMessage(HttpStatusCode.Ok, "Thêm ca khám thành công");
        }


        public async Task<List<CaKham>> GetAllAsync()
        {
            return (await _caKhamRepository.GetAllAsync()).ToList();
        }

        public async Task<CaKham> GetByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            return await _caKhamRepository.GetSingleByIdAsync(id);
        }

        public async Task<ResponeMessage> UpdateCaKham(Request_UpdateCaKhamDTO? request)
        {
            if (request == null ||
             string.IsNullOrEmpty(request.TenCaKham) ||
             request.ThoiGianBatDau == default ||
             request.ThoiGianKetThuc == default ||
             request.NgayKham == default ||
             request.SoLuongBenhNhanToiDa <= 0 ||
             request.BacSiId == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }


            var findCaKham = await _caKhamRepository.GetSingleByIdAsync(request.Id ?? -1);
            if (findCaKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy ca khám cần sửa");
            }
            // Kiểm tra BacSiId có tồn tại
            var bacSiExists = await _bacSiRepository.GetSingleByIdAsync(request.BacSiId ?? -1);
            if (bacSiExists == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Bác sĩ không tồn tại");
            }

            findCaKham.TenCaKham = request.TenCaKham;
            findCaKham.BacSiId = request.BacSiId;
            findCaKham.ThoiGianBatDau = request.ThoiGianBatDau;
            findCaKham.ThoiGianKetThuc = request.ThoiGianKetThuc;
            findCaKham.NgayKham = request.NgayKham;
            findCaKham.SoLuongBenhNhanToiDa = request.SoLuongBenhNhanToiDa;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa ca khám thành công");
        }

        public async Task<ResponeMessage> DeleteCaKham(int id)
        {
            var findCaKham = await _caKhamRepository.GetSingleByIdAsync(id);
            if (findCaKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy ca khám cần xóa");
            }

            var findLichKhamByCaKhamId = (await _lichKhamRepository.GetAllAsync()).Where(p => p.CaKhamId == findCaKham.Id).FirstOrDefault();
            if (findLichKhamByCaKhamId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa ca khám này vì có lịch khám có ID {findLichKhamByCaKhamId.Id} đang thuộc về");
            }
            _caKhamRepository.Delete(findCaKham);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa ca khám thành công");
        }

        public async Task<ResponeMessage> DangKyCaKhamAsync(Request_DangKyCaKhamDTO data, HttpContext httpContext)
        {
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                return new ResponeMessage(HttpStatusCode.Unauthorized, "");
            }
            var findCaKham = (await _caKhamRepository.FindAsync(c => c.Id == data.CaKhamId && c.BacSiId != null)).FirstOrDefault();

            if (findCaKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy ca khám cần đăng ký");
            }
            var findBacSi = await _bacSiRepository.GetSingleWithIncludesAsync(b => b.Id == findCaKham.BacSiId, n => n.ChuyenMon);
            if (findBacSi == null || findBacSi.ChuyenMonId == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy ca khám cần đăng ký");
            }

            var findLichKhams = await _lichKhamRepository.FindAsync(l => l.CaKhamId == data.CaKhamId);


            if (findCaKham.SoLuongBenhNhanToiDa <= findLichKhams.Count())
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Xin lỗi.Ca khám hiện đang quá tải");
            }

            var findLichKhamByBenhNhanId = findLichKhams.Where(p => p.BenhNhanId == nguoiDung.Id).FirstOrDefault();
            if (findLichKhamByBenhNhanId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Bạn đã đăng ký ca khám này rồi");
            }

            var thamSo = await _thamSoRepository.GetSingleByIdAsync(1);

            if (!ConThoiHanDangKy(findCaKham, thamSo.SoPhutNgungDangKyTruocKetThuc))
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Xin lỗi, thời gian mở đăng ký cho ca khám này đã hết");
            }
            await _lichKhamRepository.AddAsync(new LichKham()
            {
                SoThuTu = findLichKhams.Count() + 1,
                CaKhamId = data.CaKhamId,
                TrangThaiLichKhamId = Const_TrangThaiLichKham.Dang_Cho,
                BenhNhanId = nguoiDung.Id,
            });

            var thoiGianBatDau = findCaKham.ThoiGianBatDau.ToString(@"hh\:mm");
            var thoiGianKetThuc = findCaKham.ThoiGianKetThuc.ToString(@"hh\:mm");
            var ngayKham = findCaKham.NgayKham.ToString("dd/MM/yyyy");

            var sendMail = await _mailService.SendEmailAsync(nguoiDung.Email,
     "Đăng ký ca khám thành công",
     $@"
    <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f9f9f9;
                }}
                .container {{
                    max-width: 600px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #ffffff;
                    border: 1px solid #ddd;
                    border-radius: 8px;
                }}
                h2 {{
                    text-align: center;
                    color: #333;
                }}
                table {{
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 20px;
                }}
                td {{
                    padding: 8px;
                    border: 1px solid #ddd;
                    font-size: 14px;
                    color: #555;
                }}
                strong {{
                    color: #333;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Thông Báo Đăng Ký Khám Bệnh</h2>
                <table>
                    <tr>
                        <td>Tên bệnh nhân</td>
                        <td><strong>{nguoiDung?.HoTen ?? "N/A"}</strong></td>
                    </tr>
                    <tr>
                        <td>Tên ca khám</td>
                        <td><strong>{findCaKham?.TenCaKham ?? "N/A"}</strong></td>
                    </tr>
                    <tr>
                        <td>Thời gian bắt đầu</td>
                        <td><strong>{thoiGianBatDau}</strong></td>
                    </tr>
                    <tr>
                        <td>Thời gian kết thúc</td>
                        <td><strong>{thoiGianKetThuc}</strong></td>
                    </tr>
                    <tr>
                        <td>Ngày khám</td>
                        <td><strong>{ngayKham}</strong></td>
                    </tr>
                    <tr>
                        <td>Bác sĩ khám</td>
                        <td><strong>{findCaKham?.BacSi?.HoTen ?? "N/A"}</strong></td>
                    </tr>
                    <tr>
                        <td>Nhóm bệnh khám</td>
                        <td><strong>{findBacSi?.ChuyenMon?.TenNhomBenh ?? "N/A"}</strong></td>
                    </tr>
                    <tr>
                        <td>Số thứ tự khám</td>
                        <td><strong>{findLichKhams?.Count() + 1}</strong></td>
                    </tr>
                </table>
            </div>
        </body>
    </html>
");

            await _unitOfWork.CommitAsync();

            if (sendMail)
            {
                return new ResponeMessage(HttpStatusCode.Ok, $"Đăng ký ca khám thành công, thông tin chi tiết đã gửi tới Email {nguoiDung.Email}");
            }
            else
            {
                return new ResponeMessage(HttpStatusCode.Ok, $"Đăng ký ca khám thành công, số thứ tự của bạn là {findLichKhams.Count() + 1}");
            }
        }

        public bool ConThoiHanDangKy(CaKham cakham, int soPhutNgungDangKyTruocKetThuc)
        {
            DateTime thoiGianHienTai = DateTime.Now;

            DateTime thoiDiemNgungDangKy = cakham.NgayKham.Date
                .Add(cakham.ThoiGianKetThuc)
                .AddMinutes(-soPhutNgungDangKyTruocKetThuc);

            return thoiGianHienTai <= thoiDiemNgungDangKy;
        }


        public async Task<IEnumerable<Request_HienThiCaKhamDTO>> GetCaKhamDaDangKyAsync()
        {
            return await _caKhamRepository.GetCaKhamDaDangKyAsync();
        }

        public async Task<IEnumerable<Request_CaKhamCoSLBNDaDangKiDTO>> HienThiDanhSachCaKhamPhiaAdmin()
        {
            // Lấy danh sách ca khám từ repository


            return await _caKhamRepository.GetCaKhamsWithTenBacSiAndTenNhomBenhAsync(); ;
        }

        public async Task<ResponeMessage> DangKyCaKhamChoBacSiAsync(Request_DangKyCaKhamChoBacSiDTO data, HttpContext httpContext)
        {
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                return new ResponeMessage(HttpStatusCode.Unauthorized, "");
            }
            var findCaKham = (await _caKhamRepository.FindAsync(c => c.Id == data.CaKhamId && c.BacSiId != null && c.NhomBenhId == nguoiDung.ChuyenMonId)).FirstOrDefault();
            if (findCaKham == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Ca khám đã có bác sĩ đăng kí hoặc không phù hợp chuyên môn của bạn");
            }
            findCaKham.BacSiId = nguoiDung.Id;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Đăng kí ca khám thành công");
        }
    }
}
