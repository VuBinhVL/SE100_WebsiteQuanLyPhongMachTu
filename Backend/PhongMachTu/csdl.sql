USE [PhongMachTuDB]
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
