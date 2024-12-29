using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.NguoiDung;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.Helpers;
using PhongMachTu.Common.Security;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface INguoiDungService
    {
        Task<ResponeMessage> LoginAsync(Request_LoginDTO data);
    }

    public class NguoiDungService : INguoiDungService
    {

        private readonly INguoiDungRepository _nguoiDungRepository;
        public NguoiDungService(INguoiDungRepository nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
        }

        public async Task<ResponeMessage> LoginAsync(Request_LoginDTO data)
        {
            var nguoidungs = await _nguoiDungRepository.GetAllAsync();
            var nguoiDung = nguoidungs.Where(u => ParseHelpers.ParseTaiKhoan(u.TenTaiKhoan).Contains(data.TenTaiKhoan)).FirstOrDefault();
            if (nguoiDung == null || nguoiDung.MatKhau != EncryptionHelper.Encrypt(data.MatKhau))
            {
                return new ResponeMessage(HttpStatusCode.BadRequest, "Tên tài khoản hoặc mật khẩu không đúng");
            }
            string roleName = "";
            if (nguoiDung.VaiTroId == 1)
            {
                roleName = "Chủ Phòng Mạch";
            }
            else if (nguoiDung.VaiTroId == 2)
            {
                roleName = "Nhân Viên";
            }
            else
            {
                roleName = "Bệnh Nhân";
            }
            return new ResponeMessage(HttpStatusCode.Ok, roleName);
        }
    }
}
