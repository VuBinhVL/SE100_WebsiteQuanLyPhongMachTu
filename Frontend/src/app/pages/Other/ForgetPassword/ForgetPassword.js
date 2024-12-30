import React from "react";
import { Link } from "react-router-dom";
import Textbox from "../../../components/Other/Textbox"; // Đường dẫn tới component
import emailIcon from "../../../assets/icons/email.png"; // Icon Mật khẩu
import usernameIcon from "../../../assets/icons/user.png"; // Icon Tên đăng nhập
import logo from "../../../assets/images/clinic4.png";
import nurseIcon from "../../../assets/images/nurse.png"; // Biểu tượng y tá
import "../../../styles/index.css";
import "./ForgetPassword.css";

export default function ForgetPassword() {
  return (
    <div className="forget-page">
      <div className="forget-left">
        <h1 className="forget-title">HỆ THỐNG PHÒNG MẠCH TƯ</h1>
        <div className="forget-image">
          {/* Đặt ảnh tại đây */}
          <img src={logo} alt="Phòng mạch tư" />
        </div>
      </div>
      <div className="forget-right">
        <div className="forget-form">
          <div className="form-header">
            <h3 className="form-title">QUÊN MẬT KHẨU</h3>
            <img src={nurseIcon} alt="Nurse Icon" className="nurse-icon" />
          </div>
          <div className="form-body">
            <Textbox icon={usernameIcon} placeholder="Tên đăng nhập" />
            <Textbox icon={emailIcon} placeholder="Email" type="password" />
            <div className="form-links">
              <Link to="/login" className="form-link">
                Đăng nhập
              </Link>
              <Link to="/register" className="form-link">
                Đăng ký
              </Link>
            </div>
            <button className="submit-button">Gửi mật khẩu</button>
          </div>
        </div>
      </div>
    </div>
  );
}
