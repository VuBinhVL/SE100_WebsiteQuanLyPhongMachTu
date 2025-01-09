using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietXetNghiem;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.DTOs.Respone.ChiTietXetNghiem;
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
    public interface IChiTietXetNghiemService
    {
        Task<ResponeMessage> DeleteChiTietXetNghiemAsync(int chiTietKhamBenhId, int loaiXetNghiemId);
        Task<Respone_AddOrUpdateChiTietXetNghiemDTO> AddOrUpdateChiTietXetNghiemAsync(Request_AddOrUpdateChiTietXetNghiemDTO data);
    }
    public class ChiTietXetNghiemService : IChiTietXetNghiemService
    {
        private readonly IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IDonViTinhRepository _donViTinhRepository;
        private readonly ILoaiXetNghiemRepository _loaiXetNghiemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChiTietXetNghiemService(IChiTietXetNghiemRepository chiTietXetNghiemRepository,IChiTietKhamBenhRepository chiTietKhamBenhRepository ,IDonViTinhRepository donViTinhRepository,ILoaiXetNghiemRepository loaiXetNghiemRepository,IUnitOfWork unitOfWork)
        {
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _donViTinhRepository = donViTinhRepository;
            _loaiXetNghiemRepository = loaiXetNghiemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Respone_AddOrUpdateChiTietXetNghiemDTO> AddOrUpdateChiTietXetNghiemAsync(Request_AddOrUpdateChiTietXetNghiemDTO data)
        {
            var findChiTietKhamBenh = await _chiTietKhamBenhRepository.GetSingleWithIncludesAsync(c => c.Id == data.ChiTietKhamBenhId, c => c.PhieuKhamBenh, c => c.PhieuKhamBenh.LichKham);
            if (findChiTietKhamBenh == null)
            {
                return new Respone_AddOrUpdateChiTietXetNghiemDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy chi tiết khám bệnh hiện tại")
                };
            }

            if (findChiTietKhamBenh.PhieuKhamBenh.LichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Kham)
            {
                return new Respone_AddOrUpdateChiTietXetNghiemDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Chỉ có thể chỉnh sửa khi phiếu khám bệnh có trạng thái là đang khám")
                };
            }

        
            var findLoaiXetNghiem = await _loaiXetNghiemRepository.GetSingleByIdAsync(data.LoaiXetNghiemId);
            if (findLoaiXetNghiem == null)
            {
                return new Respone_AddOrUpdateChiTietXetNghiemDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy loại xét nghiệm đã chọn")
                };
            }


            var content = "Thêm chi tiết xét nghiệm thành công";

            var findChiTietXetNghiem = (await _chiTietXetNghiemRepository.FindAsync(c => c.ChiTietKhamBenhId == data.ChiTietKhamBenhId && c.LoaiXetNghiemId == data.LoaiXetNghiemId)).FirstOrDefault();
            if (findChiTietXetNghiem == null)//insert
            {
                findChiTietXetNghiem = await _chiTietXetNghiemRepository.AddAsync(new ChiTietXetNghiem()
                {
                    ChiTietKhamBenhId = data.ChiTietKhamBenhId,
                    LoaiXetNghiemId = data.LoaiXetNghiemId,
                    KetQua = data.KetQua,
                    DanhGia = data.DanhGia,
                    GiaXetNghiem = data.GiaXetNghiem
                });
            }
            else//update
            {
                findChiTietXetNghiem.KetQua = data.KetQua;
                content = "Sửa chi tiết xét nghiệm thành công";
            }
            findLoaiXetNghiem = await _loaiXetNghiemRepository.GetSingleByIdAsync(findChiTietXetNghiem.LoaiXetNghiemId);
            await _unitOfWork.CommitAsync();
            return new Respone_AddOrUpdateChiTietXetNghiemDTO()
            {
                ResponeMessage = new ResponeMessage(HttpStatusCode.Ok, content),
                ChiTietKhamBenhId = findChiTietXetNghiem.ChiTietKhamBenhId,
                LoaiXetNghiemId = findChiTietXetNghiem.LoaiXetNghiemId,
                DonViTinhId= findLoaiXetNghiem.DonViTinhId,
                KetQua = findChiTietXetNghiem.KetQua,
                DanhGia = findChiTietXetNghiem.DanhGia,
                GiaXetNghiem = findChiTietXetNghiem.GiaXetNghiem
            };
        }

        public async Task<ResponeMessage> DeleteChiTietXetNghiemAsync(int chiTietKhamBenhId, int loaiXetNghiemId)
        {
            var findCTXN = (await _chiTietXetNghiemRepository.FindAsync(c => c.ChiTietKhamBenhId == chiTietKhamBenhId && c.LoaiXetNghiemId == loaiXetNghiemId)).FirstOrDefault();
            if(findCTXN == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest,"Không tìm thấy chi tiết xét nghiệm đã chọn");
            }

            var findCTKB = (await _chiTietKhamBenhRepository.FindWithIncludeAsync(c => c.Id == chiTietKhamBenhId, c => c.PhieuKhamBenh, c => c.PhieuKhamBenh.LichKham)).FirstOrDefault();
            if (findCTKB == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy chi tiết khám bệnh của chi tiết xét nghiệm đã chọn");
            }

            if (findCTKB.PhieuKhamBenh.LichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Kham)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Trạng thái lịch khám phải là đang khám mới được phép xóa");
            }
            _chiTietXetNghiemRepository.Delete(findCTXN);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa chi tiết xét nghiệm thành công");
        }
    }
}
