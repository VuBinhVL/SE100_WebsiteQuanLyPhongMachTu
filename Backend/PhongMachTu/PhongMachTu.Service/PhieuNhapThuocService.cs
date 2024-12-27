using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuNhapThuoc;
using PhongMachTu.Common.DTOs.Request.PhieuNhapThuoc;
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
    public interface IPhieuNhapThuocService
    {
        Task<List<PhieuNhapThuoc>> GetAllAsync();
        Task<ResponeMessage> AddPhieuNhapThuoc(Request_AddPhieuNhapThuocDTO phieuNhapThuoc);
        Task<PhieuNhapThuoc> GetByIdAsync(int id);
        Task<ResponeMessage> UpdatePhieuNhapThuoc(Request_UpdatePhieuNhapThuocDTO? request);
        Task<ResponeMessage> DeletePhieuNhapThuoc(int id);
    }
    public class PhieuNhapThuocService : IPhieuNhapThuocService
    {
        private readonly IPhieuNhapThuocRepository _phieuNhapThuocRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IThuocRepository _thuocRepository;
        private readonly ILoaiThuocRepository _loaiThuocRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IChiTietPhieuNhapThuocRepository _chiTietPhieuNhapThuocRepository;

        public PhieuNhapThuocService(IPhieuNhapThuocRepository phieuNhapThuocRepository, IUnitOfWork unitOfWork, IThuocRepository thuocRepository,
            INguoiDungRepository nguoiDungRepository,IChiTietPhieuNhapThuocRepository chiTietPhieuNhapThuocRepository, ILoaiThuocRepository loaiThuocRepository)
        {
            _phieuNhapThuocRepository = phieuNhapThuocRepository;
            _unitOfWork = unitOfWork;
            _thuocRepository = thuocRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _chiTietPhieuNhapThuocRepository = chiTietPhieuNhapThuocRepository;
            _loaiThuocRepository = loaiThuocRepository;
        }
        public async Task<ResponeMessage> AddPhieuNhapThuoc(Request_AddPhieuNhapThuocDTO phieuNhapThuoc)
        {
            if (phieuNhapThuoc == null || string.IsNullOrEmpty(phieuNhapThuoc.TenThuoc) || phieuNhapThuoc.LoaiThuocId == null || phieuNhapThuoc.HanSuDung  == null ||
              phieuNhapThuoc.SoLuong == null || phieuNhapThuoc.DonGia == null || phieuNhapThuoc.NgayNhap == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findThuocCu = (await _thuocRepository.GetAllAsync())
                .Where(d => d.TenThuoc.Trim().ToLower() == phieuNhapThuoc.TenThuoc.Trim().ToLower())
                .FirstOrDefault();

            if (findThuocCu != null && findThuocCu.HanSuDung == phieuNhapThuoc.HanSuDung && findThuocCu.GiaNhap == phieuNhapThuoc.DonGia)
            {
                // Tạo và lưu PhieuNhapThuoc
                if (phieuNhapThuoc.NgayNhap <= DateTime.Now)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Ngày nhập phải lớn hơn thời điểm nhập");
                }
                var newPhieuNhapThuoc = new PhieuNhapThuoc
                {
                    NgayNhap = phieuNhapThuoc.NgayNhap
                };
                await _phieuNhapThuocRepository.AddAsync(newPhieuNhapThuoc);
                await _unitOfWork.CommitAsync();

                // Tạo ChiTietPhieuNhapThuoc liên kết với PhieuNhapThuoc vừa thêm
                if(phieuNhapThuoc.SoLuong <= 0)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Số lượng nhập phải lớn hơn 0");
                }
                else if (phieuNhapThuoc.DonGia <= 0)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Đơn giá phải lớn hơn 0");
                }

                var chiTiet = new ChiTietPhieuNhapThuoc
                {
                    PhieuNhapThuocId = newPhieuNhapThuoc.Id,
                    ThuocId = findThuocCu.Id,
                    SoLuong = phieuNhapThuoc.SoLuong,
                    DonGia = findThuocCu.GiaNhap
                };
                await _chiTietPhieuNhapThuocRepository.AddAsync(chiTiet);

                // Cập nhật số lượng tồn
                findThuocCu.SoLuongTon += phieuNhapThuoc.SoLuong;
                _thuocRepository.Update(findThuocCu);
            }
            else
            {
                // Tạo mới Thuoc
                if (phieuNhapThuoc.HanSuDung <= DateTime.Now)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Hạn sử dụng phải lớn hơn thời điểm nhập");
                }
                else if (phieuNhapThuoc.SoLuong <= 0)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Số lượng nhập phải lớn hơn 0");
                }
                else if (phieuNhapThuoc.DonGia <= 0)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Đơn giá phải lớn hơn 0");
                }
                var findloaiThuocID = (await _loaiThuocRepository.GetAllAsync()).Where(p => p.Id == phieuNhapThuoc.LoaiThuocId).FirstOrDefault();
                if(findloaiThuocID == null)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Loại thuốc không tồn tại");
                }
                var newThuoc = new Thuoc
                {
                    TenThuoc = phieuNhapThuoc.TenThuoc,
                    HanSuDung = phieuNhapThuoc.HanSuDung,
                    Images = phieuNhapThuoc.Images,
                    SoLuongTon = phieuNhapThuoc.SoLuong,
                    GiaNhap = phieuNhapThuoc.DonGia,
                    GiaBan = 0, // Đặt giá bán = 0, nếu có bảng tham số sửa sau
                    LoaiThuocId = phieuNhapThuoc.LoaiThuocId
                };
                await _thuocRepository.AddAsync(newThuoc);
                await _unitOfWork.CommitAsync();

                // Tạo và lưu PhieuNhapThuoc
                if (phieuNhapThuoc.NgayNhap <= DateTime.Now)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Ngày nhập phải lớn hơn thời điểm nhập");
                }
                var newPhieuNhapThuoc = new PhieuNhapThuoc
                {
                    NgayNhap = phieuNhapThuoc.NgayNhap
                };
                await _phieuNhapThuocRepository.AddAsync(newPhieuNhapThuoc);
                await _unitOfWork.CommitAsync();

                // Tạo ChiTietPhieuNhapThuoc liên kết với cả Thuoc và PhieuNhapThuoc
                var chiTiet = new ChiTietPhieuNhapThuoc
                {
                    PhieuNhapThuocId = newPhieuNhapThuoc.Id,
                    ThuocId = newThuoc.Id,
                    SoLuong = phieuNhapThuoc.SoLuong,
                    DonGia = phieuNhapThuoc.DonGia
                };
                await _chiTietPhieuNhapThuocRepository.AddAsync(chiTiet);
            }

            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Thêm Thuốc thành công");
        }



        public async Task<List<PhieuNhapThuoc>> GetAllAsync()
        {
            return (await _phieuNhapThuocRepository.GetAllAsync()).ToList();
        }

        public async Task<PhieuNhapThuoc> GetByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            return await _phieuNhapThuocRepository.GetSingleByIdAsync(id);
        }

        public async Task<ResponeMessage> UpdatePhieuNhapThuoc(Request_UpdatePhieuNhapThuocDTO? request)
        {
            if (request == null || request.Id == null || request.NgayNhap == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

           

            var findPhieuNhapThuocbyId = await _phieuNhapThuocRepository.GetSingleByIdAsync(request.Id ?? -1);
            if (findPhieuNhapThuocbyId == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            findPhieuNhapThuocbyId.NgayNhap = request.NgayNhap;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa phiếu nhập thuốc thành công");
        }

        public async Task<ResponeMessage> DeletePhieuNhapThuoc(int id)
        {
            var findPhieuNhapThuoc = await _phieuNhapThuocRepository.GetSingleByIdAsync(id);
            if (findPhieuNhapThuoc == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy phiếu nhập thuốc cần xóa");
            }

            var findChiTietPhieuNhapThuocsByPhieuNhapThuocId = (await _chiTietPhieuNhapThuocRepository.GetAllAsync()).Where(p => p.PhieuNhapThuocId == id).FirstOrDefault();
            if (findChiTietPhieuNhapThuocsByPhieuNhapThuocId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa phiếu nhập thuốc này vì có chi tiết phiếu nhập thuộc có ID {findChiTietPhieuNhapThuocsByPhieuNhapThuocId.PhieuNhapThuocId} đang thuộc về");
            }

            

            
            _phieuNhapThuocRepository.Delete(findPhieuNhapThuoc);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa phiếu nhập thuốc thành công");
        }
    }
}

