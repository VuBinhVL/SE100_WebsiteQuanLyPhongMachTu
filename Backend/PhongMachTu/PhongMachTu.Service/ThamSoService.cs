using System;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Respone;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.Common.DTOs.Request.ThamSo;

namespace PhongMachTu.Service
{
    public interface IThamSoService
    {
        Task<ResponeMessage> UpdateThamSo(Request_UpdateThamSoDTO request);
        Task<List<ThamSo>> GetAllAsync();
        Task<ThamSo> GetByIdAsync(int id);

    }
    public class ThamSoService: IThamSoService
    {
        private readonly IThamSoRepository _thamSoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ThamSoService(IThamSoRepository thamSoRepository, IUnitOfWork unitOfWork)
        {
            _thamSoRepository = thamSoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ThamSo>> GetAllAsync()
        {
            return (await _thamSoRepository.GetAllAsync()).ToList();
        }

        public async Task<ThamSo> GetByIdAsync(int id)
        {
            return await _thamSoRepository.GetSingleByIdAsync(id);
        }

        public async Task<ResponeMessage> UpdateThamSo(Request_UpdateThamSoDTO request)
        {
            if (request == null)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
            }

            var findThamSo = await _thamSoRepository.GetSingleByIdAsync(1);
            
            if (request.HeSoBan <= 1)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Hệ số bán phải lớn hơn 1");
            }
            if (request.SoPhutNgungDangKyTruocKetThuc <= 0)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Số phút ngừng đăng ký trước kết thúc phải lớn hơn 0");
            }
            if (request.SoLanHuyLichKhamToiDaChoPhep <= 0)
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Số lần hủy lịch khám tối đa cho phép phải lớn hơn 0");
            }

            findThamSo.SoLanHuyLichKhamToiDaChoPhep = request.SoLanHuyLichKhamToiDaChoPhep;
            findThamSo.HeSoBan = request.HeSoBan;
            findThamSo.SoPhutNgungDangKyTruocKetThuc = request.SoPhutNgungDangKyTruocKetThuc;

            _thamSoRepository.Update(findThamSo);
            await _unitOfWork.CommitAsync();

            return new ResponeMessage(HttpStatusCode.Ok, "Cập nhật tham số thành công");
        }
    }
}
