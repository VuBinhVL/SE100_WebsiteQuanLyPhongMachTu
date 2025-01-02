import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../Header/CustomerHeader.css";
import logo from "../../../assets/images/clinic_logo.png";
import avatar from "../../../assets/icons/user.png";
import { sIsLoggedIn } from "../../../../store";
export default function CustomerHeader() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false); // Trạng thái dropdown mở/đóng
  const navigate = useNavigate();
  const isLoggedInValue = sIsLoggedIn.use();
  const handleLogout = () => {
    localStorage.removeItem("jwtToken"); // Xóa thông tin người dùng
    sIsLoggedIn.set(false);
    setIsDropdownOpen(false); // Đóng dropdown
    navigate("/login"); // Chuyển hướng về trang login
  };

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
                <a href="/medical-exam-list" className="dropdown-item">
                  Đăng kí khám
                </a>
              </li>
              <li>
                <a href="/review-price-list" className="dropdown-item">
                  Bảng giá dịch vụ khám
                </a>
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
                src={avatar}
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
