import React, { useState } from "react";
import { FaUserCircle, FaUserCog } from "react-icons/fa";
import { GrLogout } from "react-icons/gr";
import { Link, useNavigate } from "react-router-dom";
import { sIsLoggedIn } from "../../../../store";
import { MdArrowDropDown } from "react-icons/md";
import "./HeaderOfStaff.css";

export default function HeaderOfStaff() {
  const navigate = useNavigate();
  const isLoggedInValue = sIsLoggedIn.use();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  // Đăng xuất
  const handleLogout = () => {
    localStorage.removeItem("jwtToken"); // Xóa thông tin người dùng
    sIsLoggedIn.set(false);
    navigate("/login"); // Chuyển hướng về trang login
  };

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  return (
    <>
      <div className="section_Left fs-3">Medicine Management</div>
      <div className="section_Right d-flex align-items-center">
        <div className="custom-dropdown">
          <button
            className="btn fs-4 d-flex align-items-center px-0 py-0"
            onClick={toggleDropdown}
          >
            <FaUserCircle className="fs-4 icon_header" />
            <MdArrowDropDown className="fs-3 icon_header" />
          </button>
          {isDropdownOpen && (
            <div className="custom-dropdown-menu">
              <Link
                className="dropdown-item d-flex align-items-center"
                to="/staff/information-management"
              >
                <FaUserCog className="me-2 fs-5" />
                Thông tin tài khoản
              </Link>
              <a
                className="dropdown-item d-flex align-items-center icon_personalInformation"
                onClick={handleLogout}
              >
                <GrLogout className="me-2 fs-5 icon_logout" />
                Đăng xuất
              </a>
            </div>
          )}
        </div>
      </div>
    </>
  );
}