using PhongMachTu.Common.DTOs.Request.Thuoc;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.DataAccess.Infrastructure;

namespace PhongMachTu.Service
{
  
        public interface IThuocService
        {
            Task<List<Thuoc>> GetAllAsync();
            Task<Thuoc> GetByIdAsync(int id);
            Task<ResponeMessage> UpdateThuoc(Request_UpdateThuocDTO? request);
        Task<ResponeMessage> HienThiDanhSachThuoc();
        }

        public class ThuocService : IThuocService
        {
            private readonly IThuocRepository _thuocRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILoaiThuocRepository _loaiThuocRepository;
            public ThuocService(IThuocRepository thuocRepository, IUnitOfWork unitOfWork, ILoaiThuocRepository loaiThuocRepository)
            {
                _thuocRepository = thuocRepository;
                _unitOfWork = unitOfWork;
                _loaiThuocRepository = loaiThuocRepository;
            }

           

            public async Task<List<Thuoc>> GetAllAsync()
            {
                return (await _thuocRepository.GetAllAsync()).ToList();
            }

            public async Task<Thuoc> GetByIdAsync(int id)
            {
                return await _thuocRepository.GetSingleByIdAsync(id);
            }    

            public async Task<ResponeMessage> UpdateThuoc(Request_UpdateThuocDTO? request)
                {
                    if (request == null)
                    {
                        return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
                    }

                    var findThuoc = await _thuocRepository.GetSingleByIdAsync(request.Id);
                    if (findThuoc == null)
                    {
                        return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy thuốc");
                    }

                    if(request.NgaySanXuat > request.HanSuDung)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Ngày sản xuất không thể lớn hơn hạn sử dụng");
                }
                if (request.SoLuongTon < 0)
                {
                    return new ResponeMessage(HttpStatusCode.BadRequest, "Số lượng tồn không thể nhỏ hơn 0");
                }
                    var findLoaiThuoc = await _loaiThuocRepository.GetSingleByIdAsync(request.LoaiThuocId);
                if (findLoaiThuoc == null)
                {
                    return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy loại thuốc");
                }

                findThuoc.TenThuoc = request.TenThuoc;
                    findThuoc.Images = request.Images;
                    findThuoc.SoLuongTon = request.SoLuongTon;
                    findThuoc.NgaySanXuat = request.NgaySanXuat;
                    findThuoc.HanSuDung = request.HanSuDung;
                    findThuoc.LoaiThuocId = request.LoaiThuocId;

                    await _unitOfWork.CommitAsync();

                    return new ResponeMessage(HttpStatusCode.Ok, "Sửa thông tin thuốc thành công");
                }

        public async Task<ResponeMessage> HienThiDanhSachThuoc()
        {
            var thuocs = await _thuocRepository.GetAllAsync();
        
            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(thuocs);
            return new ResponeMessage(HttpStatusCode.Ok, responseJson);
        }
    }
    }


