import React from "react";
import "./Staff.css"
import { IoMdAddCircleOutline } from "react-icons/io";
import { IoIosSearch } from "react-icons/io";
import { MdDelete } from "react-icons/md";
import { FaUserShield } from "react-icons/fa";
import { GrCircleInformation } from "react-icons/gr";
import AddStaff from "../../../../components/Admin/StaffManagement/AddStaff/AddStaff";
export default function Staff() {
    return (
        <div className="Home">
            <div className="title py-3 fs-5 mb-2">
                Total number of doctors: 4
            </div>
            <div className="row mx-0 my-0">
                <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
                    {/* <!-- Nhóm bên trái --> */}
                    <div className="d-flex align-items-center col-10">
                        <div className="contain_Search position-relative col-4 me-3">
                            <input
                                className="search rounded-2 px-3"
                                placeholder="Nhập tên hoặc số điện thoại"
                            ></input>
                            <IoIosSearch className="icon_search translate-middle-y text-secondary" />
                        </div>

                        <select
                            className="filter col-3 px-3 me-3 rounded-2"
                            defaultValue={"DEFAULT"}
                            name="cars"
                            id="cars"
                        >
                            <option value="DEFAULT" disabled hidden>
                                Lọc theo chuyên môn
                            </option>
                            <option value="volvo">Tim mạch</option>
                            <option value="saab">Gan</option>
                            <option value="opel">Thận</option>
                        </select>
                    </div>

                    {/* <!-- Button căn phải --> */}
                    {/* <button className="Add col-2 rounded-2 d-flex align-items-center justify-content-center">
                        <span><IoMdAddCircleOutline className="fs-4 me-2" /></span>
                        Add Staff
                    </button> */}
                    <AddStaff />
                </div>
                {/* table */}
                <div className="contain_Table mx-0 col-12 bg-white rounded-2">
                    <table className="table table-hover">
                        <thead>
                            <tr>
                                <th scope="col">STT</th>
                                <th scope="col">Full name</th>
                                <th scope="col">Gender</th>
                                <th scope="col">Phone number</th>
                                <th scope="col">Specialization</th>
                                <th scope="col">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">1</th>
                                <td>Trần Tiến Đạt</td>
                                <td>Nam</td>
                                <td>0123654987</td>
                                <td>Tim mạch</td>
                                <td >
                                    <div className="list_Action">
                                        <a href="#">
                                            <FaUserShield className="icon_authorise icon_action" />
                                        </a>
                                        <a href="#">
                                            <GrCircleInformation className=" icon_information icon_action" />
                                        </a>
                                        <a href="#">
                                            <MdDelete className="icon_delete icon_action" />
                                        </a>


                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <th scope="row">1</th>
                                <td>Trần Tiến Đạt</td>
                                <td>Nam</td>
                                <td>0123654987</td>
                                <td>Tim mạch</td>
                                <td >
                                    <div className="list_Action">
                                        <a href="#">
                                            <FaUserShield className="icon_authorise icon_action" />
                                        </a>
                                        <a href="#">
                                            <GrCircleInformation className=" icon_information icon_action" />
                                        </a>
                                        <a href="#">
                                            <MdDelete className="icon_delete icon_action" />
                                        </a>


                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <th scope="row">1</th>
                                <td>Trần Tiến Đạt</td>
                                <td>Nam</td>
                                <td>0123654987</td>
                                <td>Tim mạch</td>
                                <td >
                                    <div className="list_Action">
                                        <a href="#">
                                            <FaUserShield className="icon_authorise icon_action" />
                                        </a>
                                        <a href="#">
                                            <GrCircleInformation className=" icon_information icon_action" />
                                        </a>
                                        <a href="#">
                                            <MdDelete className="icon_delete icon_action" />
                                        </a>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">1</th>
                                <td>Trần Tiến Đạt</td>
                                <td>Nam</td>
                                <td>0123654987</td>
                                <td>Tim mạch</td>
                                <td >
                                    <div className="list_Action">
                                        <a href="#">
                                            <FaUserShield className="icon_authorise icon_action" />
                                        </a>
                                        <a href="#">
                                            <GrCircleInformation className=" icon_information icon_action" />
                                        </a>
                                        <a href="#">
                                            <MdDelete className="icon_delete icon_action" />
                                        </a>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">1</th>
                                <td>Trần Tiến Đạt</td>
                                <td>Nam</td>
                                <td>0123654987</td>
                                <td>Tim mạch</td>
                                <td >
                                    <div className="list_Action">
                                        <a href="#">
                                            <FaUserShield className="icon_authorise icon_action" />
                                        </a>
                                        <a href="#">
                                            <GrCircleInformation className=" icon_information icon_action" />
                                        </a>
                                        <a href="#">
                                            <MdDelete className="icon_delete icon_action" />
                                        </a>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    {/* pagination */}
                    <nav aria-label="Page navigation example" className="contain_pagination">
                        <ul className="pagination">
                            <li className="page-item">
                                <a className="page-link page-link-two" href="#" aria-label="Previous">
                                    <span aria-hidden="true">&laquo;</span>
                                </a>
                            </li>
                            <li className="page-item active"><a className="page-link" href="#">1</a></li>
                            <li className="page-item"><a className="page-link" href="#">2</a></li>
                            <li className="page-item"><a className="page-link" href="#">3</a></li>
                            <li className="page-item">
                                <a className="page-link page-link-two" href="#" aria-label="Next">
                                    <span aria-hidden="true">&raquo;</span>
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </div>
    );
}
