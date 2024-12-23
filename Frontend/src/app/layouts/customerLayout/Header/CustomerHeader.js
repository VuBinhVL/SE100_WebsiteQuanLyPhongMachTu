import React from "react";
import "../Header/CustomerHeader.css";
import logo from "../../../assets/images/logo_phongkham.png";

export default function CustomerHeader() {
  return (
    <header className="customer-header">
      <div className="logo">
        <img className="logo-img" src={logo} alt="Logo phòng khám"></img>
      </div>
      <nav className="header-nav">
        <ul className="nav-list">
          <li className="nav-item">
            <a></a>Trang chủ
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
            <a href=""></a>Bác sĩ
          </li>
          <li className="nav-item">
            <a></a>Bài viết
          </li>
          <li className="nav-item" id="btn-dangnhap">
            <a></a>Đăng nhập
          </li>
        </ul>
      </nav>
    </header>
  );
}
