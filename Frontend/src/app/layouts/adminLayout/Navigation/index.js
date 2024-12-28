import React, { useState } from "react";
import "./Navigation.css";
import { FaUser } from "react-icons/fa";
import { LiaBookSolid } from "react-icons/lia";
import { GiMedicines } from "react-icons/gi";
import { FaClipboardList } from "react-icons/fa";
import { RxDashboard } from "react-icons/rx";
import { IoIosArrowForward } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
import { GoDotFill } from "react-icons/go";
import { NavLink } from "react-router-dom"
export default function Navigation() {
    const [checkClick, setCheckClick] = useState(false);
    const handleCheckClick = () => {
        setCheckClick(!checkClick);
    }
    console.log(checkClick)
    return (
        <div className="Navigation_admin">
            <div className="slide-bar bg-white  min-vh-100 d-flex justify-content-between flex-column">
                <div>
                    <a className="logo-app text-decoration-none d-flex  align-items-center text-black  mt-0">

                        <img src="https://brandforma.com/wp-content/uploads/2024/03/medical-doctor-logo-for-sale.png" className="inner-image" />
                        <span className="fs-4 inner-title fw-bold">Private Practice </span>
                    </a>
                    <hr className="text-secondary mt-3"></hr>
                    <ul className="nav nav-pills flex-column">
                        <li className="nav-item text-black fs-5 py-2 py-sm-0">
                            <NavLink to="/admin" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page" end>
                                <RxDashboard className="icon icon-dashboard" />
                                <span className="ms-4">Dashboard</span>
                            </NavLink>
                        </li>
                        <li className="nav-item text-black fs-5 py-2 py-sm-0">
                            <a className="Parent nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page" data-bs-toggle="collapse"
                                href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" onClick={handleCheckClick}>
                                <FaUser className="fs-5 icon" />
                                <span className="ms-4">Users</span>
                                <span className="iconArrowRight">
                                    {
                                        checkClick === true ? (<IoIosArrowDown />) : (<IoIosArrowForward />)
                                    }
                                </span>
                            </a>
                            <div className="collapse" id="collapseExample">
                                <div className="card card-body">
                                    <div className="listUsers d-flex flex-column ">
                                        <NavLink to="Staff" className="itemStaffs itemOfListUsers my-2 py-2" >
                                            <span><GoDotFill className="icon_Bullet_point" /></span>
                                            Staffs
                                        </NavLink>

                                        <NavLink to="Patien" className="itemPatiens itemOfListUsers my-2 py-2 ">
                                            <span><GoDotFill className="icon_Bullet_point" /></span>
                                            Patiens
                                        </NavLink>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li className="nav-item text-black fs-5 py-2 py-sm-0">
                            <NavLink to="ExaminationForm" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page">
                                <FaClipboardList className="fs-5 icon-medicalExaminationForm icon" />
                                <span className="ms-4">Examination Forms</span>
                            </NavLink>
                        </li>
                        <li className="nav-item text-black fs-5 py-2 py-sm-0">
                            <NavLink to="MedicalShift" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page">
                                <LiaBookSolid className="fs-4 icon-examination icon" />
                                <span className="ms-3">Medical Shifts</span>
                            </NavLink>
                        </li>
                        <li className="nav-item text-black fs-5 py-2 py-sm-0">
                            <NavLink to="Medicine" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page">
                                <GiMedicines className="fs-4 icon-medicine icon " />
                                <span className="ms-3">Medicines</span>
                            </NavLink>
                        </li>
                    </ul>

                </div>
            </div>
        </div>

    );
}
