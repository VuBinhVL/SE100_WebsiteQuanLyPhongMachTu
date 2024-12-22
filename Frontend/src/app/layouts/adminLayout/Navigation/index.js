import React, { useState } from "react";
import "../../../styles/Navigation.css";
import { FaUser } from "react-icons/fa";
import { LiaBookSolid } from "react-icons/lia";
import { GiMedicines } from "react-icons/gi";
import { FaClipboardList } from "react-icons/fa";
import { RxDashboard } from "react-icons/rx";
import { IoIosArrowForward } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
import { GoDotFill } from "react-icons/go";
export default function Navigation() {
    const [checkClick, setCheckClick] = useState(false);
    const handleCheckClick = () => {
        setCheckClick(!checkClick);
    }
    console.log(checkClick)
    return (
        <div className="container-fluid">
            <div className="row">
                <div className="slide-bar bg-white col-auto col-md-2 min-vh-100 d-flex justify-content-between flex-column">
                    <div>
                        <a className="logo-app text-decoration-none d-flex  align-items-center text-black  mt-0">

                            <img src="https://brandforma.com/wp-content/uploads/2024/03/medical-doctor-logo-for-sale.png" className="inner-image" />
                            <span className="fs-4 inner-title fw-bold">Private Practice </span>
                        </a>
                        <hr className="text-secondary mt-3"></hr>
                        <ul className="nav nav-pills flex-column">
                            <li className="nav-item text-black fs-5 py-2 py-sm-0">
                                <a href="#" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page">
                                    <RxDashboard className="icon icon-dashboard" />
                                    <span className="ms-4">Dashboard</span>
                                </a>
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
                                        <div className="listUsers d-flex flex-column">
                                            <button className="itemStaffs itemOfListUsers my-2 py-2" >
                                                <span><GoDotFill className="icon_Bullet_point" /></span>
                                                Staffs
                                            </button>

                                            <button className="itemPatiens itemOfListUsers my-2 py-2">
                                                <span><GoDotFill className="icon_Bullet_point" /></span>
                                                Patiens
                                            </button>
                                            <button className="itemAuthorizations itemOfListUsers my-2 py-2">
                                                <span><GoDotFill className="icon_Bullet_point" /></span>
                                                Authorization
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </li>

                            <li className="nav-item text-black fs-5 py-2 py-sm-0">
                                <a href="#" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page">
                                    <FaClipboardList className="fs-5 icon-medicalExaminationForm icon" />
                                    <span className="ms-4">Examination Forms</span>
                                </a>
                            </li>
                            <li className="nav-item text-black fs-5 py-2 py-sm-0">
                                <a href="#" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page">
                                    <LiaBookSolid className="fs-4 icon-examination icon" />
                                    <span className="ms-3">Medical Shifts</span>
                                </a>
                            </li>
                            <li className="nav-item text-black fs-5 py-2 py-sm-0">
                                <a href="#" className="nav-link d-flex align-items-center text-black fs-5 my-2" aria-current="page">
                                    <GiMedicines className="fs-4 icon-medicine icon" />
                                    <span className="ms-3">Medicines</span>
                                </a>
                            </li>
                        </ul>

                    </div>
                </div>
            </div>
        </div>
    );
}
