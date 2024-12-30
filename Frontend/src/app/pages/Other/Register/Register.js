import React from "react";
import { Link } from "react-router-dom";
import Textbox from "../../../components/Other/Textbox"; // Đường dẫn tới component
import "./Register.css";
import nurseIcon from "../../../assets/images/nurse.png"; // Biểu tượng y tá
import userIcon from "../../../assets/icons/fullname.png"; // Icon Họ và tên
import phoneIcon from "../../../assets/icons/phone.png"; // Icon Số điện thoại
import emailIcon from "../../../assets/icons/email.png"; // Icon Email
import usernameIcon from "../../../assets/icons/user.png"; // Icon Tên đăng nhập
import passwordIcon from "../../../assets/icons/password.png"; // Icon Mật khẩu
import logo from "../../../assets/images/clinic4.png";
import "../../../styles/index.css";

export default function Register() {
  return (
    <div className="register-page">
      <div className="register-left">
        <h1 className="register-title">HỆ THỐNG PHÒNG MẠCH TƯ</h1>
        <div className="register-image">
          {/* Đặt ảnh tại đây */}
          <img src={logo} alt="Phòng mạch tư" />
        </div>
      </div>
      <div className="register-right">
        <div className="register-form">
          <div className="form-header">
            <h3 className="form-title">ĐĂNG KÍ</h3>
            <img src={nurseIcon} alt="Nurse Icon" className="nurse-icon" />
          </div>
          <div className="form-body">
            <Textbox icon={userIcon} placeholder="Họ và tên" />
            <Textbox icon={phoneIcon} placeholder="Số điện thoại" />
            <Textbox icon={emailIcon} placeholder="Email" />
            <Textbox icon={usernameIcon} placeholder="Tên đăng nhập" />
            <Textbox
              icon={passwordIcon}
              placeholder="Mật khẩu"
              type="password"
            />
            <div className="form-links">
              <Link to="/login" className="form-link">
                Đăng nhập
              </Link>
              <Link to="/forget-password" className="form-link">
                Quên mật khẩu
              </Link>
            </div>
            <button className="submit-button">Tạo tài khoản</button>
          </div>
        </div>
      </div>
    </div>
  );
}
