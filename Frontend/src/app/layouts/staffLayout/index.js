
import "./LayoutStaff.css"
import { Outlet } from "react-router-dom";
import Footer from "../adminLayout/Footer/index";
import NavigationOfStaff from "./Navigation";
import HeaderOfStaff from "./Header";
export default function LayoutStaff() {
    return (
        <>
            <div className="LayoutStaff">
                <div className="row mx-0">
                    <div className="col-12 d-flex px-0 ">
                        <div className="section_left col-auto col-md-2">
                            <NavigationOfStaff />
                        </div>
                        <div className="section_right col-9">
                            <div className="header d-flex align-items-center py-3 px-4">
                                <HeaderOfStaff />
                            </div>
                            <div className="content px-4 py-3">
                                <Outlet />
                            </div>
                            <div className="footer d-flex justify-content-center py-4">
                                <Footer />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

