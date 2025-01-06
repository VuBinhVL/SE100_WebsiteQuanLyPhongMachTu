import React from "react";
import { FaUserCircle } from "react-icons/fa";
import "./Header.css";
import { FaUserCog } from "react-icons/fa";
import { GrLogout } from "react-icons/gr";
import { Link, useNavigate } from "react-router-dom";
import { sIsLoggedIn } from "../../../../store";

export default function Header() {
  const navigate = useNavigate();
  const isLoggedInValue = sIsLoggedIn.use();

  //Đăng xuất
  const handleLogout = () => {
    localStorage.removeItem("jwtToken"); // Xóa thông tin người dùng
    sIsLoggedIn.set(false);
    navigate("/login"); // Chuyển hướng về trang login
  };
  return (
    <>
      <div className="section_Left fs-3">Medicine Management</div>
      <div className="section_Right d-flex align-items-center">
        {/* <a href="#" className="fs-3 d-flex align-items-center">
                    <FaUserCircle className="fs-3 icon_header" />
                </a> */}
        <div className="dropdown">
          <a
            className="btn dropdown-toggle fs-4 d-flex align-items-center px-0 py-0"
            href="#"
            role="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            <FaUserCircle className="fs-4 icon_header" />
          </a>

          <div className="dropdown-menu">
            <Link
              className="dropdown-item d-flex align-items-center"
              to="/admin/information-management"
            >
              <span>
                <FaUserCog className="me-2 fs-5" />
              </span>
              Thông tin tài khoản
            </Link>
            <a
              className="dropdown-item d-flex align-items-center icon_personalInformation"
              onClick={handleLogout}
            >
              <span>
                <GrLogout className="me-2 fs-5 icon_logout" />
              </span>
              Đăng xuất
            </a>
          </div>
        </div>
      </div>
    </>
  );
}
