using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.DTOs.Request.NguoiDung;
using PhongMachTu.Common.DTOs.Respone;
using PhongMachTu.Common.DTOs.Respone.NguoiDung;
using PhongMachTu.Common.Helpers;
using PhongMachTu.Common.Security;
using PhongMachTu.DataAccess.Models;
using PhongMachTu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Service
{
    public interface INguoiDungService
    {
        Task<Respone_Login> LoginAsync(Request_LoginDTO data);
    }

    public class NguoiDungService : INguoiDungService
    {

        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IVaiTroRepository _vaiTroRepository;
        private readonly ISuChoPhepRepository _suChoPhepRepository;
        private readonly IConfiguration _configuration;
        private readonly TokenStore _tokenStore;

        public NguoiDungService(INguoiDungRepository nguoiDungRepository,IConfiguration configuration,IVaiTroRepository vaiTroRepository, ISuChoPhepRepository suChoPhepRepository, TokenStore tokenStore)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _configuration = configuration;
            _vaiTroRepository = vaiTroRepository;
            _suChoPhepRepository = suChoPhepRepository;
            _tokenStore = tokenStore;
        }

        public async Task<Respone_Login> LoginAsync(Request_LoginDTO data)
        {
            var nguoidungs = await _nguoiDungRepository.GetAllAsync();
            var nguoiDung = nguoidungs.Where(u => ParseHelpers.ParseTaiKhoan(u.TenTaiKhoan).Contains(data.TenTaiKhoan)).FirstOrDefault();
            if (nguoiDung == null || nguoiDung.MatKhau != EncryptionHelper.Encrypt(data.MatKhau))
            {
                return new Respone_Login()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Tên tài khoản hoặc mật khẩu không chính xác"
                };
            }

            nguoiDung.VaiTro = await _vaiTroRepository.GetSingleByIdAsync(nguoiDung.VaiTroId);
            nguoiDung.SuChoPheps = await _suChoPhepRepository.FindWithIncludeAsync(s=>s.NguoiDungId==nguoiDung.Id, c=>c.ChucNang);
            var token = GenerateJwtToken(nguoiDung);    
            _tokenStore.AddToken(nguoiDung.Id,token);
            string roleName = "";
            if (nguoiDung.VaiTroId == 1)
            {
                roleName = Const_VaiTro.Chu_Phong_Mach;
            }
            else if (nguoiDung.VaiTroId == 2)
            {
                roleName = Const_VaiTro.Nhan_Vien;
            }
            else
            {
                roleName = Const_VaiTro.Benh_Nhan;
            }

            return new Respone_Login()
            {
                HttpStatusCode = HttpStatusCode.Ok,
                Token = token,
                Message = roleName
            };
        }


        private string GenerateJwtToken(NguoiDung nguoiDung)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var key = _configuration["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Tạo danh sách claims cơ bản
            var claims = new List<Claim>
            {
            new Claim("UserId", nguoiDung.Id.ToString()), // Lưu UserId vào claims
            new Claim("VaiTro", nguoiDung.VaiTro?.TenVaiTro ?? "Unknown"), // Lưu vai trò
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Thêm danh sách tên chức năng vào claims
            if (nguoiDung.SuChoPheps != null)
            {
                var permissions = nguoiDung.SuChoPheps
                    .Where(s => s.ChucNang != null)
                    .Select(s => s.ChucNang!.TenChucNang)
                    .ToList();

                foreach (var permission in permissions)
                {
                    claims.Add(new Claim("SuChoPhep", permission));
                }
            }

            // Tạo JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
