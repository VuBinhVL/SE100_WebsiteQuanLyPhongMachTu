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
    }
    public class ChiTietKhamBenhService : IChiTietKhamBenhService
    {
        private IChupChieuRepository _chupChieuRepository;
        private IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private IChiTietXetNghiemRepository _chiTietXetNghiemRepository;
        private IUnitOfWork _unitOfWork;
        public ChiTietKhamBenhService(IChupChieuRepository chupChieuRepository,IChiTietKhamBenhRepository chiTietKhamBenhRepository,IChiTietXetNghiemRepository chiTietXetNghiemRepository,IUnitOfWork unitOfWork)
        {
            _chupChieuRepository = chupChieuRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _chiTietXetNghiemRepository = chiTietXetNghiemRepository;
            _unitOfWork = unitOfWork;
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
