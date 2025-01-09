using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.ChupChieu;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.DTOs.Respone.ChupChieu;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface IChupChieuService
    {
        Task<ResponeMessage> DeleteChupChieuByIdAsync(int id);
        Task<ResponeMessage> UpdateChupChieuByIdAsync(Request_UpdateChupChieuDTO data);
        Task<Respone_AddChupChieuDTO> AddChupChieuAsync(Request_AddChupChieuDTO data);
    }
    public class ChupChieuService : IChupChieuService
    {
        private readonly IChupChieuRepository _chupChieuRepository;
        private readonly IChiTietKhamBenhRepository _chiTietKhamBenhRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChupChieuService(IChupChieuRepository chupChieuRepository,IChiTietKhamBenhRepository chiTietKhamBenhRepository, IUnitOfWork unitOfWork)
        {
            _chupChieuRepository = chupChieuRepository;
            _chiTietKhamBenhRepository = chiTietKhamBenhRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Respone_AddChupChieuDTO> AddChupChieuAsync(Request_AddChupChieuDTO data)
        {
            if (data.Gia < 0)
            {
                return new Respone_AddChupChieuDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest, "Giá phải >=0"),
                };
            }

            var findCTKB = await _chiTietKhamBenhRepository.GetSingleByIdAsync(data.ChiTietKhamBenhId);

            if (findCTKB == null)
            {
                return new Respone_AddChupChieuDTO()
                {
                    ResponeMessage = new ResponeMessage(HttpStatusCode.BadRequest,"Không tim thấy chi tiết khám bệnh"),
                };
            }

            var chupchieuNew= await _chupChieuRepository.AddAsync(new ChupChieu()
            {
                Images= data.Images,
                KetLuan=data.KetLuan,
                Gia=data.Gia,
                ChiTietKhamBenhId=data.ChiTietKhamBenhId
            });

            await _unitOfWork.CommitAsync();

            return new Respone_AddChupChieuDTO()
            {
                ResponeMessage = new ResponeMessage(HttpStatusCode.Ok, "Thêm kết quả chụp chiếu thành công"),
                IdAdd=chupchieuNew.Id
            };
        }

        public async Task<ResponeMessage> DeleteChupChieuByIdAsync(int id)
        {
            var findChupChieu = await _chupChieuRepository.GetSingleByIdAsync(id);
            if (findChupChieu == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound,"Không tìm thấy kết quả chụp chiếu đã chọn");
            }

            _chupChieuRepository.Delete(findChupChieu);
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok,"Xóa kết quả chụp chiếu thành công");
        }

        public async Task<ResponeMessage> UpdateChupChieuByIdAsync(Request_UpdateChupChieuDTO data)
        {
            var findChupChieu = await _chupChieuRepository.GetSingleByIdAsync(data.Id);
            if (findChupChieu == null)
            {
                return new ResponeMessage(HttpStatusCode.NotFound, "Không tìm thấy kết quả chụp chiếu đã chọn");
            }
            if (data.Gia < 0)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Giá phải >=0");
            }

            if (!string.IsNullOrEmpty(data.Image))
            {
                findChupChieu.Images = data.Image;
            }
            findChupChieu.KetLuan = data.KetLuan;
            findChupChieu.Gia = data.Gia;
            await _unitOfWork.CommitAsync();
            return new ResponeMessage(HttpStatusCode.Ok, "Cập nhật kết quả chụp chiếu thành công");
        }
    }
}
