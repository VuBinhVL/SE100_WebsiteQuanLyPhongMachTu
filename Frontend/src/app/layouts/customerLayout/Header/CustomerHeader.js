import React from "react";
import { Link } from "react-router-dom";
import "../Header/CustomerHeader.css";
import logo from "../../../assets/images/clinic_logo.png";

export default function CustomerHeader() {
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
            </Link>{" "}
          </li>
          <li className="nav-item">
            <Link to="/" className="nav-links">
              Bài viết
            </Link>
          </li>
          <li className="nav-item" id="btn-dangnhap">
            <Link to="/login" className="nav-links">
              Đăng nhập
            </Link>
          </li>
        </ul>
      </nav>
    </header>
  );
}
