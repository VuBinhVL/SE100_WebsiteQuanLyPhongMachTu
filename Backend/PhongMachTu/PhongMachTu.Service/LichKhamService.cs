using Org.BouncyCastle.Asn1.Ocsp;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.LichKhamAdmin;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface ILichKhamService
    {
        Task<IEnumerable<LichKhamDTO>> HienThiDanhSachLichKhamPhiaAdmin(int CaKhamId);
        Task<IEnumerable<TrangThaiLichKham>> GetTrangThaiLichKham();
        Task<Request_HienThiChiTietLichKhamDTO> HienThiChiTietLichKhamAsync(int lichKhamId);
    }
    public class LichKhamService : ILichKhamService
    {
        private readonly ILichKhamRepository _lichKhamRepository;
        private readonly ITrangThaiLichKhamRepository _trangThaiLichKhamRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly ICaKhamRepository _caKhamRepository;
        private readonly INhomBenhRepository _nhomBenhRepository;
        public LichKhamService(ILichKhamRepository lichKhamRepository, ITrangThaiLichKhamRepository trangThaiLichKhamRepository,
            INguoiDungRepository nguoiDungRepository, ICaKhamRepository caKhamRepository, INhomBenhRepository nhomBenhRepository)
        {
            _lichKhamRepository = lichKhamRepository;
            _trangThaiLichKhamRepository = trangThaiLichKhamRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _caKhamRepository = caKhamRepository;
            _nhomBenhRepository = nhomBenhRepository;
        }

        public async Task<IEnumerable<TrangThaiLichKham>> GetTrangThaiLichKham()
        {
            var listTrangThaiLichKham = await _trangThaiLichKhamRepository.GetAllAsync();
            return listTrangThaiLichKham;
        }

        public async Task<Request_HienThiChiTietLichKhamDTO> HienThiChiTietLichKhamAsync(int lichKhamId)
        {
            if(lichKhamId <= 0)
            {
                throw new Exception("ThuocId không hợp lệ");
            }
            var findLichKham = await _lichKhamRepository.GetSingleByIdAsync(lichKhamId);
            if (findLichKham == null)
            {
                throw new Exception("Không tìm thấy lịch khám");
            }
            var findBenhNhan = await _nguoiDungRepository.GetSingleByIdAsync(findLichKham.BenhNhanId);
            if (findBenhNhan == null)
            {
                throw new Exception("Không tìm thấy bệnh nhân");
            }
            var findCaKham = await _caKhamRepository.GetSingleByIdAsync(findLichKham.CaKhamId);
            if (findCaKham == null)
            {
                throw new Exception("Không tìm thấy ca khám");
            }
            var findBacSi = await _nguoiDungRepository.GetSingleByIdAsync(findCaKham.BacSiId??-1);
            if (findBacSi == null)
            {
                throw new Exception("Không tìm thấy bác sĩ");
            }
            var findNhomBenh = await _nhomBenhRepository.GetSingleByIdAsync(findCaKham.NhomBenhId);
            if (findNhomBenh == null)
            {
                throw new Exception("Không tìm thấy nhóm bệnh");
            }
            var findTrangThaiLichKham = await _trangThaiLichKhamRepository.GetSingleByIdAsync(findLichKham.TrangThaiLichKhamId);
            if (findTrangThaiLichKham == null)
            {
                throw new Exception("Không tìm thấy trạng thái lịch khám");
            }
            var rs = new Request_HienThiChiTietLichKhamDTO
            {
                TenBenhNhan = findBenhNhan.HoTen,
                GioiTinh = findBenhNhan.GioiTinh,
                SoDienThoai = findBenhNhan.SoDienThoai,
                DiaChi = findBenhNhan.DiaChi,
                NgayKham = findCaKham.NgayKham,
                TenCaKham = findCaKham.TenCaKham,
                STT = findLichKham.SoThuTu,
                ThoiGianBatDau = findCaKham.ThoiGianBatDau,
                ThoiGianKetThuc = findCaKham.ThoiGianKetThuc,
                TenNhomBenh = findNhomBenh.TenNhomBenh,
                TrangThai = findTrangThaiLichKham.TenTrangThai,
                TenBacSi = findBacSi.HoTen,
                TenChuyenMon = findNhomBenh.TenNhomBenh,
                NgaySinhBN = findBenhNhan.NgaySinh
            };

            return rs;
        }

        public async Task<IEnumerable<LichKhamDTO>> HienThiDanhSachLichKhamPhiaAdmin(int CaKhamId)
        {
            return await _lichKhamRepository.GetListLichKhamDTOsAsync(CaKhamId);

           

        }
    }
}
