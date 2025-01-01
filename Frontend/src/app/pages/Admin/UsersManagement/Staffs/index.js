import React, { useEffect, useState } from "react";
import "./Staff.css";
import { IoIosSearch } from "react-icons/io";
import { FaUserShield } from "react-icons/fa";
import AddStaff from "../../../../components/Admin/StaffManagement/AddStaff/AddStaff";
import DetailStaff from "../../../../components/Admin/StaffManagement/DetailStaff/DetailStaff";
import DeleteStaff from "../../../../components/Admin/StaffManagement/DeleteStaff/DeleteStaff";
import { fetchGet } from "../../../../lib/httpHandler";

export default function Staff() {
    const [listStaff, setListStaff] = useState([]);
    const [listStaffShow, setListStaffShow] = useState([]);
    const [listSpecialization, setListSpecialization] = useState([]);
    const [dataSearch, setDataSearch] = useState("");
    const [filterSpecialization, setFilterSpecialization] = useState("DEFAULT");

    // Lấy danh sách nhân viên
    useEffect(() => {
        const uri = "/api/admin/quan-li-nhan-vien";
        fetchGet(
            uri,
            (sus) => {
                setListStaff(sus);
            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, []);

    //gọi api lấy chuyên môn (nhóm bệnh)
    useEffect(() => {
        const uri = "/api/admin/quan-li-nhom-benh";
        fetchGet(
            uri,
            (sus) => {
                setListSpecialization(sus);
            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, []);

    // Cập nhật danh sách hiển thị khi listStaff thay đổi
    useEffect(() => {
        setListStaffShow(listStaff);
    }, [listStaff]);

    // Hàm xử lý tìm kiếm
    const handleSearch = (e) => {
        const value = e.target.value;
        setDataSearch(value);
        applyFilterAndSearch(value, filterSpecialization);
    };

    // Hàm xử lý lọc
    const handleFilter = (e) => {
        const value = e.target.value;
        setFilterSpecialization(value);
        applyFilterAndSearch(dataSearch, value);
    };

    // Hàm áp dụng tìm kiếm và lọc
    const applyFilterAndSearch = (searchValue, filterValue) => {
        let filteredList = [...listStaff];

        // Lọc theo chuyên môn
        if (filterValue !== "DEFAULT" && filterValue !== "Tất cả") {
            filteredList = filteredList.filter(
                (item) => item.tenChuyenMon === filterValue
            );
        }

        // Tìm kiếm theo họ tên hoặc số điện thoại
        if (searchValue.trim()) {
            const lowercasedSearch = searchValue.toLowerCase();
            filteredList = filteredList.filter(
                (item) =>
                    item.hoTen.toLowerCase().includes(lowercasedSearch) ||
                    item.soDienThoai.includes(lowercasedSearch)
            );
        }

        setListStaffShow(filteredList);
    };

    return (
        <div className="Staff_Management">
            <div className="title py-3 fs-5 mb-2">
                Total number of doctors: {listStaff.length}
            </div>
            <div className="row mx-0 my-0">
                <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
                    <div className="d-flex align-items-center col-10">
                        <div className="contain_Search position-relative col-4 me-3">
                            <input
                                onChange={handleSearch}
                                value={dataSearch}
                                className="search rounded-2 px-3"
                                placeholder="Enter your name or phone number"
                            />
                            <IoIosSearch className="icon_search translate-middle-y text-secondary" />
                        </div>

                        <select
                            className="filter col-3 px-3 me-3 rounded-2"
                            value={filterSpecialization}
                            onChange={handleFilter}
                        >
                            <option value="DEFAULT" hidden>
                                Filter by specialization
                            </option>
                            <option value="Tất cả">Tất cả</option>
                            {listSpecialization && listSpecialization.length > 0 && listSpecialization.map((item) => (
                                <option key={item.id} value={item.tenNhomBenh}>
                                    {item.tenNhomBenh}
                                </option>
                            ))}
                        </select>
                    </div>
                    <AddStaff setListStaff={setListStaff} listStaff={listStaff} />
                </div>
                <div className="contain_Table mx-0 col-12 bg-white rounded-2">
                    <table className="table table-hover">
                        <thead>
                            <tr>
                                <th>STT</th>
                                <th>Full name</th>
                                <th>Gender</th>
                                <th>Phone number</th>
                                <th>Specialization</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {listStaffShow.map((item, index) => (
                                <tr key={item.id}>
                                    <td>{index + 1}</td>
                                    <td>{item.hoTen}</td>
                                    <td>{item.gioiTinh}</td>
                                    <td>{item.soDienThoai}</td>
                                    <td>{item.tenChuyenMon}</td>
                                    <td>
                                        <div className="list_Action">
                                            <FaUserShield className="icon_authorise icon_action" />
                                            <DetailStaff item={item} setListStaff={setListStaff} listStaff={listStaff} />
                                            <DeleteStaff item={item} setListStaff={setListStaff} listStaff={listStaff} />
                                        </div>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                    <nav className="contain_pagination">
                        <ul className="pagination">
                            <li className="page-item">
                                <a className="page-link page-link-two" href="#">
                                    &laquo;
                                </a>
                            </li>
                            <li className="page-item active">
                                <a className="page-link" href="#">
                                    1
                                </a>
                            </li>
                            <li className="page-item">
                                <a className="page-link" href="#">
                                    2
                                </a>
                            </li>
                            <li className="page-item">
                                <a className="page-link" href="#">
                                    3
                                </a>
                            </li>
                            <li className="page-item">
                                <a className="page-link page-link-two" href="#">
                                    &raquo;
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </div>
    );
}

