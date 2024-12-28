import React from "react";
import "../Footer/CustomerFooter.css";
export default function CustomerFooter() {
  return (
    <footer className="customer-footer" id="footer">
      <div className="footer-container" id="footer-container">
        {/* Giới thiệu phòng mạch */}
        <div className="footer-section footer-about" id="footer-about">
          <h3 className="footer-heading" id="about-heading">
            Giới thiệu
          </h3>
          <p className="footer-text" id="about-text">
            Phòng mạch tư nhân DG cam kết mang đến dịch vụ chăm sóc sức khỏe tốt
            nhất cho khách hàng. Với đội ngũ bác sĩ giàu kinh nghiệm và trang
            thiết bị hiện đại, chúng tôi luôn sẵn sàng phục vụ bạn.
          </p>
        </div>

        {/* Liên hệ */}
        <div className="footer-section footer-contact" id="footer-contact">
          <h3 className="footer-heading" id="contact-heading">
            Liên hệ
          </h3>
          <ul className="footer-list" id="contact-list">
            <li className="footer-item" id="contact-address">
              Địa chỉ: 123 Đường ABC, Quận 1, Thành phố Hồ Chí Minh
            </li>
            <li className="footer-item" id="contact-phone">
              Số điện thoại: 0123 456 789
            </li>
            <li className="footer-item" id="contact-email">
              Email: info@phongmachabc.com
            </li>
          </ul>
        </div>

        {/* Thời gian làm việc */}
        <div
          className="footer-section footer-working-hours"
          id="footer-working-hours"
        >
          <h3 className="footer-heading" id="working-hours-heading">
            Thời gian làm việc
          </h3>
          <ul className="footer-list" id="working-hours-list">
            <li className="footer-item" id="working-hours-weekdays">
              Thứ Hai - Thứ Sáu: 8:00 - 17:00
            </li>
            <li className="footer-item" id="working-hours-saturday">
              Thứ Bảy: 8:00 - 12:00
            </li>
            <li className="footer-item" id="working-hours-sunday">
              Chủ Nhật: Nghỉ
            </li>
          </ul>
        </div>
      </div>

      {/* Phần bản quyền */}
      <div className="footer-copyright" id="footer-copyright">
        <p className="footer-text" id="copyright-text">
          &copy; 2024 Phòng mạch DG. Mọi quyền được bảo lưu.
        </p>
      </div>
    </footer>
  );
}
