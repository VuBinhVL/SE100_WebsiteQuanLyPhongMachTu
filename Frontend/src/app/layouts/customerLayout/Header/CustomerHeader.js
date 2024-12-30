import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "../Header/CustomerHeader.css";
import logo from "../../../assets/images/clinic_logo.png";
import avatar from "../../../assets/icons/user.png";

export default function CustomerHeader() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false); // Trạng thái dropdown mở/đóng
  let [isLoggedIn, setIsLoggedIn] = useState(false);
  useEffect(() => {
    if (localStorage.getItem("user") !== null) {
      setIsLoggedIn(true);
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("user"); // Xóa thông tin người dùng
    setIsLoggedIn(false); // Cập nhật trạng thái đăng xuất
    setIsDropdownOpen(false); // Đóng dropdown
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
            <a href="#" className="nav-link">
              Dịch vụ
            </a>
            <ul className="dropdown-menu">
              <li>
                <a href="/service1" className="dropdown-item">
                  Đăng kí khám
                </a>
              </li>
              <li>
                <a href="/service2" className="dropdown-item">
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
          {isLoggedIn ? (
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
                    <Link to="/medical-records">Hồ sơ bệnh án</Link>
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
