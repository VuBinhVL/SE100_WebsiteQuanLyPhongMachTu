import React from "react";
import { FaUserCircle } from "react-icons/fa";
import "../../../styles/adminStyle/Header/Header.css"
import { FaUserCog } from "react-icons/fa";
import { GrLogout } from "react-icons/gr";
export default function Header() {
    return (
        <>
            <div className="section_Left fs-3">
                Medicine Management
            </div>
            <div className="section_Right d-flex align-items-center">
                {/* <a href="#" className="fs-3 d-flex align-items-center">
                    <FaUserCircle className="fs-3 icon_header" />
                </a> */}
                <div className="dropdown">
                    <a className="btn dropdown-toggle fs-4 d-flex align-items-center px-0 py-0" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        <FaUserCircle className="fs-4 icon_header" />
                    </a>

                    <div className="dropdown-menu">
                        <a className="dropdown-item d-flex align-items-center" href="#">
                            <span><FaUserCog className="me-2 fs-5" /></span>
                            Personal information
                        </a>
                        <a className="dropdown-item d-flex align-items-center icon_personalInformation" href="#">
                            <span><GrLogout className="me-2 fs-5 icon_logout" /></span>
                            Log out
                        </a>

                    </div>
                </div>
            </div>
        </>
    );
}
