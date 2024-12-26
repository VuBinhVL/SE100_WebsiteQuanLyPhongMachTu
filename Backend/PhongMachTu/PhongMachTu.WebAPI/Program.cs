﻿using Microsoft.EntityFrameworkCore;
using PhongMachTu.DataAccess;
using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Repositories;
using PhongMachTu.Service;

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

//Đăng ký service
builder.Services.AddScoped<IDonViTinhService, DonViTinhService>();
builder.Services.AddScoped<INhomBenhService, NhomBenhService>();
builder.Services.AddScoped<IBenhLyService, BenhLyService>();
builder.Services.AddScoped<ILoaiThuocService, LoaiThuocService>();
builder.Services.AddScoped<IPhieuNhapThuocService, PhieuNhapThuocService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

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
