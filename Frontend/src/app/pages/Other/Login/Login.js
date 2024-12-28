import React, { useState } from "react";
import { Link } from "react-router-dom";
import Textbox from "../../../components/Other/Textbox"; // Đường dẫn tới component
import SuccessMessageBox from "../../../components/MessageBox/SuccessMessageBox"; // Đường dẫn tới SuccessMessageBox
import "./Login.css";
import nurseIcon from "../../../assets/images/nurse.png"; // Biểu tượng y tá
import usernameIcon from "../../../assets/icons/user.png"; // Icon Tên đăng nhập
import passwordIcon from "../../../assets/icons/password.png"; // Icon Mật khẩu
import logo from "../../../assets/images/clinic4.png";
import "../../../styles/index.css";
import { fetchPost } from "../../../lib/httpHandler";
import { useNavigate } from "react-router-dom";
export default function Login() {
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);

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
      (sus) => {
        alert(sus.message);
        if (sus.message === "Bệnh Nhân") {
          navigate("/");
        } else {
          navigate("/admin");
        }
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
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

      {/* Hiển thị thông báo thành công */}
      {/* {showSuccessMessage && (
        <SuccessMessageBox
          title="Đăng nhập thành công!"
          description="Chào mừng bạn trở lại hệ thống quản lý phòng mạch tư."
          onClose={() => setShowSuccessMessage(false)} // Ẩn thông báo khi nhấn nút "Done"
        />
      )} */}
    </div>
  );
}
