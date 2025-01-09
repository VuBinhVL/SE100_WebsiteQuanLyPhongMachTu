using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietDonThuoc;
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
    public interface IChiTietDonThuocService
    {
        Task<ResponeMessage> DeleteChiTietDonThuocAsync(int chiTietKhamBenhId, int thuocId);
        Task<Respone_AddOrUpdateChiTietDonThuocDTO> AddOrUpdateChiTietDonThuocAsync(Request_AddOrUpdateChiTietDonThuocDTO data);
    }
    public class ChiTietDonThuocService : IChiTietDonThuocService
    {
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IChiTietDonThuocRepository _chiTietDonThuocRepository;
        private readonly IThamSoRepository _thamSoRepository;
        private readonly IThuocRepository _thuocRepository;
        public readonly IUnitOfWork _unitOfWork;
        public ChiTietDonThuocService(IChiTietKhamBenhRepository chiTietKhamBenhRepository, IChiTietDonThuocRepository chiTietDonThuocRepository, IThamSoRepository thamSoRepository,IThuocRepository thuocRepository, IUnitOfWork unitOfWork)
        {
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
            _thamSoRepository = thamSoRepository;
            _thuocRepository = thuocRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponeMessage> DeleteChiTietDonThuocAsync(int chiTietKhamBenhId, int thuocId)
        {
            var findChiTietDonThuoc = (await _chiTietDonThuocRepository.FindAsync(t => t.ChiTietKhamBenhId == chiTietKhamBenhId && t.ThuocId == thuocId)).FirstOrDefault();
            if (findChiTietDonThuoc == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy chi tiết đơn thuốc đã chọn");
            }

            var findCTKB = await _chiTietKhamBenhRepository.GetSingleWithIncludesAsync(c => c.Id == chiTietKhamBenhId, c => c.PhieuKhamBenh, c => c.PhieuKhamBenh.LichKham);
            if (findCTKB.PhieuKhamBenh.LichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Kham)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Chỉ có thể xóa khi trạng thái lịch khám là đang khám");
            }

            _chiTietDonThuocRepository.Delete(findChiTietDonThuoc);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa chi tiết đơn thuốc thành công");
        }

        public async Task<Respone_AddOrUpdateChiTietDonThuocDTO> AddOrUpdateChiTietDonThuocAsync(Request_AddOrUpdateChiTietDonThuocDTO data)
        {
            if (data.SoLuong <= 0)
            {
                return new Respone_AddOrUpdateChiTietDonThuocDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest,"Số lượng phải lớn hơn 0")
                }; 
            }

            var findChiTietKhamBenh = await _chiTietKhamBenhRepository.GetSingleWithIncludesAsync(c => c.Id == data.ChiTietKhamBenhId, c => c.PhieuKhamBenh, c => c.PhieuKhamBenh.LichKham);
            if (findChiTietKhamBenh == null)
            {
                return new Respone_AddOrUpdateChiTietDonThuocDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy chi tiết khám bệnh hiện tại")
                };
            }


            if(findChiTietKhamBenh.PhieuKhamBenh.LichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Kham)
            {
                return new Respone_AddOrUpdateChiTietDonThuocDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Chỉ có thể chỉnh sửa đơn thuốc khi phiếu khám bệnh có trạng thái là đang khám")
                };
            }

            var findThamSo = await _thamSoRepository.GetSingleByIdAsync(1);
            if (findThamSo == null)
            {
                return new Respone_AddOrUpdateChiTietDonThuocDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy dữ liệu tham số của hệ thống,vui lòng báo tới quản trị viên")
                };
            }

            var findThuoc = await _thuocRepository.GetSingleByIdAsync(data.ThuocId);
            if(findThuoc == null)
            {
                return new Respone_AddOrUpdateChiTietDonThuocDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy thuốc đã chọn")
                };
            }
            if (findThuoc.SoLuongTon < data.SoLuong)
            {
                return new Respone_AddOrUpdateChiTietDonThuocDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy thuốc đã chọn")
                };
            }

         

            var content = "Thêm chi tiết đơn thuốc thành công";

            var findChiTietDonThuoc = (await _chiTietDonThuocRepository.FindAsync(c=>c.ChiTietKhamBenhId==data.ChiTietKhamBenhId && c.ThuocId==data.ThuocId)).FirstOrDefault();
            if (findChiTietDonThuoc == null)//insert
            {
                findChiTietDonThuoc = await _chiTietDonThuocRepository.AddAsync(new ChiTietDonThuoc(){
                    ChiTietKhamBenhId= data.ChiTietKhamBenhId,
                    ThuocId = data.ThuocId,
                    SoLuong=data.SoLuong,
                    DonGia = (int)(findThuoc.GiaNhap*findThamSo.HeSoBan),
                    GhiChu="Không có ghi chú"
                });
            }
            else//update
            {
                findChiTietDonThuoc.SoLuong = data.SoLuong;
                findChiTietDonThuoc.DonGia = (int)(findThuoc.GiaNhap * findThamSo.HeSoBan);
                content = "Sửa chi tiết đơn thuốc thành công";
            }
            await _unitOfWork.CommitAsync();
            return new Respone_AddOrUpdateChiTietDonThuocDTO()
            {
                ResponeMessage = new ResponeMessage(HttpStatusCode.Ok, content),
                ChiTietKhamBenhId = findChiTietDonThuoc.ChiTietKhamBenhId,
                ThuocId = findChiTietDonThuoc.ThuocId,
                SoLuong = findChiTietDonThuoc.SoLuong,
                DonGia = findChiTietDonThuoc.DonGia
            };
        }
    }
}
