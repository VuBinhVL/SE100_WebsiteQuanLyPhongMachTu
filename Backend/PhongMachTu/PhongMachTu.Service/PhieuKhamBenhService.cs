using Microsoft.AspNetCore.Http;
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
        Task<IEnumerable<Respone_PhieukhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync();
        Task<ResponeMessage> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data);
        Task<Respone_DetailPhieukhamBenhDTO> DetailPhieuKhamBenhAsync(int id);
        Task<IEnumerable<Respone_PhieukhamBenhDTO>> GetListPhieuKhamBenhsByUserCurAsync(HttpContext httpContext);

        Task<ResponeMessage> XacNhanThanhToanAsync(int id);
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
        private readonly INguoiDungService _nguoiDungService;
        private readonly IHoSoBenhAnRepository _hoSoBenhAnRepository;
        private readonly IChiTietHoSoBenhAnRepository _chiTietHoSoBenhAnRepository;
        private readonly IThamSoRepository _thamSoRepository;
        private readonly IThuocRepository _thuocRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PhieuKhamBenhService(IPhieuKhamBenhRepository phieuKhamBenhRepository, ILichKhamRepository lichKhamRepository, INguoiDungRepository nguoiDungRepository, IChiTietDonThuocRepository chiTietDonThuocRepository, ICaKhamRepository caKhamRepository, IChiTietKhamBenhRepository chiTietKhamBenhRepository, IChupChieuRepository chupChieuRepository, IChiTietXetNghiemRepository chiTietXetNghiemRepository, INguoiDungService nguoiDungService, IHoSoBenhAnRepository hoSoBenhAnRepository, IChiTietHoSoBenhAnRepository chiTietHoSoBenhAnRepository, IThamSoRepository thamSoRepository, IThuocRepository thuocRepository, IUnitOfWork unitOfWork)
        {
            _phieuKhamBenhRepository = phieuKhamBenhRepository;
            _lichKhamRepository = lichKhamRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _chiTietDonThuocRepository = chiTietDonThuocRepository;
            _caKhamRepository = caKhamRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _chupChieuRepository = chupChieuRepository;
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _nguoiDungService = nguoiDungService;
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _chiTietHoSoBenhAnRepository = chiTietHoSoBenhAnRepository;
            _thamSoRepository = thamSoRepository;
            _thuocRepository = thuocRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponeMessage> AddPhieuKhamBenhAsync(Request_AddPhieuKhamBenhDTO data)
        {
            var findLichKham = await _lichKhamRepository.GetSingleByIdAsync(data.LichKhamId);
            if (findLichKham == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy lịch khám đã chọn");
            }
            if (findLichKham.TrangThaiLichKhamId != Const_TrangThaiLichKham.Dang_Cho)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Phiếu khám bệnh cho lịch khám này đã tồn tại");
            }

            findLichKham.TrangThaiLichKhamId = Const_TrangThaiLichKham.Dang_Kham;
            var pkb = new PhieuKhamBenh
            {
                NgayTao = DateTime.Now,
                LichKhamId = data.LichKhamId,
            };

            await _phieuKhamBenhRepository.AddAsync(pkb);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Tạo phiếu khám bệnh thành công");
        }

        public async Task<IEnumerable<Respone_PhieukhamBenhDTO>> GetListPhieuKhamBenhDTOsAsync()
        {
            var findPKBs = await _phieuKhamBenhRepository.GetAllWithIncludeAsync(p => p.LichKham, p => p.LichKham.TrangThaiLichKham, p => p.LichKham.BenhNhan, p => p.LichKham.CaKham.BacSi);
            List<Respone_PhieukhamBenhDTO> list = new List<Respone_PhieukhamBenhDTO>();
            foreach (var item in findPKBs)
            {
                list.Add(new Respone_PhieukhamBenhDTO()
                {
                    Id = item.Id,
                    TenBenhNhan = item.LichKham.BenhNhan.HoTen,
                    TenBacSi = item.LichKham.CaKham.BacSi.HoTen,
                    NgayTao = item.NgayTao,
                    SoDienThoai = item.LichKham.BenhNhan.SoDienThoai,
                    TenTrangThaiPKB = item.LichKham.TrangThaiLichKham.TenTrangThai
                });
            }
            return list;
        }

        public async Task<Respone_DetailPhieukhamBenhDTO> DetailPhieuKhamBenhAsync(int id)
        {
            var findPKB = await _phieuKhamBenhRepository.GetSingleByIdAsync(id);
            if (findPKB == null)
            {
                return null;
            }
            var findLichKham = await _lichKhamRepository.GetSingleWithIncludesAsync(l => l.Id == findPKB.LichKhamId, l => l.TrangThaiLichKham);
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

            var rsp = new Respone_DetailPhieukhamBenhDTO();
            rsp.HinhAnhBenhNhan = findBenhNhan.Image;
            rsp.HoTenBenhNhan = findBenhNhan.HoTen;
            rsp.GioiTinh = findBenhNhan.GioiTinh;
            rsp.NgaySinh = findBenhNhan.NgaySinh;
            rsp.DiaChi = findBenhNhan.DiaChi;
            rsp.ThoiGianKham = findPKB.NgayTao;
            rsp.TenBacSiKham = findBacSi.HoTen;
            rsp.NhomBenhId = findBacSi.ChuyenMonId??-1;
            rsp.TenTrangThai = findLichKham.TrangThaiLichKham.TenTrangThai;
            rsp.TienKham = 0;
            rsp.TienThuoc = 0;
            rsp.TienXetNghiem = 0;
            rsp.TienChupChieu = 0;

            var findChiTietKhamBenhs = await _chiTietKhamBenhRepository.FindWithIncludeAsync(c => c.PhieuKhamBenhId == id, c => c.BenhLy);

            foreach (var ctkb in findChiTietKhamBenhs)
            {
                rsp.ChiTietKhamBenhs.Add(new Respone_DetailPhieukhamBenhDTO.Respone_ChiTietKhamBenhDTO()
                {
                    Id = ctkb.Id,
                    TenBenhLy = ctkb.BenhLy.TenBenhLy,
                    GiaKham = ctkb.GiaKham,
                });

                rsp.TienKham += ctkb.GiaKham;

                var findChiTietDonThuocs = await _chiTietDonThuocRepository.FindWithIncludeAsync(c => c.ChiTietKhamBenhId == ctkb.Id, c => c.Thuoc);
                foreach (var ctdt in findChiTietDonThuocs)
                {
                    rsp.TienThuoc += ctdt.DonGia;
                }

                var findChupchieus = await _chupChieuRepository.FindAsync(c => c.ChiTietKhamBenhId == ctkb.Id);
                foreach (var chupchieu in findChupchieus)
                {
                    rsp.TienChupChieu += chupchieu.Gia;
                }

                var findChiTietXetNghiems = await _chiTietXetNghiemRepository.FindAsync(c => c.ChiTietKhamBenhId == ctkb.Id);
                foreach (var ctxn in findChiTietXetNghiems)
                {
                    rsp.TienXetNghiem += ctxn.GiaXetNghiem;
                }
            }
            return rsp;
        }

        public async Task<IEnumerable<Respone_PhieukhamBenhDTO>> GetListPhieuKhamBenhsByUserCurAsync(HttpContext httpContext)
        {
            var findNguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (findNguoiDung == null)
            {
                return null;
            }
            var findPKBs = (await _phieuKhamBenhRepository.GetAllWithIncludeAsync(p => p.LichKham, p => p.LichKham.CaKham, p => p.LichKham.TrangThaiLichKham, p => p.LichKham.BenhNhan)).Where(p => p.LichKham.CaKham.BacSiId == findNguoiDung.Id);
            List<Respone_PhieukhamBenhDTO> list = new List<Respone_PhieukhamBenhDTO>();
            foreach (var item in findPKBs)
            {
                list.Add(new Respone_PhieukhamBenhDTO()
                {
                    Id = item.Id,
                    TenBenhNhan = item.LichKham.BenhNhan.HoTen,
                    NgayTao = item.NgayTao,
                    SoDienThoai = item.LichKham.BenhNhan.SoDienThoai,
                    TenTrangThaiPKB = item.LichKham.TrangThaiLichKham.TenTrangThai
                });
            }
            return list;

        }

        public async Task<ResponeMessage> XacNhanThanhToanAsync(int id)
        {
            var findPKB = await _phieuKhamBenhRepository.GetSingleWithIncludesAsync(p => p.Id == id, p => p.LichKham, p => p.LichKham.CaKham);
            if (findPKB == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy phiếu khám bệnh");
            }

            //check đã thanh toán hay hủy chưa
            if (findPKB.LichKham.TrangThaiLichKhamId == Const_TrangThaiLichKham.Da_Huy)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không thể thanh toán do phiếu khám bệnh đã bị hủy");
            }
            if (findPKB.LichKham.TrangThaiLichKhamId == Const_TrangThaiLichKham.Hoan_Tat)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Phiếu khám bệnh đã thanh toán trước đó rồi");
            }

            var findCTKBs = await _chiTietKhamBenhRepository.FindAsync(c => c.PhieuKhamBenhId == id);

            //xử lý tồn kho của thuốc
            foreach (var ctkb in findCTKBs)
            {
                var findCTDTs = await _chiTietDonThuocRepository.FindWithIncludeAsync(t => t.ChiTietKhamBenhId == ctkb.Id, t => t.Thuoc);
                foreach (var ctdt in findCTDTs)
                {
                    if (ctdt.SoLuong > ctdt.Thuoc.SoLuongTon)
                    {
                        return new ResponeMessage(HttpStatusCode.BadRequest, $"{ctdt.Thuoc.TenThuoc} chỉ còn {ctdt.Thuoc.SoLuongTon}");
                    }
                    ctdt.Thuoc.SoLuongTon -= ctdt.SoLuong;
                }
            }

            //xử lý hồ sơ bệnh án.
            var findHoSoBenhAnByNhomBenh = (await _hoSoBenhAnRepository.FindAsync(h => h.NhomBenhId == findPKB.LichKham.CaKham.NhomBenhId)).FirstOrDefault();
            if (findHoSoBenhAnByNhomBenh == null)
            {
                findHoSoBenhAnByNhomBenh = await _hoSoBenhAnRepository.AddAsync(new HoSoBenhAn()
                {
                    NgayTao = findPKB.NgayTao,
                    BenhNhanId = findPKB.LichKham.BenhNhanId,
                    NhomBenhId = findPKB.LichKham.CaKham.NhomBenhId
                });
            }
            foreach (var ctkb in findCTKBs)
            {
                await _chiTietHoSoBenhAnRepository.AddAsync(new ChiTietHoSoBenhAn()
                {
                    HoSoBenhAn = findHoSoBenhAnByNhomBenh,
                    ChiTietKhamBenhId = ctkb.Id
                });
            }

            //chuyển trạng thái
            findPKB.LichKham.TrangThaiLichKhamId = Const_TrangThaiLichKham.Hoan_Tat;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok,"Thanh toán thành công");
        }
    }
}
