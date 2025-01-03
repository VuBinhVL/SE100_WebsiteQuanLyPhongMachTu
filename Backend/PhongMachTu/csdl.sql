USE [PhongMachTuDB]
GO
SET IDENTITY_INSERT [dbo].[ChucNangs] ON 

INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (1, N'Xem danh sách nhân viên')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (2, N'Chỉnh sửa thông tin nhân viên')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (3, N'Xóa nhân viên')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (4, N'Thêm nhân viên')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (5, N'Xem báo cáo, thông kê')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (6, N'Xem danh sách thuốc')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (7, N'Xóa thuốc')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (8, N'Chỉnh sửa thông tin thuốc')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (9, N'Nhập thuốc')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (10, N'Xem danh sách ca khám')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (11, N'Tạo ca khám')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (12, N'Chỉnh sửa ca khám')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (13, N'Hủy ca khám')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (14, N'Hủy lịch khám')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (15, N'Sửa lịch khám')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (16, N'Xem phiếu khám bệnh')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (17, N'Sửa phiếu khám bệnh')
INSERT [dbo].[ChucNangs] ([Id], [TenChucNang]) VALUES (18, N'Xóa phiếu khám bệnh')
SET IDENTITY_INSERT [dbo].[ChucNangs] OFF
GO
SET IDENTITY_INSERT [dbo].[NhomBenhs] ON 

INSERT [dbo].[NhomBenhs] ([Id], [TenNhomBenh]) VALUES (1, N'Tim mạch')
SET IDENTITY_INSERT [dbo].[NhomBenhs] OFF
GO
SET IDENTITY_INSERT [dbo].[VaiTros] ON 

INSERT [dbo].[VaiTros] ([Id], [TenVaiTro], [ChucNangIdsDefault]) VALUES (1, N'Chủ Phòng Mạch', N'')
INSERT [dbo].[VaiTros] ([Id], [TenVaiTro], [ChucNangIdsDefault]) VALUES (2, N'Nhân Viên', N'')
INSERT [dbo].[VaiTros] ([Id], [TenVaiTro], [ChucNangIdsDefault]) VALUES (3, N'Bệnh Nhân', N'')
SET IDENTITY_INSERT [dbo].[VaiTros] OFF
GO
SET IDENTITY_INSERT [dbo].[NguoiDungs] ON 

INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (1, N'nguyenvana', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn A', N'nguyenvana@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456780', NULL, 3, NULL)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (2, N'nguyenvanb', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn B', N'nguyenvanb@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456789', NULL, 3, NULL)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (3, N'nguyenvanc', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn C', N'nguyenvanc@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456787', NULL, 3, NULL)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (4, N'nguyenvand', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn D', N'nguyenvand@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456788', NULL, 3, NULL)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (5, N'["0989444999","tranthia@gmail.com"]', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Trần Văn A', N'tranthia@gmail.com', NULL, N'Nam', 0, N'0989444999', NULL, 2, 1)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (6, N'["0989444998","tranvanb@gmail.com"]', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Trần Văn B', N'tranvanb@gmail.com', NULL, N'Nam', 0, N'0989444998', NULL, 2, 1)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (7, N'["0989444997","tranthic@gmail.com"]', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Trần Thị C', N'tranthic@gmail.com', NULL, N'Nữ', 0, N'0989444997', NULL, 2, 1)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (8, N'["0989444996","tranthid@gmail.com"]', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Trần Thị D', N'tranthid@gmail.com', NULL, N'Nữ', 0, N'0989444996', NULL, 2, 1)
INSERT [dbo].[NguoiDungs] ([Id], [TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId]) VALUES (11, N'admin', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Thế Dũng', N'dungtienthe1920@gmail.com', N'Thái Bình', N'Nam', 0, N'0397487360', CAST(N'2003-04-30T00:00:00.0000000' AS DateTime2), 1, 1)
SET IDENTITY_INSERT [dbo].[NguoiDungs] OFF
GO
INSERT [dbo].[SuChoPheps] ([ChucNangId], [NguoiDungId]) VALUES (1, 5)
INSERT [dbo].[SuChoPheps] ([ChucNangId], [NguoiDungId]) VALUES (2, 5)
GO
SET IDENTITY_INSERT [dbo].[ThamSos] ON 

INSERT [dbo].[ThamSos] ([Id], [SoLanHuyLichKhamToiDaChoPhep], [HeSoBan],SoPhutNgungDangKyTruocKetThuc) VALUES (1, 5, 1.2,30)
SET IDENTITY_INSERT [dbo].[ThamSos] OFF
GO


  INSERT INTO TrangThaiLichKhams(TenTrangThai)
  VALUES
  (N'Đang chờ'),
  (N'Đang khám'),
  (N'Hoàn tất'),
  (N'Đã hủy');


 INSERT INTO dbo.CaKhams (TenCaKham, ThoiGianBatDau, ThoiGianKetThuc, NgayKham, SoLuongBenhNhanToiDa, BacSiId)
VALUES
(N'Ca Sáng', '07:00:00', '11:00:00', '2024-12-30', 2, 5),
(N'Ca Chiều', '13:00:00', '17:00:00', '2024-12-30', 2, 6),
(N'Ca Tối', '18:00:00', '22:00:00', '2024-12-30', 2, NULL);
