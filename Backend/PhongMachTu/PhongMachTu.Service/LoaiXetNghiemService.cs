using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.LoaiXetNghiem;
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
    public interface ILoaiXetNghiemService
    {
        Task<List<LoaiXetNghiem>> GetAllAsync();
        Task<ResponeMessage> AddLoaiXetNghiemAsync(Request_AddLoaiXetNghiemDTO data);
        Task<ResponeMessage> UpdateLoaiXetNghiemAsync(Request_UpdateLoaiXetNghiemDTO data);
        Task<ResponeMessage> DeleteLoaiXetNghiemByIdAsync(int? id);

    }

    public class LoaiXetNghiemService : ILoaiXetNghiemService
    {
        private readonly ILoaiXetNghiemRepository _loaiXetNghiemRepository;
        private readonly IDonViTinhRepository _donViTinhRepository;
        private readonly IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoaiXetNghiemService(ILoaiXetNghiemRepository loaiXetNghiemRepository,IDonViTinhRepository donViTinhRepository, IChiTietXetNghiemRepository chiTietXetNghiemRepository, IUnitOfWork unitOfWork)
        {
            _loaiXetNghiemRepository = loaiXetNghiemRepository;
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _donViTinhRepository=donViTinhRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddLoaiXetNghiemAsync(Request_AddLoaiXetNghiemDTO data)
        {

            var findDonViTinh = await _donViTinhRepository.GetSingleByIdAsync(data.DonViTinhId);
            if (findDonViTinh == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Đơn vị tính không hợp lệ");
            }

            await _loaiXetNghiemRepository.AddAsync(new LoaiXetNghiem()
            {
                TenXetNghiem=data.TenXetNghiem,
                GiaThamKhao=data.GiaThamKhao,
                DonViTinhId=data.DonViTinhId,
            });
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Thêm loại xét nghiệm thành công");
        }

        public async Task<ResponeMessage> DeleteLoaiXetNghiemByIdAsync(int? id)
        {
            var findLoaiXetNghiem = await _loaiXetNghiemRepository.GetSingleByIdAsync(id??-1);
            if (findLoaiXetNghiem == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy loại xét nghiệm");
            }

            var chiTietXetNghiems = await _chiTietXetNghiemRepository.FindAsync(c=>c.LoaiXetNghiemId==id);
            if (chiTietXetNghiems.Any())
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không thể xóa do có phiếu khám bệnh đã dùng loại xét nghiệm này");
            }

            _loaiXetNghiemRepository.Delete(findLoaiXetNghiem);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa loại xét nghiệm thành công");
        }

        public async Task<List<LoaiXetNghiem>> GetAllAsync()
        {
            var rs = await _loaiXetNghiemRepository.GetAllWithIncludeAsync(l=>l.DonViTinh);
            return rs.ToList();
        }

        public async Task<ResponeMessage> UpdateLoaiXetNghiemAsync(Request_UpdateLoaiXetNghiemDTO data)
        {
            var findDonViTinh = await _donViTinhRepository.GetSingleByIdAsync(data.DonViTinhId);
            if (findDonViTinh == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Đơn vị tính không hợp lệ");
            }

            var findLoaiXetNghiem = await _loaiXetNghiemRepository.GetSingleByIdAsync(data.Id);
            if (findLoaiXetNghiem == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy loại xét nghiệm");
            }
            findLoaiXetNghiem.TenXetNghiem = data.TenXetNghiem;
            findLoaiXetNghiem.DonViTinhId = data.DonViTinhId;
            findLoaiXetNghiem.GiaThamKhao=data.GiaThamKhao;

            _loaiXetNghiemRepository.Update(findLoaiXetNghiem);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Cập nhật thông tin loại xét nghiệm thành công");
        }
    }
}
