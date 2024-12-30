import { useState, React } from "react";
import { Link, useNavigate } from "react-router-dom";
import Textbox from "../../../components/Other/Textbox"; // Đường dẫn tới component
import { showErrorMessageBox } from "../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showSuccessMessageBox } from "../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import "./Register.css";
import nurseIcon from "../../../assets/images/nurse.png"; // Biểu tượng y tá
import userIcon from "../../../assets/icons/fullname.png"; // Icon Họ và tên
import phoneIcon from "../../../assets/icons/phone.png"; // Icon Số điện thoại
import emailIcon from "../../../assets/icons/email.png"; // Icon Email
import usernameIcon from "../../../assets/icons/user.png"; // Icon Tên đăng nhập
import passwordIcon from "../../../assets/icons/password.png"; // Icon Mật khẩu
import logo from "../../../assets/images/clinic4.png";
import { fetchPost } from "../../../lib/httpHandler";
import "../../../styles/index.css";

export default function Register() {
  const [HoTen, setHoTen] = useState("");
  const [SoDienThoai, setSoDienThoai] = useState("");
  const [Email, setEmail] = useState("");
  const [TenTaiKhoan, setTenTaiKhoan] = useState("");
  const [MatKhau, setMatKhau] = useState("");
  const navigate = useNavigate();

  //Gọi API để đăng kí tài khoản
  const HandlerRegister = () => {
    const data = {
      HoTen,
      SoDienThoai,
      Email,
      TenTaiKhoan,
      MatKhau,
    };
    if (
      HoTen === "" ||
      SoDienThoai === "" ||
      Email === "" ||
      TenTaiKhoan === "" ||
      MatKhau === ""
    ) {
      showErrorMessageBox("Vui lòng nhập đầy đủ thông tin");
      return;
    } else if (SoDienThoai.length !== 10) {
      showErrorMessageBox("Số điện thoại phải đủ 10 số");
      return;
    } else if (SoDienThoai[0] !== "0") {
      showErrorMessageBox("Số điện thoại phải bắt đầu bằng số 0");
      return;
    } else if (Email.indexOf("@") === -1) {
      showErrorMessageBox("Email không hợp lệ");
      return;
    }
    const uri = "/api/register";
    fetchPost(
      uri,
      data,
      (data) => {
        showSuccessMessageBox("Đăng kí tài khoản thành công");
        setTimeout(() => {
          navigate("/login");
        }, 2000);
      },
      (data) => {
        showErrorMessageBox(data.message);
      },
      () => {
        showErrorMessageBox("Kết nối đến máy chủ thất bại");
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
            <h3 className="form-title">ĐĂNG KÍ</h3>
            <img src={nurseIcon} alt="Nurse Icon" className="nurse-icon" />
          </div>
          <div className="form-body">
            <Textbox
              icon={userIcon}
              placeholder="Họ và tên"
              onChange={(e) => setHoTen(e.target.value)}
            />
            <Textbox
              icon={phoneIcon}
              placeholder="Số điện thoại"
              onChange={(e) => setSoDienThoai(e.target.value)}
            />
            <Textbox
              icon={emailIcon}
              placeholder="Email"
              onChange={(e) => setEmail(e.target.value)}
            />
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
              <Link to="/login" className="form-link">
                Đăng nhập
              </Link>
              <Link to="/forget-password" className="form-link">
                Quên mật khẩu
              </Link>
            </div>
            <button className="submit-button" onClick={HandlerRegister}>
              Tạo tài khoản
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
