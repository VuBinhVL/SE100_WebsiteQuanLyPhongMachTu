﻿using Microsoft.AspNetCore.Http;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.CaKham;
using PhongMachTu.Common.DTOs.Request.HoSoBenhAn;
using PhongMachTu.Common.DTOs.Respone;
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
    public interface IHoSoBenhAnService
    {
        Task<List<HoSoBenhAn>> GetAllAsync();
        Task<ResponeMessage> HienThiHoSoBenhAnAsync( HttpContext httpContext);
    }
    public class HoSoBenhAnService : IHoSoBenhAnService
    {
        private readonly IHoSoBenhAnRepository hoSoBenhAnRepository;
        private readonly INguoiDungService _nguoiDungService;
        public HoSoBenhAnService(IHoSoBenhAnRepository _hoSoBenhAnRepository, INguoiDungService nguoiDungService)
        {
            _hoSoBenhAnRepository = hoSoBenhAnRepository;
            _nguoiDungService = nguoiDungService;
        }
        public async Task<List<HoSoBenhAn>> GetAllAsync()
        {
            return (await hoSoBenhAnRepository.GetAllAsync()).ToList();
        }
        public async Task<ResponeMessage> HienThiHoSoBenhAnAsync(HttpContext httpContext)
        {
            var nguoiDung = await _nguoiDungService.GetNguoiDungByHttpContext(httpContext);
            if (nguoiDung == null)
            {
                return new ResponeMessage(HttpStatusCode.Unauthorized, "");
            }
            var findBenhNhan = (await hoSoBenhAnRepository.GetAllAsync()).Where(d => d.BenhNhanId == nguoiDung.Id).FirstOrDefault();
            var rs = new Request_HienThiHoSoBenhAnDTO()
            {
                Id = findBenhNhan.Id,
                NgayTao = findBenhNhan.NgayTao
            };
            // Chuyển đổi đối tượng thành JSON
            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(rs);
            return new ResponeMessage(HttpStatusCode.Ok, responseJson);
        }

        }
}
