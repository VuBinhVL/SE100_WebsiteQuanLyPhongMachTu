import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import Textbox from "../../../components/Other/Textbox"; // Đường dẫn tới component
import emailIcon from "../../../assets/icons/email.png"; // Icon Mật khẩu
import usernameIcon from "../../../assets/icons/user.png"; // Icon Tên đăng nhập
import logo from "../../../assets/images/clinic4.png";
import nurseIcon from "../../../assets/images/nurse.png"; // Biểu tượng y tá
import { fetchPost } from "../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showSuccessMessageBox } from "../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import "../../../styles/index.css";
import "./ForgetPassword.css";

export default function ForgetPassword() {
  const [tenTaiKhoan, setTenTaiKhoan] = useState("");
  const [email, setEmail] = useState("");
  const navigate = useNavigate();

  // Hàm xử lý sự kiện khi người dùng nhấn nút "Gửi mật khẩu"
  const handleChangePassword = () => {
    const dataSend = {
      tenTaiKhoan,
      email,
    };
    if (tenTaiKhoan === "" || email === "") {
      showErrorMessageBox("Vui lòng nhập đầy đủ thông tin");
      return;
    } else if (!email.includes("@")) {
      showErrorMessageBox("Email không hợp lệ");
      return;
    }
    console.log(dataSend);
    const uri = "/api/forgot-password";
    fetchPost(
      uri,
      dataSend,
      (result) => {
        showSuccessMessageBox(result.message);
        setTimeout(() => {
          navigate("/login");
        }, 5000);
      },
      (error) => showErrorMessageBox(error.message),
      () => showErrorMessageBox("Đã xảy ra lỗi, vui lòng thử lại sau")
    );
  };

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
            <Textbox
              icon={usernameIcon}
              placeholder="Tên đăng nhập"
              onChange={(e) => setTenTaiKhoan(e.target.value)}
            />
            <Textbox
              icon={emailIcon}
              placeholder="Email"
              onChange={(e) => setEmail(e.target.value)}
            />
            <div className="form-links">
              <Link to="/login" className="form-link">
                Đăng nhập
              </Link>
              <Link to="/register" className="form-link">
                Đăng ký
              </Link>
            </div>
            <button className="submit-button" onClick={handleChangePassword}>
              Gửi mật khẩu
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
