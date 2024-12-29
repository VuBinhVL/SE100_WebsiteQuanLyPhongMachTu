using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhongMachTu.Common.ConstValue;
using PhongMachTu.Common.Helpers;
using PhongMachTu.Common.MyMiddlewares;
using PhongMachTu.DataAccess;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.Service;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Add DbContext
builder.Services.AddDbContext<PhongMachTuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnectString")));

// Đăng ký DbFactory và UnitOfWork
builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Đăng ký Repository
builder.Services.AddScoped<IBenhLyRepository, BenhLyRepository>();
builder.Services.AddScoped<ICaKhamRepository, CaKhamRepository>();
builder.Services.AddScoped<IChiTietDonThuocRepository, ChiTietDonThuocRepository>();
builder.Services.AddScoped<IChiTietHoSoBenhAnRepository, ChiTietHoSoBenhAnRepository>();
builder.Services.AddScoped<IChiTietKhamBenhRepository, ChiTietKhamBenhRepository>();
builder.Services.AddScoped<IChiTietPhieuNhapThuocRepository, ChiTietPhieuNhapThuocRepository>();
builder.Services.AddScoped<IChiTietXetNghiemRepository, ChiTietXetNghiemRepository>();
builder.Services.AddScoped<IChucNangRepository, ChucNangRepository>();
builder.Services.AddScoped<IChupChieuRepository, ChupChieuRepository>();
builder.Services.AddScoped<IDonViTinhRepository, DonViTinhRepository>();
builder.Services.AddScoped<IHoSoBenhAnRepository, HoSoBenhAnRepository>();
builder.Services.AddScoped<ILichKhamRepository, LichKhamRepository>();
builder.Services.AddScoped<ILoaiThuocRepository, LoaiThuocRepository>();
builder.Services.AddScoped<ILoaiXetNghiemRepository, LoaiXetNghiemRepository>();
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<INhomBenhRepository, NhomBenhRepository>();
builder.Services.AddScoped<IPhieuKhamBenhRepository, PhieuKhamBenhRepository>();
builder.Services.AddScoped<IPhieuNhapThuocRepository, PhieuNhapThuocRepository>();
builder.Services.AddScoped<ISuChoPhepRepository, SuChoPhepRepository>();
builder.Services.AddScoped<IThuocRepository, ThuocRepository>();
builder.Services.AddScoped<ITrangThaiLichKhamRepository, TrangThaiLichKhamRepository>();
builder.Services.AddScoped<IVaiTroRepository, VaiTroRepository>();
builder.Services.AddScoped<IThamSoRepository, ThamSoRepository>();

//Đăng ký service
builder.Services.AddScoped<IDonViTinhService, DonViTinhService>();
builder.Services.AddScoped<INhomBenhService, NhomBenhService>();
builder.Services.AddScoped<IBenhLyService, BenhLyService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<IBenhNhanService, BenhNhanService>();
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();
builder.Services.AddScoped<ILoaiThuocService, LoaiThuocService>();
builder.Services.AddScoped<IPhieuNhapThuocService, PhieuNhapThuocService>();
builder.Services.AddScoped<ICaKhamService, CaKhamService>();

//bổ trợ phần token
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<TokenStore>();


//add jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Yêu cầu Kiểm tra Issuer
        ValidateAudience = false, // Không cần Kiểm tra Audience
        ValidateLifetime = true, // Yêu cầu Kiểm tra thời hạn của token
        ClockSkew = TimeSpan.Zero, // Loại bỏ thời gian lệch,check thời hạn thêm chính xác
        ValidateIssuerSigningKey = true, // Yêu cầu Kiểm tra Signature
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Cấu hình Issuer
       // ValidAudience = builder.Configuration["Jwt:Audience"], // Cấu hình Audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) ,
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                //context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

//add AddPolicy cho phân quyền
builder.Services.AddAuthorization(options =>
{
    //chỉnh sửa thông tin nhân viên
    options.AddPolicy(Const_ChucNang.Quan_Ly_Nhan_Vien_Edit, policy =>
    {
        policy.RequireAssertion(context =>
        {
            var role = context.User.FindFirst("VaiTro")?.Value;

            if(role== Const_VaiTro.Chu_Phong_Mach)
            {
                return true;
            }

            var permissions = context.User.Claims
                .Where(c => c.Type == "SuChoPhep")
                .Select(c => c.Value);
            return role == Const_VaiTro.Nhan_Vien
                   && permissions.Contains(Const_ChucNang.Quan_Ly_Nhan_Vien_Edit);
        });
    });

    //xóa nhân viên
    options.AddPolicy(Const_ChucNang.Quan_Ly_Nhan_Vien_Delete, policy =>
    {
        policy.RequireAssertion(context =>
        {
            var role = context.User.FindFirst("VaiTro")?.Value;
            if (role == Const_VaiTro.Chu_Phong_Mach)
            {
                return true;
            }

            var permissions = context.User.Claims
                .Where(c => c.Type == "SuChoPhep")
                .Select(c => c.Value);
            return  role == Const_VaiTro.Nhan_Vien
                   && permissions.Contains(Const_ChucNang.Quan_Ly_Nhan_Vien_Delete);
        });
    });

    //nao viết tiếp hihi
});


// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            // Tùy chỉnh JSON trả về
            //var response = new
            //{
            //    message="valid-by-modelstate",
            //    errors 
            //};

            var response = new
            {
                message = errors.Any() ? errors.First().Value[0] : "Có lỗi xảy ra"
            };

            return new BadRequestObjectResult(response);
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin() // Cho phép bất kỳ nguồn gốc nào
               .AllowAnyHeader() // Cho phép bất kỳ header nào
               .AllowAnyMethod(); // Cho phép bất kỳ phương thức HTTP nào
    });
});




var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //hỗ trợ chạy be,fe trên local được đồng thời
    app.UseCors(); // Áp dụng CORS
    //hỗ trợ chạy be,fe trên local được đồng thời
}

app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthentication();
app.UseMiddleware<TokenValidationMiddleware>();//phải dùng cái này ở dưới authen vì authen có nhiệm vụ handle token
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    // Đăng ký cho API Controllers
    endpoints.MapControllers();

    // Đăng ký cho area CUSTOMER
    endpoints.MapAreaControllerRoute(
        name: "customer_area",
        areaName: "CUSTOMER",
        pattern: "CUSTOMER/{controller=Home}/{action=Index}/{id?}"
    );

    // Đăng ký cho area ADMIN
    endpoints.MapAreaControllerRoute(
        name: "admin_area",
        areaName: "ADMIN",
        pattern: "ADMIN/{controller=Home}/{action=Index}/{id?}"
    );

    // Route mặc định
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
