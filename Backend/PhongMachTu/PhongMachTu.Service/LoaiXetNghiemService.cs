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
    }

    public class LoaiXetNghiemService : ILoaiXetNghiemService
    {
        private readonly ILoaiXetNghiemRepository _loaiXetNghiemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoaiXetNghiemService(ILoaiXetNghiemRepository loaiXetNghiemRepository,IUnitOfWork unitOfWork)
        {
            _loaiXetNghiemRepository = loaiXetNghiemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddLoaiXetNghiemAsync(Request_AddLoaiXetNghiemDTO data)
        {

            var findDonViTinh = await _loaiXetNghiemRepository.GetSingleByIdAsync(data.DonViTinhId);
            if(findDonViTinh == null)
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

        public async Task<List<LoaiXetNghiem>> GetAllAsync()
        {
            var rs = await _loaiXetNghiemRepository.GetAllWithIncludeAsync(l=>l.DonViTinh);
            return rs.ToList();
        }

        public async Task<ResponeMessage> UpdateLoaiXetNghiemAsync(Request_UpdateLoaiXetNghiemDTO data)
        {
            var findDonViTinh = await _loaiXetNghiemRepository.GetSingleByIdAsync(data.DonViTinhId);
            if (findDonViTinh == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Đơn vị tính không hợp lệ");
            }

            findDonViTinh.TenXetNghiem = data.TenXetNghiem;
            findDonViTinh.DonViTinhId = data.DonViTinhId;
            findDonViTinh.GiaThamKhao=data.GiaThamKhao;

            _loaiXetNghiemRepository.Update(findDonViTinh);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Cập nhật thông tin loại xét nghiệm thành công");
        }
    }
}
