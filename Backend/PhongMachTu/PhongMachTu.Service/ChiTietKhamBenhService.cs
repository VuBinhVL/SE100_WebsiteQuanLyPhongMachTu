using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChiTietKhamBenh;
using PhongMachTu.Common.DTOs.Request.ChupChieu;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.DTOs.Respone.ChiTietKhamBenh;
using PhongMachTu.Common.DTOs.Respone.ChupChieu;
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
    public interface IChiTietKhamBenhService
    {
        Task<Respone_ChiTietKhamBenhDTO> DetailChiTietKhamBenhAsync(int id);
        Task<ResponeMessage> DeleteChiTietKhamBenhAsync(int id);
        Task<Respone_AddOrUpdateChiTietKhamBenhDTO> AddOrUpdateChiTietKhamBenhAsync(Request_AddOrUpdateChiTietKhamBenhDTO data);
    }
    public class ChiTietKhamBenhService : IChiTietKhamBenhService
    {
        private readonly IChupChieuRepository _chupChieuRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private readonly IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        private readonly IChiTietDonThuocRepository _chiTietDonThuocRepository;
        private readonly IBenhLyRepository _benhLyRepository;
        private readonly ILichKhamRepository _lichKhamRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChiTietKhamBenhService(IChupChieuRepository chupChieuRepository, IChiTietKhamBenhRepository chiTietKhamBenhRepository, IChiTietXetNghiemRepository chiTietXetNghiemRepository, IPhieuKhamBenhRepository phieuKhamBenhRepository, IChiTietDonThuocRepository chiTietDonThuocRepository, IBenhLyRepository benhLyRepository,ILichKhamRepository lichKhamRepository, IUnitOfWork unitOfWork)
        {
            _chupChieuRepository = chupChieuRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
            _benhLyRepository = benhLyRepository;
            _lichKhamRepository = lichKhamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Respone_AddOrUpdateChiTietKhamBenhDTO> AddOrUpdateChiTietKhamBenhAsync(Request_AddOrUpdateChiTietKhamBenhDTO data)
        {
            if (data.GiaKham < 0)
            {
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Giá phải >=0"),
                };
            }

            var findPKB = await _phieuKhamBenhRepository.GetSingleByIdAsync(data.PhieuKhamBenhId);
            if (findPKB == null)
            {
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Có lỗi xảy ra do không tìm thấy phiếu khám bệnh hiện tại"),
                };
            }

            var findLichKham = await _lichKhamRepository.GetSingleWithIncludesAsync(l=>l.Id==findPKB.LichKhamId,l=>l.CaKham);
            if (findLichKham == null)
            {
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Có lỗi xảy ra do không tìm thấy lịch khám của phiếu khám bệnh tương ứng"),
                };
            }
            var content = "Phiếu khám bệnh hiện tại đã bị hủy nên không thể chỉnh sửa";

            if (findLichKham.TrangThaiLichKhamId == Const_TrangThaiLichKham.Hoan_Tat)
            {
                content = "Phiếu khám bệnh hiện tại đã thanh toán nên không thể chỉnh sửa";
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, content),
                };
            }

            var findBenhLy = await _benhLyRepository.GetSingleByIdAsync(data.BenhLyId);
            if (findBenhLy == null)
            {
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Có lỗi xảy ra do không tìm thấy bệnh lý đã chọn"),
                };
            }

            if (findLichKham.CaKham.NhomBenhId != findBenhLy.Id)
            {
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Bệnh lý đã chọn không thuộc chuyên môn của bác sĩ đang khám"),
                };
            }


            if (data.Id == -1)//add
            {
                var ctNew = await _chiTietKhamBenhRepository.AddAsync(new ChiTietKhamBenh()
                {
                    PhieuKhamBenhId = data.PhieuKhamBenhId,
                    BenhLyId = data.BenhLyId,
                    GiaKham = data.GiaKham,
                    GhiChu = "Chưa có ghi chú"
                });
                await _unitOfWork.CommitAsync();
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.Ok, "Thêm kết quả khám thành công"),
                    IdAdd = ctNew.Id
                };
            }
            else//update
            {
                var findCTKB = await _chiTietKhamBenhRepository.GetSingleByIdAsync(data.Id);
                if (findCTKB == null)
                {
                    return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                    {
                        ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Có lỗi xảy ra do không tìm thấy chi tiết khám bệnh đã chọn"),
                    };
                }
                findCTKB.BenhLyId = data.BenhLyId;
                findCTKB.GiaKham = data.GiaKham;
                await _unitOfWork.CommitAsync();
                return new Respone_AddOrUpdateChiTietKhamBenhDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.Ok, "Cập nhật chi tiết khám bệnh thành công"),
                };
            }


        }

        public async Task<ResponeMessage> DeleteChiTietKhamBenhAsync(int id)
        {
            var findCTKB = (await _chiTietKhamBenhRepository.FindWithIncludeAsync(c => c.Id == id, c => c.PhieuKhamBenh, c => c.PhieuKhamBenh.LichKham)).FirstOrDefault();
            if (findCTKB == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy chi tiết khám bệnh đã chọn");
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
            var chupchieus = await _chupChieuRepository.FindAsync(c => c.ChiTietKhamBenhId == id);
            foreach (var item in chupchieus)
            {
                _chupChieuRepository.Delete(item);
            }
            var chitietxetnghiems = await _chiTietXetNghiemRepository.FindAsync(c => c.ChiTietKhamBenhId == id);
            foreach (var item in chitietxetnghiems)
            {
                _chiTietXetNghiemRepository.Delete(item);
            }

            var chitietdonthuocs = await _chiTietDonThuocRepository.FindAsync(c => c.ChiTietKhamBenhId == id);
            foreach (var item in chitietdonthuocs)
            {
                _chiTietDonThuocRepository.Delete(item);
            }
            _chiTietKhamBenhRepository.Delete(findCTKB);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa chi tiết phiếu khám thành công");

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
                    Images = chupchieu.Images,
                    KetLuan = chupchieu.KetLuan,
                    Gia = chupchieu.Gia
                });
            }

            var findChiTietXetNghiems = await _chiTietXetNghiemRepository.FindWithIncludeAsync(c => c.ChiTietKhamBenhId == id, c => c.LoaiXetNghiem, c => c.LoaiXetNghiem.DonViTinh);
            foreach (var cctn in findChiTietXetNghiems)
            {
                rsp.ChiTietXetNghiems.Add(new Respone_ChiTietKhamBenhDTO.Respone_ChiTietXetNghiem()
                {
                    ChiTietKhamBenhId = id,
                    LoaiXetNghiemId = cctn.LoaiXetNghiemId,
                    TenXetNghiem = cctn.LoaiXetNghiem.TenXetNghiem,
                    TenDonViTinh = cctn.LoaiXetNghiem.DonViTinh.TenDonViTinh,
                    KetQua = cctn.KetQua,
                    DanhGia = cctn.DanhGia,
                    GiaXetNghiem = cctn.GiaXetNghiem
                });
            }

            var findChiTietDonThuocs = await _chiTietDonThuocRepository.FindWithIncludeAsync(c => c.ChiTietKhamBenhId == id, c => c.Thuoc);
            foreach (var ctdt in findChiTietDonThuocs)
            {
                rsp.ChiTietDonThuocs.Add(new Respone_ChiTietKhamBenhDTO.Respone_ChiTietDonThuocDTO()
                {
                    ChiTietKhamBenhId = ctdt.ChiTietKhamBenhId,
                    ThuocId = ctdt.ThuocId,
                    TenThuoc = ctdt.Thuoc.TenThuoc,
                    SoLuong = ctdt.SoLuong,
                    DonGia = ctdt.DonGia
                });
            }
            return rsp;
        }
    }
}
