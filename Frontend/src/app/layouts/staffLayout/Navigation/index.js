import React, { useState } from "react";
import { FaUser } from "react-icons/fa";
import { GoDotFill } from "react-icons/go";
import { IoIosArrowDown, IoIosArrowForward } from "react-icons/io";
import { LiaBookSolid } from "react-icons/lia";
import { NavLink } from "react-router-dom";
import { FaClipboardList } from "react-icons/fa";
import "./NavigationOfStaff.css";
export default function NavigationOfStaff() {
  const [checkClick, setCheckClick] = useState(false);
  const handleCheckClick = () => {
    setCheckClick(!checkClick);
  };
  // console.log(checkClick)
  return (
    <div className="Navigation_Staff">
      <div className="slide-bar bg-white  min-vh-100 d-flex justify-content-between flex-column">
        <div>
          <a className="logo-app text-decoration-none d-flex  align-items-center text-black  mt-0">
            <img
              src="https://brandforma.com/wp-content/uploads/2024/03/medical-doctor-logo-for-sale.png"
              className="inner-image"
            />
            <span className="fs-4 inner-title fw-bold">Private Practice </span>
          </a>
          <hr className="text-secondary mt-3"></hr>
          <ul className="nav nav-pills flex-column">
            <li className="nav-item text-black fs-5 py-2 py-sm-0">
              <NavLink
                to="/staff"
                className="nav-link d-flex align-items-center text-black fs-5 my-2"
                aria-current="page"
                end
              >
                <FaUser className="fs-4 icon-patient icon" />
                <span className="ms-3">Quản lý bệnh nhân</span>
              </NavLink>
            </li>
            <li className="nav-item text-black fs-5 py-2 py-sm-0">
              <NavLink
                to="medical-examination-card"
                className="nav-link d-flex align-items-center text-black fs-5 my-2"
                aria-current="page"
              >
                <FaClipboardList className="fs-5 icon-medicalExaminationForm icon" />
                <span className="ms-4">Quản lý phiếu khám bệnh</span>
              </NavLink>
            </li>
            <li className="nav-item text-black fs-5 py-2 py-sm-0">
              <a
                className="Parent nav-link d-flex align-items-center text-black fs-5 my-2"
                aria-current="page"
                data-bs-toggle="collapse"
                role="button"
                aria-expanded={checkClick}
                onClick={handleCheckClick}
              >
                <LiaBookSolid className="fs-4 icon-examination icon" />
                <span className="ms-4">Quản lý ca khám</span>
                <span className="iconArrowRight">
                  {checkClick === true ? (
                    <IoIosArrowDown />
                  ) : (
                    <IoIosArrowForward />
                  )}
                </span>
              </a>
              <div
                className={`collapse ${checkClick ? "show" : "hide"}`}
                id="collapseExample"
              >
                <div className="card card-body">
                  <div className="listUsers d-flex flex-column ">
                    <NavLink
                      to="examination-management"
                      className="itemStaffs itemOfListUsers my-2 py-2"
                    >
                      <span>
                        <GoDotFill className="icon_Bullet_point" />
                      </span>
                      Xem ca khám
                    </NavLink>

                    <NavLink
                      to="examination-register"
                      className="itemPatiens itemOfListUsers my-2 py-2 "
                    >
                      <span>
                        <GoDotFill className="icon_Bullet_point" />
                      </span>
                      Đăng ký ca khám
                    </NavLink>
                  </div>
                </div>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
}
