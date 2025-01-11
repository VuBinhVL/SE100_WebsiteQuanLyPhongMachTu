# Website Quản Lý Phòng Mạch Tư

## Nhóm Thực Hiện
- **Vũ Bình**: Trưởng nhóm, thiết kế FE và tích hợp API.
- **Tiến Đạt**: Thành viên, thiết kế FE và tích hợp API.
- **Văn Minh**: Phát triển backend và API.
- **Thế Dũng**: Phát triển BE và API.

## Giới Thiệu
Đồ án cuối kỳ môn **Phát Triển Phần Mềm Hướng Đối Tượng** nhằm thiết kế và phát triển một website quản lý phòng mạch tư. Hệ thống giúp tối ưu hóa quy trình quản lý phòng mạch và được xây dựng cho 3 phía người dùng: bệnh nhân, nhân viên và chủ phòng mạch.

## Mục Tiêu
- Tạo một giao diện trực quan, thân thiện với người dùng.
- Quản lý thông tin bệnh nhân, lịch hẹn và phiếu khám bệnh.
- Xây dựng quy trình xử lý hóa đơn nhanh chóng và đảm bảo tính chính xác.
- Tích hợp báo cáo, thống kê hoạt động phòng mạch.

## Tính Năng
- **Phía Khách Hàng:**
  - Quản lý hồ sơ bệnh án.
  - Xem chi tiết dịch vụ khám và đặt lịch khám.
  - Nhận thông báo lịch khám qua email tài khoản người dùng.
  - Quản lý thông tin cá nhân.

- **Phía Nhân Viên:**
  - Quản lý bệnh nhân.
  - Quản lý phiếu khám bệnh.
  - Quản lý các ca khám của mình.
  - Đăng ký ca khám phù hợp với mình.

- **Phía Admin:**
  - Các chức năng giống như nhân viên.
  - Thống kê báo cáo hoạt động phòng mạch.
  - Quản lý nhân viên.
  - Quản lý nhập thuốc, giá trị tham số của hệ thống (giá bán thuốc, số lần hủy lịch khám tối đa của khách hàng,...).
  - Quản lý các loại thuốc, nhóm bệnh.

## Kiến Trúc Hệ Thống
- **Giao Diện Người Dùng:** Sử dụng React để thiết kế giao diện trực quan và linh hoạt (frontend).
- **Hệ Quản Lý Dữ Liệu:** Sử dụng cơ sở dữ liệu SQL Server để lưu trữ thông tin bệnh nhân, lịch hẹn và hóa đơn.
- **Xử Lý Server:** Sử dụng .NET MVC để cung cấp các API quản lý dữ liệu (backend).
- **Triển Khai:** Website được triển khai trên một tên miền cố định.

## Kỹ Thuật Được Sử Dụng
- **Frontend:** React, HTML, CSS, JavaScript.
- **Backend:** .NET MVC.
- **Database:** SQL Server.

## Yêu Cầu Hệ Thống
- Trình duyệt hỗ trợ HTML5 và JavaScript.
- Môi trường chạy .NET Framework.
- Cơ sở dữ liệu SQL Server.

## Hướng Dẫn Cài Đặt
1. Clone repository:
   ```bash
   git clone https://github.com/VuBinhVL/SE100_WebsiteQuanLyPhongMachTu.git
   ```
2. Cài đặt các thư viện yêu cầu:
   
      2.1 Bên FE
   ```bash
   npm install
   ```
      2.2 Bên BE
   ```bash
   dotnet restore
   ```
3. Thay đổi chuỗi `connectstring` trong file `appsettings.json` theo cấu hình cơ sở dữ liệu của bạn.

4. Khởi động frontend:
   ```bash
   npm start
   ```
5. Khởi động backend:
   ```bash
   dotnet run
   ```

