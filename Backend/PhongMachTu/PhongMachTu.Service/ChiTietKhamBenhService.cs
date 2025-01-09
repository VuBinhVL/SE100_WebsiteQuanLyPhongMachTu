using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChupChieu;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.DTOs.Respone.ChiTietKhamBenh;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface IChiTietKhamBenhService
    {
        Task<Respone_ChiTietKhamBenhDTO> DetailChiTietKhamBenhAsync(int id);
        Task<ResponeMessage> DeleteChiTietKhamBenhAsync(int id);
    }
    public class ChiTietKhamBenhService : IChiTietKhamBenhService
    {
        private IChupChieuRepository _chupChieuRepository;
        private IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        private IChiTietDonThuocRepository _chiTietDonThuocRepository;
        private IUnitOfWork _unitOfWork;
        public ChiTietKhamBenhService(IChupChieuRepository chupChieuRepository,IChiTietKhamBenhRepository chiTietKhamBenhRepository,IChiTietXetNghiemRepository chiTietXetNghiemRepository,IPhieuKhamBenhRepository phieuKhamBenhRepository,IChiTietDonThuocRepository chiTietDonThuocRepository,IUnitOfWork unitOfWork)
        {
            _chupChieuRepository = chupChieuRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> DeleteChiTietKhamBenhAsync(int id)
        {
            var findCTKB = (await _chiTietKhamBenhRepository.FindWithIncludeAsync(c=>c.Id==id, c=>c.PhieuKhamBenh ,c=>c.PhieuKhamBenh.LichKham)).FirstOrDefault();
            if (findCTKB == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound,"Không tìm thấy chi tiết khám bệnh đã chọn");
            }

            if (findCTKB.PhieuKhamBenh.LichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Kham)
            {
                var content = "Phiếu khám bệnh cho chi tiết khám bệnh này đã bị hủy nên không thể sửa đổi";
                if (findCTKB.PhieuKhamBenh.LichKham.TrangThaiLichKhamId == Const_TrangThaiLichKham.Hoan_Tat)
                {
                    content = "Phiếu khám bệnh cho chi tiết khám bệnh này đã thanh toán nên không thể sửa đổi";
                }
                return new ResponeMessage(HttpStatusCode.BadRequest, content);
            }
            //xóa chụp chiếu , kết quả xét nghiệm,đơn thuốc
            //không cần xóa chi tiết hồ sơ bệnh án vì nghiệp vụ hồ sơ bệnh án tạo khi hóa đơn hoàn tất
            var chupchieus = await _chupChieuRepository.FindAsync(c=>c.ChiTietKhamBenhId==id);
            foreach(var item in chupchieus)
            {
                _chupChieuRepository.Delete(item);
            }
            var chitietxetnghiems = await _chiTietXetNghiemRepository.FindAsync(c => c.ChiTietKhamBenhId == id);
            foreach(var item in chitietxetnghiems)
            {
                _chiTietXetNghiemRepository.Delete(item);
            }

            var chitietdonthuocs = await _chiTietDonThuocRepository.FindAsync(c => c.ChiTietKhamBenhId == id);
            foreach(var item in chitietdonthuocs)
            {
                _chiTietDonThuocRepository.Delete(item);
            }
            _chiTietKhamBenhRepository.Delete(findCTKB);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok,"Xóa chi tiết phiếu khám thành công");

        }

        public async Task<Respone_ChiTietKhamBenhDTO> DetailChiTietKhamBenhAsync(int id)
        {
            var findChiTietKhamBenh = await _chiTietKhamBenhRepository.GetSingleByIdAsync(id);
            if (findChiTietKhamBenh == null)
            {
                return null;
            }

            var rsp = new Respone_ChiTietKhamBenhDTO();
            rsp.GhiChu = findChiTietKhamBenh.GhiChu;

            var findChupChieus = await _chupChieuRepository.FindAsync(c => c.ChiTietKhamBenhId == id);
            foreach (var chupchieu in findChupChieus)
            {
                rsp.ChupChieus.Add(new Respone_ChiTietKhamBenhDTO.Respone_ChupChieuDTO()
                {
                    Id = chupchieu.Id,
                    Images=chupchieu.Images,
                    KetLuan=chupchieu.KetLuan,
                    Gia=chupchieu.Gia
                });
            }

            var findChiTietXetNghiems = await _chiTietXetNghiemRepository.FindWithIncludeAsync(c => c.ChiTietKhamBenhId == id,c=>c.LoaiXetNghiem, c => c.LoaiXetNghiem.DonViTinh);
            foreach(var cctn in findChiTietXetNghiems)
            {
                rsp.ChiTietXetNghiems.Add(new Respone_ChiTietKhamBenhDTO.Respone_ChiTietXetNghiem()
                {
                   ChiTietKhamBenhId=id,
                   LoaiXetNghiemId=cctn.LoaiXetNghiemId,
                   TenXetNghiem = cctn.LoaiXetNghiem.TenXetNghiem,
                   TenDonViTinh=cctn.LoaiXetNghiem.DonViTinh.TenDonViTinh,
                   KetQua=cctn.KetQua,
                   DanhGia=cctn.DanhGia,
                   GiaXetNghiem=cctn.GiaXetNghiem
                });
            }

            return rsp;
        }
    }
}
