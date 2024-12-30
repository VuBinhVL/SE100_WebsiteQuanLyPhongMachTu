import React, { useState } from "react";
import { Link } from "react-router-dom";
import Textbox from "../../../components/Other/Textbox"; // Đường dẫn tới component
import { showSuccessMessageBox } from "../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox"; // Đường dẫn tới hàm hiển thị MessageBox
import { showYesNoMessageBox } from "../../../components/MessageBox/YesNoMessageBox/showYesNoMessgeBox"; // Đường dẫn tới hàm hiển thị MessageBox
import { showErrorMessageBox } from "../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // Đường dẫn tới hàm hiển thị MessageBox

import "./Login.css";
import nurseIcon from "../../../assets/images/nurse.png"; // Biểu tượng y tá
import usernameIcon from "../../../assets/icons/user.png"; // Icon Tên đăng nhập
import passwordIcon from "../../../assets/icons/password.png"; // Icon Mật khẩu
import logo from "../../../assets/images/clinic4.png";
import "../../../styles/index.css";
import { fetchPost } from "../../../lib/httpHandler";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [tenTaiKhoan, setTenTaiKhoan] = useState("");
  const [matKhau, setMatKhau] = useState("");
  const navigate = useNavigate();

  const handleLogin = () => {
    const dataSend = {
      tenTaiKhoan,
      matKhau,
    };
    console.log(dataSend);
    const uri = "/api/login";
    fetchPost(
      uri,
      dataSend,
      async (sus) => {
        showYesNoMessageBox("Bạn có muốn đăng nhập không?").then((result) => {
          if (result) {
            console.log("User chọn YES. Thực hiện hành động xóa...");
            // Thực hiện tiếp hành động
            navigate("/admin");
          } else {
            console.log("User chọn NO. Dừng lại...");
            // Dừng lại không làm gì
          }
        });
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        showErrorMessageBox("Có lỗi xảy ra");
      }
    );
  };

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
            <h3 className="form-title">ĐĂNG NHẬP</h3>
            <img src={nurseIcon} alt="Nurse Icon" className="nurse-icon" />
          </div>
          <div className="form-body">
            <Textbox
              icon={usernameIcon}
              placeholder="Tên đăng nhập"
              onChange={(e) => setTenTaiKhoan(e.target.value)}
            />
            <Textbox
              icon={passwordIcon}
              placeholder="Mật khẩu"
              type="password"
              onChange={(e) => setMatKhau(e.target.value)}
            />
            <div className="form-links">
              <Link to="/register" className="form-link">
                Tạo tài khoản
              </Link>
              <Link to="/forgot-password" className="form-link">
                Quên mật khẩu
              </Link>
            </div>
            <button onClick={handleLogin} className="submit-button">
              Đăng nhập
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
