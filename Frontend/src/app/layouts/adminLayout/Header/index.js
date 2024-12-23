import React from "react";
import { FaUserCircle } from "react-icons/fa";
import "../../../styles/adminStyle/Header/Header.css"
export default function Header() {
    return (
        <>
            <div className="section_Left fs-3">
                Quản lý thuốc
            </div>
            <div className="section_Right d-flex align-items-center">
                <a href="#" className="fs-3 d-flex align-items-center">
                    <FaUserCircle className="fs-3 icon_header" />
                </a>
            </div>
        </>


    );
}
