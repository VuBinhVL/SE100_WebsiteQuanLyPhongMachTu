using PhongMachTu.Common.DTOs.Request.NhomBenh;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using PhongMachTu.Common.ConstValue;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhongMachTu.Common.DTOs.Request.DonViTinh;

namespace PhongMachTu.Service
{
    public interface INhomBenhService
    {
        Task<List<NhomBenh>> GetAllAsync();
        Task<ResponeMessage> AddNhomBenh(Request_AddNhomBenhDTO nhomBenh);
        Task<NhomBenh> GetByIdAsync(int id);
        Task<ResponeMessage> UpdateNhomBenh(Request_UpdateNhomBenhDTO? request);
        Task<ResponeMessage> DeleteNhomBenh(int id);
    }
    public class NhomBenhService : INhomBenhService
    {

        private readonly INhomBenhRepository _nhomBenhRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHoSoBenhAnRepository _hoSoBenhAnRepository;
        private readonly IBenhLyRepository _benhLyRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;

        public NhomBenhService(INhomBenhRepository nhomBenhRepository, IUnitOfWork unitOfWork, IHoSoBenhAnRepository hoSoBenhAnRepository, IBenhLyRepository benhLyRepository, INguoiDungRepository nguoiDungRepository)
        {
            _nhomBenhRepository = nhomBenhRepository;
            _unitOfWork = unitOfWork;
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _benhLyRepository = benhLyRepository;
            _nguoiDungRepository = nguoiDungRepository;
        }
        public async Task<ResponeMessage> AddNhomBenh(Request_AddNhomBenhDTO nhomBenh)
        {
            if (nhomBenh == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findNhomBenh = (await _nhomBenhRepository.GetAllAsync()).Where(d => d.TenNhomBenh.Trim().ToLower() == nhomBenh.TenNhomBenh.Trim().ToLower()).FirstOrDefault();
            if (findNhomBenh != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên nhóm bệnh này đã có rồi");
            }

            await _nhomBenhRepository.AddAsync(new NhomBenh()
            {
                TenNhomBenh = nhomBenh.TenNhomBenh
            });
            await _unitOfWork.CommitAsync();

            return new ResponeMessage(HttpStatusCode.Ok, "Thêm nhóm bệnh thành công");
        }

        public async Task<List<NhomBenh>> GetAllAsync()
        {
            return (await _nhomBenhRepository.GetAllAsync()).ToList();
        }

        public async Task<NhomBenh> GetByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            return await _nhomBenhRepository.GetSingleByIdAsync(id);
        }

        public async Task<ResponeMessage> UpdateNhomBenh(Request_UpdateNhomBenhDTO? request)
        {
            if (request == null || request.Id == null || string.IsNullOrEmpty(request.TenNhomBenh))
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findNhomBenh = (await _nhomBenhRepository.GetAllAsync()).Where(d => d.TenNhomBenh.Trim().ToLower() == request.TenNhomBenh.Trim().ToLower()).FirstOrDefault();
            if (findNhomBenh != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên nhóm bệnh này đã có rồi");
            }

            var findNhomBenhbyId = await _nhomBenhRepository.GetSingleByIdAsync(request.Id ?? -1);
            if (findNhomBenhbyId == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            findNhomBenhbyId.TenNhomBenh = request.TenNhomBenh;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa tên nhóm bệnh thành công");
        }

        public async Task<ResponeMessage> DeleteNhomBenh(int id)
        {
            var findNhomBenh = await _nhomBenhRepository.GetSingleByIdAsync(id);
            if (findNhomBenh == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy nhóm bệnh cần xóa");
            }

            var findHoSoBenhAnsByNhomBenhId = (await _hoSoBenhAnRepository.GetAllAsync()).Where(p => p.NhomBenhId == id).FirstOrDefault();
            if (findHoSoBenhAnsByNhomBenhId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa nhóm bệnh này vì có hồ sơ bệnh án có ID {findHoSoBenhAnsByNhomBenhId.BenhNhanId} đang thuộc về");
            }

            var findBenhLysByNhomBenhId = (await _benhLyRepository.GetAllAsync()).Where(p => p.NhomBenhId == id).FirstOrDefault();
            if (findBenhLysByNhomBenhId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa nhóm bệnh này vì có bệnh lý {findBenhLysByNhomBenhId.TenBenhLy} đang thuộc về");
            }

            var findNguoiDungsByNhomBenhId = (await _nguoiDungRepository.GetAllAsync()).Where(p => p.ChuyenMonId == id).FirstOrDefault();
            if (findNguoiDungsByNhomBenhId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa nhóm bệnh này vì có bác sĩ {findNguoiDungsByNhomBenhId.HoTen} có chuyên môn đang thuộc về");
            }
            _nhomBenhRepository.Delete(findNhomBenh);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa nhóm bệnh thành công");
        }
    }
    

}
