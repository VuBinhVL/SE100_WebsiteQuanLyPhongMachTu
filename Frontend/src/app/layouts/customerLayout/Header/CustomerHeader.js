import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../Header/CustomerHeader.css";
import logo from "../../../assets/images/clinic_logo.png";
import avatar from "../../../assets/icons/user.png";
import { sIsLoggedIn } from "../../../../store";
import { showErrorMessageBox } from "../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { fetchGet } from "../../../lib/httpHandler";

export default function CustomerHeader() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false); // Trạng thái dropdown mở/đóng
  const navigate = useNavigate();
  const isLoggedInValue = sIsLoggedIn.use();

  const [image, setImage] = useState(""); // Lưu ảnh đại diện

  //Đăng xuất
  const handleLogout = () => {
    localStorage.removeItem("jwtToken"); // Xóa thông tin người dùng
    sIsLoggedIn.set(false);
    setIsDropdownOpen(false); // Đóng dropdown
    navigate("/login"); // Chuyển hướng về trang login
  };

  //Lấy avatar
  useEffect(() => {
    const token = localStorage.getItem("jwtToken");
    if (!token || !isLoggedInValue) return; // Không gọi API nếu không có token hoặc chưa đăng nhập

    const uri = "/api/quan-li-thong-tin-ca-nhan";
    fetchGet(
      uri,
      (data) => {
        setImage(data.image || ""); // Gán ảnh đại diện nếu có
      },
      (error) => {
        showErrorMessageBox(error.message);
      },
      () => {
        showErrorMessageBox("Không thể kết nối đến server");
      }
    );
  }, [isLoggedInValue]);

  return (
    <header className="customer-header">
      <div className="logo">
        <Link to="/">
          <img className="logo-img" src={logo} alt="Logo phòng khám"></img>
        </Link>
      </div>
      <nav className="header-nav">
        <ul className="nav-list">
          <li className="nav-item">
            <Link to="/" className="nav-links">
              Trang chủ
            </Link>
          </li>
          <li className="nav-item dropdown">
            <a className="nav-link">Dịch vụ</a>
            <ul className="dropdown-menu">
              <li>
                <Link to="/medical-exam-list" className="dropdown-item">
                  Đăng kí khám
                </Link>
              </li>
              <li>
                <Link to="/review-price-list" className="dropdown-item">
                  Bảng giá dịch vụ khám
                </Link>
              </li>
            </ul>
          </li>

          <li className="nav-item">
            <Link to="/doctors" className="nav-links">
              Bác sĩ
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/" className="nav-links">
              Bài viết
            </Link>
          </li>
          {isLoggedInValue ? (
            // Khi đã đăng nhập, hiển thị avatar và dropdown
            <li className="nav-item">
              <img
                src={image}
                alt="Avatar"
                className="avatar"
                onClick={() => setIsDropdownOpen(!isDropdownOpen)}
              />
              {isDropdownOpen && (
                <ul className="login-menu">
                  <li className="login-item">
                    <Link to="/account">Thông tin tài khoản</Link>
                  </li>
                  <li className="login-item">
                    <Link to="/medical-record">Hồ sơ bệnh án</Link>
                  </li>
                  <li className="login-item" onClick={handleLogout}>
                    Đăng xuất
                  </li>
                </ul>
              )}
            </li>
          ) : (
            // Khi chưa đăng nhập, hiển thị nút Đăng nhập
            <li className="nav-item" id="btn-dangnhap">
              <Link to="/login" className="nav-links">
                Đăng nhập
              </Link>
            </li>
          )}
        </ul>
      </nav>
    </header>
  );
}
