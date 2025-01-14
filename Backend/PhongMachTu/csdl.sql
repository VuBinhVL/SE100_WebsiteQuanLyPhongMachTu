﻿use PhongMachTuDB
INSERT INTO NhomBenhs (TenNhomBenh)
VALUES
(N'Tim mạch'),
(N'Hô hấp'),
(N'Da liễu'),
(N'Tiêu hóa');

INSERT INTO BenhLys (TenBenhLy, TrieuTrung, GiaThamKhao, Images, NhomBenhId)
VALUES
-- Nhóm bệnh: Tim mạch
(N'Tăng huyết áp', N'Huyết áp cao, đau đầu, chóng mặt', 500000, NULL, 1),
(N'Suy tim', N'Khó thở, phù, mệt mỏi', 600000, NULL, 1),

-- Nhóm bệnh: Hô hấp
(N'Viêm phổi', N'Ho khan, sốt, đau ngực', 400000, NULL, 2),
(N'Hen suyễn', N'Khó thở, ho, khò khè', 450000, NULL, 2),

-- Nhóm bệnh: Da liễu
(N'Nấm da', N'Ngứa, nổi mẩn đỏ, bong tróc da', 300000, NULL, 3),
(N'Viêm da cơ địa', N'Ngứa, khô da, mẩn đỏ', 350000, NULL, 3),

-- Nhóm bệnh: Tiêu hóa
(N'Loét dạ dày', N'Đau bụng, ợ chua, buồn nôn', 550000, NULL, 4),
(N'Hội chứng ruột kích thích', N'Đau bụng, tiêu chảy, táo bón', 500000, NULL, 4);

INSERT [dbo].[ChucNangs] ([TenChucNang]) VALUES 
(N'Xem danh sách nhân viên'),
(N'Chỉnh sửa thông tin nhân viên'),
(N'Xóa nhân viên'),
(N'Thêm nhân viên'),
(N'Xem báo cáo, thông kê'),
(N'Xem danh sách thuốc'),
(N'Xóa thuốc'),
(N'Chỉnh sửa thông tin thuốc'),
(N'Nhập thuốc'),
(N'Xem danh sách ca khám'),
(N'Tạo ca khám'),
(N'Chỉnh sửa ca khám'),
(N'Hủy ca khám'),
(N'Hủy lịch khám'),
(N'Sửa lịch khám'),
(N'Xem phiếu khám bệnh'),
(N'Sửa phiếu khám bệnh'),
(N'Xóa phiếu khám bệnh');

INSERT [dbo].[VaiTros] ([TenVaiTro], [ChucNangIdsDefault]) VALUES 
(N'Chủ Phòng Mạch', N''),
(N'Nhân Viên', N''),
(N'Bệnh Nhân', N'');

INSERT [dbo].[NguoiDungs] ([TenTaiKhoan], [MatKhau], [Image], [HoTen], [Email], [DiaChi], [GioiTinh], [IsLock], [SoDienThoai], [NgaySinh], [VaiTroId], [ChuyenMonId])
VALUES 
( N'nguyenvana', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn A', N'nguyenvana@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456780', NULL, 3, NULL),
(N'nguyenvanb', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn B', N'nguyenvanb@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456789', NULL, 3, NULL),
( N'nguyenvanc', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn C', N'nguyenvanc@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456787', NULL, 3, NULL),
( N'nguyenvand', N'UumvFAvMWwf5sDFLFa7QBA==', N'no_img.png', N'Nguyễn Văn D', N'nguyenvand@gmail.com', N'Chưa cập nhật', N'Khác', 0, N'0123456788', NULL, 3, NULL),
(N'tranthia', N'UumvFAvMWwf5sDFLFa7QBA==', N'https://bna.1cdn.vn/2019/08/30/uploaded-hoangvinhbna-2019_08_30-_091426-1.jpg', N'Mai Kiều Diễm', N'tranthia@gmail.com', NULL, N'Nữ', 0, N'0989444999', NULL, 2, 1),
( N'tranvanb', N'UumvFAvMWwf5sDFLFa7QBA==', N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThiuYVZJYa7egYwbAzKv8Wk04cZbjsRWqJnQ&s', N'Võ Mạnh Duy', N'tranvanb@gmail.com', NULL, N'Nam', 0, N'0989444998', NULL, 2, 2),
(N'tranthic', N'UumvFAvMWwf5sDFLFa7QBA==', N'https://icdn.24h.com.vn/upload/4-2021/images/2021-10-28/1-1635354392-100-width650height755.jpg', N'Lý Mộ Uyển', N'tranthic@gmail.com', NULL, N'Nữ', 0, N'0989444997', NULL, 2, 3),
(N'tranthid', N'UumvFAvMWwf5sDFLFa7QBA==', N'https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEg7z8kaSXEVe79m7aagF6QHIFQWGJaZKXehirS7RDnlyV_u8sOaXuJcfYEsdc4ckHvNBgBCNP7XBoggMVqJHsgWj0YiOrQNOckMVdg7h95m9wLHLDI5cVNjAGW3nPUTAu9oz1dxPX0X9GkH565ZqwdpyRy-TBcE1OCRH8aWCPInTlwQ59VH8RgsXKDgEw/s500/ao-blouse-dieu-duong.jpg', N'Võ Thu Kiều', N'tranthid@gmail.com', NULL, N'Nữ', 0, N'0989444996', NULL, 2, 4),
(N'admin', N'UumvFAvMWwf5sDFLFa7QBA==', N'https://hthaostudio.com/wp-content/uploads/2022/03/Anh-bac-si-nam-7-min.jpg', N'Thế Dũng', N'dungtienthe1920@gmail.com', N'Thái Bình', N'Nam', 0, N'0397487360', CAST(N'2003-04-30T00:00:00.0000000' AS DateTime2), 1, 1),
( N'rumstar2004', N'UumvFAvMWwf5sDFLFa7QBA==', N'https://vwu.vn/documents/20182/3458479/28_Feb_2022_115842_GMTbsi_thuhien.jpg/c04e15ea-fbe4-415f-bacc-4e5d4cc0204d', N'Trần Thanh Trúc ', N'vubinh.2004.17.7@gmail.com', N'Đồng Nai', N'Nữ', 0, N'0393712562', CAST(N'2004-07-17T00:00:00.0000000' AS DateTime2), 3, NULL);

INSERT [dbo].[SuChoPheps] ([ChucNangId], [NguoiDungId]) VALUES 
(1, 5),
(2, 5);

INSERT [dbo].[ThamSos] ([SoLanHuyLichKhamToiDaChoPhep], [HeSoBan], [SoPhutNgungDangKyTruocKetThuc]) 
VALUES (5, 1.2, 30);

INSERT INTO TrangThaiLichKhams (TenTrangThai)
VALUES
(N'Đang chờ'),
(N'Đang khám'),
(N'Hoàn tất'),
(N'Đã hủy');

INSERT INTO CaKhams (TenCaKham, ThoiGianBatDau, ThoiGianKetThuc, NgayKham, SoLuongBenhNhanToiDa, BacSiId, NhomBenhId)
VALUES
-- Ngày 7/1/2025
(N'Ca Sáng', '08:00:00', '12:00:00', '2025-01-07', 10, 6, 1),
(N'Ca Chiều', '13:00:00', '17:00:00', '2025-01-07',15, 7, 2),

-- Ngày 8/1/2025
(N'Ca Sáng', '08:00:00', '12:00:00', '2025-01-08', 10, 8, 3),
(N'Ca Chiều', '13:00:00', '17:00:00', '2025-01-08', 15, 9, 4),

-- Ngày 9/1/2025
(N'Ca Sáng', '08:00:00', '12:00:00', '2025-01-09', 10, 6, 1),
(N'Ca Chiều', '13:00:00', '17:00:00', '2025-01-09', 15, 7, 2),

-- Ngày 10/1/2025
(N'Ca Sáng', '08:00:00', '12:00:00', '2025-01-10', 10, 8, 3),
(N'Ca Chiều', '13:00:00', '17:00:00', '2025-01-10', 15, 9, 4),

-- Ngày 11/1/2025
(N'Ca Sáng', '08:00:00', '12:00:00', '2025-01-11', 10, 6, 1),
(N'Ca Chiều', '13:00:00', '17:00:00', '2025-01-11', 15, 7, 2);

INSERT INTO LichKhams (SoThuTu, TrangThaiLichKhamId, CaKhamId, BenhNhanId)
VALUES
-- Lịch khám ngày 7/1/2025
(1, 1, 1, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Sáng ngày 7/1
(2, 1, 2, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Chiều ngày 7/1

-- Lịch khám ngày 8/1/2025
(1, 1, 3, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Sáng ngày 8/1
(2, 1, 4, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Chiều ngày 8/1

-- Lịch khám ngày 9/1/2025
(1, 1, 5, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Sáng ngày 9/1
(2, 1, 6, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Chiều ngày 9/1

-- Lịch khám ngày 10/1/2025
(1, 1, 7, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Sáng ngày 10/1
(2, 1, 8, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Chiều ngày 10/1

-- Lịch khám ngày 11/1/2025
(1, 1, 9, 10), -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Sáng ngày 11/1
(2, 1, 10, 10); -- Bệnh nhân 10, trạng thái "Đang chờ", Ca Chiều ngày 11/1

INSERT INTO PhieuKhamBenhs (NgayTao, LichKhamId)
VALUES
('2025-01-07 08:30:00', 1), -- Phiếu khám bệnh ngày 7/1/2025, lịch khám ID 1
('2025-01-07 14:30:00', 2), -- Phiếu khám bệnh ngày 7/1/2025, lịch khám ID 2
('2025-01-08 08:30:00', 3), -- Phiếu khám bệnh ngày 8/1/2025, lịch khám ID 3
('2025-01-08 14:30:00', 4), -- Phiếu khám bệnh ngày 8/1/2025, lịch khám ID 4
('2025-01-09 08:30:00', 5), -- Phiếu khám bệnh ngày 9/1/2025, lịch khám ID 5
('2025-01-09 14:30:00', 6), -- Phiếu khám bệnh ngày 9/1/2025, lịch khám ID 6
('2025-01-10 08:30:00', 7), -- Phiếu khám bệnh ngày 10/1/2025, lịch khám ID 7
('2025-01-10 14:30:00', 8), -- Phiếu khám bệnh ngày 10/1/2025, lịch khám ID 8
('2025-01-11 08:30:00', 9), -- Phiếu khám bệnh ngày 11/1/2025, lịch khám ID 9
('2025-01-11 14:30:00', 10); -- Phiếu khám bệnh ngày 11/1/2025, lịch khám ID 10

INSERT INTO HoSoBenhAns (NgayTao, BenhNhanId, NhomBenhId)
VALUES
('2025-01-07 08:00:00', 10, 1), -- Hồ sơ bệnh án ngày 7/1/2025, nhóm bệnh 1
('2025-01-08 08:00:00', 10, 2), -- Hồ sơ bệnh án ngày 8/1/2025, nhóm bệnh 2
('2025-01-09 08:00:00', 10, 3), -- Hồ sơ bệnh án ngày 9/1/2025, nhóm bệnh 3
('2025-01-10 08:00:00', 10, 4); -- Hồ sơ bệnh án ngày 10/1/2025, nhóm bệnh 4



INSERT INTO ChiTietKhamBenhs (PhieuKhamBenhId, BenhLyId, GiaKham, GhiChu)
VALUES
-- Dữ liệu liên kết với các phiếu khám bệnh
(1, 1, 50000, NULL), -- Phiếu khám ID 1, bệnh lý ID 1, giá khám 50,000, không có ghi chú
(1, 2, 60000, NULL), -- Phiếu khám ID 1, bệnh lý ID 2, giá khám 60,000, không có ghi chú

(2, 3, 70000, NULL), -- Phiếu khám ID 2, bệnh lý ID 3, giá khám 70,000, không có ghi chú
(2, 4, 80000, NULL), -- Phiếu khám ID 2, bệnh lý ID 4, giá khám 80,000, không có ghi chú

(3, 1, 50000, NULL), -- Phiếu khám ID 3, bệnh lý ID 1, giá khám 50,000, không có ghi chú
(3, 2, 60000, NULL), -- Phiếu khám ID 3, bệnh lý ID 2, giá khám 60,000, không có ghi chú

(4, 3, 70000, NULL), -- Phiếu khám ID 4, bệnh lý ID 3, giá khám 70,000, không có ghi chú
(4, 4, 80000, NULL), -- Phiếu khám ID 4, bệnh lý ID 4, giá khám 80,000, không có ghi chú

(5, 1, 50000, NULL), -- Phiếu khám ID 5, bệnh lý ID 1, giá khám 50,000, không có ghi chú
(5, 2, 60000, NULL), -- Phiếu khám ID 5, bệnh lý ID 2, giá khám 60,000, không có ghi chú

(6, 3, 70000, NULL), -- Phiếu khám ID 6, bệnh lý ID 3, giá khám 70,000, không có ghi chú
(6, 4, 80000, NULL), -- Phiếu khám ID 6, bệnh lý ID 4, giá khám 80,000, không có ghi chú

(7, 1, 50000, NULL), -- Phiếu khám ID 7, bệnh lý ID 1, giá khám 50,000, không có ghi chú
(7, 2, 60000, NULL), -- Phiếu khám ID 7, bệnh lý ID 2, giá khám 60,000, không có ghi chú

(8, 3, 70000, NULL), -- Phiếu khám ID 8, bệnh lý ID 3, giá khám 70,000, không có ghi chú
(8, 4, 80000, NULL), -- Phiếu khám ID 8, bệnh lý ID 4, giá khám 80,000, không có ghi chú

(9, 1, 50000, NULL), -- Phiếu khám ID 9, bệnh lý ID 1, giá khám 50,000, không có ghi chú
(9, 2, 60000, NULL), -- Phiếu khám ID 9, bệnh lý ID 2, giá khám 60,000, không có ghi chú

(10, 3, 70000, NULL), -- Phiếu khám ID 10, bệnh lý ID 3, giá khám 70,000, không có ghi chú
(10, 4, 80000, NULL); -- Phiếu khám ID 10, bệnh lý ID 4, giá khám 80,000, không có ghi chú

INSERT INTO LoaiThuocs (TenLoaiThuoc)
VALUES
(N'Thuốc giảm đau'),        -- Loại thuốc 1
(N'Thuốc kháng sinh'),      -- Loại thuốc 2
(N'Thuốc bổ sung dinh dưỡng'), -- Loại thuốc 3
(N'Thuốc tiêu hóa');        -- Loại thuốc 4

INSERT INTO Thuocs (TenThuoc, Images, SoLuongTon, GiaNhap, NgaySanXuat, HanSuDung, LoaiThuocId)
VALUES
-- Thuốc thuộc nhóm 1
(N'Paracetamol', NULL, 100, 5000, '2025-01-01', '2026-01-01', 1), -- Paracetamol không có ảnh, tồn kho 100, giá nhập 5,000
(N'Amoxicillin', NULL, 50, 10000, '2024-12-01', '2026-12-01', 1), -- Amoxicillin không có ảnh, tồn kho 50, giá nhập 10,000

-- Thuốc thuộc nhóm 2
(N'Ibuprofen', NULL, 200, 8000, '2025-01-15', '2026-07-15', 2), -- Ibuprofen không có ảnh, tồn kho 200, giá nhập 8,000
(N'Cefixime', NULL, 120, 15000, '2024-11-01', '2025-11-01', 2), -- Cefixime không có ảnh, tồn kho 120, giá nhập 15,000

-- Thuốc thuộc nhóm 3
(N'Vitamin C', NULL, 300, 2000, '2025-02-01', '2026-02-01', 3), -- Vitamin C không có ảnh, tồn kho 300, giá nhập 2,000
(N'Omega 3', NULL, 150, 18000, '2024-10-01', '2026-10-01', 3), -- Omega 3 không có ảnh, tồn kho 150, giá nhập 18,000

-- Thuốc thuộc nhóm 4
(N'Loperamide', NULL, 80, 6000, '2025-01-10', '2027-01-10', 4), -- Loperamide không có ảnh, tồn kho 80, giá nhập 6,000
(N'Bismuth Subsalicylate', NULL, 60, 7000, '2024-09-01', '2026-09-01', 4); -- Bismuth Subsalicylate không có ảnh, tồn kho 60, giá nhập 7,000

INSERT INTO ChupChieus (Images, KetLuan, Gia, ChiTietKhamBenhId)
VALUES
-- Dữ liệu liên kết với các chi tiết khám bệnh
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tình trạng bình thường', 200000, 1),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Nghi ngờ tổn thương phổi', 250000, 2),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tình trạng bình thường', 200000, 3),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tổn thương xương đòn', 300000, 4),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tình trạng bình thường', 200000, 5),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tổn thương vùng ngực', 350000, 6),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tình trạng bình thường', 200000, 7),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Nghi ngờ viêm phổi', 250000, 8),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tình trạng bình thường', 200000, 9),
(N'https://easterngroup.com.vn/uploads/chup-x-quang-1280x720.jpeg', N'Tổn thương phổi mãn tính', 400000, 10);

INSERT INTO ChiTietDonThuocs (ChiTietKhamBenhId, ThuocId, SoLuong, DonGia, GhiChu)
VALUES
-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 1
(1, 1, 2, 5000, N'Uống sau ăn'), -- Thuốc ID 1: Paracetamol, 2 viên, 5,000/viên
(1, 2, 1, 10000, N'Uống vào buổi sáng'), -- Thuốc ID 2: Amoxicillin, 1 viên, 10,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 2
(2, 3, 3, 8000, NULL), -- Thuốc ID 3: Ibuprofen, 3 viên, 8,000/viên
(2, 4, 1, 15000, NULL), -- Thuốc ID 4: Cefixime, 1 viên, 15,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 3
(3, 1, 2, 5000, N'Uống sau ăn'), -- Thuốc ID 1: Paracetamol, 2 viên, 5,000/viên
(3, 2, 1, 10000, NULL), -- Thuốc ID 2: Amoxicillin, 1 viên, 10,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 4
(4, 3, 4, 8000, N'Uống trước khi ngủ'), -- Thuốc ID 3: Ibuprofen, 4 viên, 8,000/viên
(4, 4, 2, 15000, NULL), -- Thuốc ID 4: Cefixime, 2 viên, 15,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 5
(5, 1, 1, 5000, N'Uống sau ăn'), -- Thuốc ID 1: Paracetamol, 1 viên, 5,000/viên
(5, 2, 1, 10000, NULL), -- Thuốc ID 2: Amoxicillin, 1 viên, 10,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 6
(6, 3, 3, 8000, NULL), -- Thuốc ID 3: Ibuprofen, 3 viên, 8,000/viên
(6, 4, 2, 15000, NULL), -- Thuốc ID 4: Cefixime, 2 viên, 15,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 7
(7, 1, 2, 5000, N'Uống sau ăn'), -- Thuốc ID 1: Paracetamol, 2 viên, 5,000/viên
(7, 2, 1, 10000, NULL), -- Thuốc ID 2: Amoxicillin, 1 viên, 10,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 8
(8, 3, 3, 8000, N'Uống trước khi ngủ'), -- Thuốc ID 3: Ibuprofen, 3 viên, 8,000/viên
(8, 4, 1, 15000, NULL), -- Thuốc ID 4: Cefixime, 1 viên, 15,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 9
(9, 1, 2, 5000, N'Uống sau ăn'), -- Thuốc ID 1: Paracetamol, 2 viên, 5,000/viên
(9, 2, 1, 10000, NULL), -- Thuốc ID 2: Amoxicillin, 1 viên, 10,000/viên

-- Chi tiết đơn thuốc cho Phiếu khám bệnh ID 10
(10, 3, 4, 8000, NULL), -- Thuốc ID 3: Ibuprofen, 4 viên, 8,000/viên
(10, 4, 2, 15000, NULL); -- Thuốc ID 4: Cefixime, 2 viên, 15,000/viên

INSERT INTO DonViTinhs (TenDonViTinh)
VALUES
(N'Máu'),          -- ID 1: Liên quan đến xét nghiệm máu
(N'Nước tiểu'),    -- ID 2: Liên quan đến xét nghiệm nước tiểu
(N'Vi sinh'),      -- ID 3: Liên quan đến xét nghiệm vi sinh
(N'Hình ảnh');     -- ID 4: Liên quan đến xét nghiệm hình ảnh

INSERT INTO LoaiXetNghiems (TenXetNghiem, GiaThamKhao, DonViTinhId)
VALUES
-- Các loại xét nghiệm máu
(N'Huyết đồ', 150000, 1), -- Xét nghiệm huyết đồ, giá tham khảo 150,000, đơn vị tính ID 1
(N'Sinh hóa máu', 200000, 1), -- Xét nghiệm sinh hóa máu, giá tham khảo 200,000, đơn vị tính ID 1

-- Các loại xét nghiệm nước tiểu
(N'Xét nghiệm nước tiểu tổng quát', 100000, 2), -- Xét nghiệm nước tiểu tổng quát, giá tham khảo 100,000, đơn vị tính ID 2
(N'Tỷ trọng nước tiểu', 120000, 2), -- Xét nghiệm tỷ trọng nước tiểu, giá tham khảo 120,000, đơn vị tính ID 2

-- Các loại xét nghiệm vi sinh
(N'Phân lập vi khuẩn', 300000, 3), -- Xét nghiệm phân lập vi khuẩn, giá tham khảo 300,000, đơn vị tính ID 3
(N'Nuôi cấy vi khuẩn', 350000, 3), -- Xét nghiệm nuôi cấy vi khuẩn, giá tham khảo 350,000, đơn vị tính ID 3

-- Các loại xét nghiệm hình ảnh
(N'Xét nghiệm X-quang phổi', 500000, 4), -- Xét nghiệm X-quang phổi, giá tham khảo 500,000, đơn vị tính ID 4
(N'Siêu âm tổng quát', 400000, 4); -- Siêu âm tổng quát, giá tham khảo 400,000, đơn vị tính ID 4

INSERT INTO ChiTietXetNghiems (ChiTietKhamBenhId, LoaiXetNghiemId, KetQua, DanhGia, GiaXetNghiem)
VALUES
-- Xét nghiệm cho Phiếu khám ID 1
(1, 1, 4.5, N'Bình thường', 150000), -- Xét nghiệm Huyết đồ, kết quả 4.5, giá 150,000
(1, 2, 6.8, N'Cao hơn bình thường', 200000), -- Xét nghiệm Sinh hóa máu, kết quả 6.8, giá 200,000

-- Xét nghiệm cho Phiếu khám ID 2
(2, 3, 1.2, N'Bình thường', 100000), -- Xét nghiệm nước tiểu tổng quát, kết quả 1.2, giá 100,000
(2, 4, 1.0, N'Bình thường', 120000), -- Xét nghiệm tỷ trọng nước tiểu, kết quả 1.0, giá 120,000

-- Xét nghiệm cho Phiếu khám ID 3
(3, 1, 4.7, N'Bình thường', 150000), -- Xét nghiệm Huyết đồ, kết quả 4.7, giá 150,000
(3, 2, 5.5, N'Bình thường', 200000), -- Xét nghiệm Sinh hóa máu, kết quả 5.5, giá 200,000

-- Xét nghiệm cho Phiếu khám ID 4
(4, 3, 1.1, N'Bình thường', 100000), -- Xét nghiệm nước tiểu tổng quát, kết quả 1.1, giá 100,000
(4, 4, 1.0, N'Bình thường', 120000), -- Xét nghiệm tỷ trọng nước tiểu, kết quả 1.0, giá 120,000

-- Xét nghiệm cho Phiếu khám ID 5
(5, 1, 4.9, N'Bình thường', 150000), -- Xét nghiệm Huyết đồ, kết quả 4.9, giá 150,000
(5, 2, 6.0, N'Bình thường', 200000), -- Xét nghiệm Sinh hóa máu, kết quả 6.0, giá 200,000

-- Xét nghiệm cho Phiếu khám ID 6
(6, 3, 1.2, N'Bình thường', 100000), -- Xét nghiệm nước tiểu tổng quát, kết quả 1.2, giá 100,000
(6, 4, 1.1, N'Bình thường', 120000), -- Xét nghiệm tỷ trọng nước tiểu, kết quả 1.1, giá 120,000

-- Xét nghiệm cho Phiếu khám ID 7
(7, 1, 4.6, N'Bình thường', 150000), -- Xét nghiệm Huyết đồ, kết quả 4.6, giá 150,000
(7, 2, 6.3, N'Bình thường', 200000), -- Xét nghiệm Sinh hóa máu, kết quả 6.3, giá 200,000

-- Xét nghiệm cho Phiếu khám ID 8
(8, 3, 1.1, N'Bình thường', 100000), -- Xét nghiệm nước tiểu tổng quát, kết quả 1.1, giá 100,000
(8, 4, 1.0, N'Bình thường', 120000), -- Xét nghiệm tỷ trọng nước tiểu, kết quả 1.0, giá 120,000

-- Xét nghiệm cho Phiếu khám ID 9
(9, 1, 4.8, N'Bình thường', 150000), -- Xét nghiệm Huyết đồ, kết quả 4.8, giá 150,000
(9, 2, 6.2, N'Bình thường', 200000), -- Xét nghiệm Sinh hóa máu, kết quả 6.2, giá 200,000

-- Xét nghiệm cho Phiếu khám ID 10
(10, 3, 1.3, N'Bình thường', 100000), -- Xét nghiệm nước tiểu tổng quát, kết quả 1.3, giá 100,000
(10, 4, 1.1, N'Bình thường', 120000); -- Xét nghiệm tỷ trọng nước tiểu, kết quả 1.1, giá 120,000

INSERT INTO ChiTietHoSoBenhAns (HoSoBenhAnId, ChiTietKhamBenhId)
VALUES
-- Dữ liệu liên kết giữa hồ sơ bệnh án và chi tiết khám bệnh
(1, 1), -- Hồ sơ bệnh án ID 1 liên kết với Chi tiết khám bệnh ID 1
(2, 2), -- Hồ sơ bệnh án ID 1 liên kết với Chi tiết khám bệnh ID 2
(3, 3), -- Hồ sơ bệnh án ID 2 liên kết với Chi tiết khám bệnh ID 3
(3, 4), -- Hồ sơ bệnh án ID 2 liên kết với Chi tiết khám bệnh ID 4
(4, 5), -- Hồ sơ bệnh án ID 3 liên kết với Chi tiết khám bệnh ID 5
(4, 6), -- Hồ sơ bệnh án ID 3 liên kết với Chi tiết khám bệnh ID 6
(1, 7), -- Hồ sơ bệnh án ID 4 liên kết với Chi tiết khám bệnh ID 7
(1, 8), -- Hồ sơ bệnh án ID 4 liên kết với Chi tiết khám bệnh ID 8
(2, 9), -- Hồ sơ bệnh án ID 5 liên kết với Chi tiết khám bệnh ID 9
(2, 10); -- Hồ sơ bệnh án ID 5 liên kết với Chi tiết khám bệnh ID 10
