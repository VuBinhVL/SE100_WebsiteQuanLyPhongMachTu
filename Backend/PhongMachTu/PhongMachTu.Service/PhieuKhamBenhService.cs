using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.PhieuKhamBenh;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.DTOs.Respone.PhieuKhamBenh;
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
    public interface IPhieuKhamBenhService
    {
        Task<IEnumerable<PhieuKhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync();
        Task<ResponeMessage> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data);
        Task<Respone_PhieukhamBenhDTO> DetailPhieuKhamBenhAsync(int id);
    }
    public class PhieuKhamBenhService : IPhieuKhamBenhService
    {
        private readonly IPhieuKhamBenhRepository _phieuKhamBenhRepository;
        private readonly ILichKhamRepository _lichKhamRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IChiTietDonThuocRepository _chiTietDonThuocRepository;
        private readonly ICaKhamRepository _caKhamRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IChupChieuRepository _chupChieuRepository;
        private readonly IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PhieuKhamBenhService(IPhieuKhamBenhRepository phieuKhamBenhRepository, ILichKhamRepository lichKhamRepository, INguoiDungRepository nguoiDungRepository, IChiTietDonThuocRepository chiTietDonThuocRepository, ICaKhamRepository caKhamRepository,IChiTietKhamBenhRepository chiTietKhamBenhRepository,IChupChieuRepository chupChieuRepository,IChiTietXetNghiemRepository chiTietXetNghiemRepository, IUnitOfWork unitOfWork)
        {
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
            _lichKhamRepository = lichKhamRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
            _caKhamRepository = caKhamRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _chupChieuRepository = chupChieuRepository;
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data)
        {
            var findLichKham = await _lichKhamRepository.GetSingleByIdAsync(data.LichKhamId);
            if (findLichKham == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy lịch khám đã chọn");
            }

            var findPhieuKhamBenh = (await _phieuKhamBenhRepository.FindAsync(p => p.LichKhamId == data.LichKhamId)).FirstOrDefault();
            if (findPhieuKhamBenh != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Phiếu khám bệnh cho lịch khám này đã tồn tại");
            }

            var pkb = new PhieuKhamBenh
            {
                NgayTao = DateTime.Now,
                LichKhamId = data.LichKhamId,
            };

            await _phieuKhamBenhRepository.AddAsync(pkb);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Tạo phiếu khám bệnh thành công");
        }

        public async Task<IEnumerable<PhieuKhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync()
        {
            var listPhieuKhamBenh = await _phieuKhamBenhRepository.GetListPhieuKhamBenhDTOsAsync();

            return listPhieuKhamBenh;
        }

        public async Task<Respone_PhieukhamBenhDTO> DetailPhieuKhamBenhAsync(int id)
        {
            var findPKB = await _phieuKhamBenhRepository.GetSingleByIdAsync(id);
            if (findPKB == null)
            {
                return null;
            }
            var findLichKham = await _lichKhamRepository.GetSingleByIdAsync(findPKB.LichKhamId);
            if (findLichKham == null)
            {
                return null;
            }
            var findCaKham = await _caKhamRepository.GetSingleByIdAsync(findLichKham.CaKhamId);
            if (findCaKham == null)
            {
                return null;
            }
            var findBenhNhan = await _nguoiDungRepository.GetSingleByIdAsync(findLichKham.BenhNhanId);
            if (findBenhNhan == null)
            {
                return null;
            }
            var findBacSi = await _nguoiDungRepository.GetSingleByIdAsync(findCaKham.BacSiId ?? -1);
            if (findBacSi == null)
            {
                return null;
            }

            var rsp = new Respone_PhieukhamBenhDTO();
            rsp.HinhAnhBenhNhan = findBenhNhan.Image;
            rsp.HoTenBenhNhan = findBenhNhan.HoTen;
            rsp.GioiTinh = findBenhNhan.GioiTinh;
            rsp.NgaySinh = findBenhNhan.NgaySinh;
            rsp.DiaChi = findBenhNhan.DiaChi;
            rsp.ThoiGianKham = findPKB.NgayTao;
            rsp.TenBacSiKham = findBacSi.HoTen;
            rsp.DaThanhToan = findLichKham.TrangThaiLichKhamId == Const_TrangThaiLichKham.Hoan_Tat;
            rsp.TienKham = 0;
            rsp.TienThuoc = 0;
            rsp.TienXetNghiem = 0;
            rsp.TienChupChieu = 0;

            var findChiTietKhamBenhs = await _chiTietKhamBenhRepository.FindWithIncludeAsync(c => c.PhieuKhamBenhId == id, c => c.BenhLy);

            foreach(var ctkb in findChiTietKhamBenhs)
            {
                rsp.ChiTietKhamBenhs.Add(new Respone_PhieukhamBenhDTO.Respone_ChiTietKhamBenhDTO()
                {
                    Id = ctkb.Id,
                    TenBenhLy = ctkb.BenhLy.TenBenhLy,
                    GiaKham = ctkb.GiaKham,
                });

                rsp.TienKham += ctkb.GiaKham;

               var findChiTietDonThuocs = await _chiTietDonThuocRepository.FindWithIncludeAsync(c => c.ChiTietKhamBenhId == ctkb.Id,c=>c.Thuoc);
                foreach(var ctdt in findChiTietDonThuocs)
                {
                    rsp.TienThuoc += ctdt.DonGia;
                }

                var findChupchieus = await _chupChieuRepository.FindAsync(c => c.ChiTietKhamBenhId == ctkb.Id);
                foreach(var chupchieu in findChupchieus)
                {
                    rsp.TienChupChieu += chupchieu.Gia;
                }

                var findChiTietXetNghiems = await _chiTietXetNghiemRepository.FindAsync(c=>c.ChiTietKhamBenhId==ctkb.Id);
                foreach(var ctxn in findChiTietXetNghiems)
                {
                    rsp.TienXetNghiem += ctxn.GiaXetNghiem;
                }
            }
            return rsp;
        }

    }
}
