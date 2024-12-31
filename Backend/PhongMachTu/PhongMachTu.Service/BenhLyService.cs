﻿using PhongMachTu.Common.DTOs.Request.BenhLy;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.NhomBenh;
using Microsoft.EntityFrameworkCore;

namespace PhongMachTu.Service
{
    public interface IBenhLyService
    {
        Task<List<BenhLy>> GetAllAsync();   
        Task<ResponeMessage> AddBenhLy(Request_AddBenhLyDTO benhLy);    
        Task<BenhLy> GetByIdAsync(int id);
        Task<ResponeMessage> UpdateBenhLy(Request_UpdateBenhLyDTO? request);
        Task<ResponeMessage> DeleteBenhLy(int id);
        Task<ResponeMessage> HienThiBangGiaBenhLy();
    }
    public class BenhLyService : IBenhLyService
    {
        private readonly INhomBenhRepository _nhomBenhRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHoSoBenhAnRepository _hoSoBenhAnRepository;
        private readonly IBenhLyRepository _benhLyRepository;
        private readonly IChiTietHoSoBenhAnRepository _chiTietHoSoBenhAnRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;

        public BenhLyService(INhomBenhRepository nhomBenhRepository, IUnitOfWork unitOfWork, IHoSoBenhAnRepository hoSoBenhAnRepository, IBenhLyRepository benhLyRepository, IChiTietHoSoBenhAnRepository chiTietHoSoBenhAnRepository, IChiTietKhamBenhRepository chiTietKhamBenhRepository)
        {
            _nhomBenhRepository = nhomBenhRepository;
            _unitOfWork = unitOfWork;
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _benhLyRepository = benhLyRepository;
            _chiTietHoSoBenhAnRepository = chiTietHoSoBenhAnRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
        }
        public async Task<ResponeMessage> AddBenhLy(Request_AddBenhLyDTO benhly)
        {
            if (benhly == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findBenhLy = (await _benhLyRepository.GetAllAsync()).Where(d => d.TenBenhLy.Trim().ToLower() == benhly.TenBenhLy.Trim().ToLower()).FirstOrDefault();
            if (findBenhLy != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên bệnh lý này đã có rồi");
            }

            await _benhLyRepository.AddAsync(new BenhLy()
            {
                TenBenhLy = benhly.TenBenhLy,
                TrieuTrung = benhly.TrieuChung,
                GiaThamKhao = benhly.GiaThamKhao,
                Images = benhly.Images,
                NhomBenhId = benhly.NhomBenhId
            });
            await _unitOfWork.CommitAsync();

            return new ResponeMessage(HttpStatusCode.Ok, "Thêm bệnh lý thành công");
        }

        public async Task<List<BenhLy>> GetAllAsync()
        {
            return (await _benhLyRepository.GetAllAsync()).ToList();
        }

        public async Task<BenhLy> GetByIdAsync(int id)
        {
            if (id == -1)
            {
                return null;
            }
            return await _benhLyRepository.GetSingleByIdAsync(id);
        }

        public async Task<ResponeMessage> UpdateBenhLy(Request_UpdateBenhLyDTO? request)
        {
            if (request == null || request.Id == null || string.IsNullOrEmpty(request.TenBenhLy))
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }
            var findBenhLy = (await _benhLyRepository.GetAllAsync()).Where(d => d.TenBenhLy.Trim().ToLower() == request.TenBenhLy.Trim().ToLower()).FirstOrDefault();
            if (findBenhLy != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên bệnh lý này đã có rồi");
            }

            var findBenhLybyID = await _benhLyRepository.GetSingleByIdAsync(request.Id ?? -1);
            if (findBenhLybyID == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            findBenhLybyID.TenBenhLy = request.TenBenhLy;
            findBenhLybyID.TrieuTrung = request.TrieuChung;
            findBenhLybyID.GiaThamKhao = request.GiaThamKhao;
            findBenhLybyID.Images = request.Images;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Sửa tên bệnh lý thành công");
        }


        public async Task<ResponeMessage> DeleteBenhLy(int id)
        {
            var findBenhLy = await _benhLyRepository.GetSingleByIdAsync(id);
            if (findBenhLy == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Không tìm thấy bệnh lý cần xóa");
            }

           

            var findChiTietKhamBenhsByBenhLyId = (await _chiTietKhamBenhRepository.GetAllAsync()).Where(p => p.BenhLyId == id).FirstOrDefault();
            if (findChiTietKhamBenhsByBenhLyId != null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, $"Không thể xóa bệnh lý này vì có chi tiết khám bệnh ID: {findChiTietKhamBenhsByBenhLyId.Id} đang thuộc về");
            }
            


            _benhLyRepository.Delete(findBenhLy);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Xóa bệnh lý thành công");
        }

        public async Task<ResponeMessage> HienThiBangGiaBenhLy()
        {
             // Lấy danh sách bệnh lý, bao gồm thông tin nhóm bệnh
            var benhLys = await _benhLyRepository
                .Query() // Truy vấn từ repository
                .Include(bl => bl.NhomBenh) // Eager loading để lấy thông tin NhomBenh
                .ToListAsync();


            if (benhLys == null || !benhLys.Any())
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy thông tin bệnh lý.");
            }

            var bangGiaBenhLy = benhLys.Select(bl => new Request_HienThiBangGiaBenhLy
            {
                TenNhomBenh = bl.NhomBenh?.TenNhomBenh,
                TenBenhLy = bl.TenBenhLy,              
                GiaThamKhao = bl.GiaThamKhao           
            }).ToList();

            // Chuyển đổi kết quả sang JSON
            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(bangGiaBenhLy);

            return new ResponeMessage(HttpStatusCode.Ok, responseJson);
        }

    }
}
