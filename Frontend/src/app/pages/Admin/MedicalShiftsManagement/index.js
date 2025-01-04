import React, { useEffect, useState } from "react";
import { IoIosSearch } from "react-icons/io";
import { IoArrowForwardCircleOutline } from "react-icons/io5";
import { IoIosArrowDown } from "react-icons/io";
import { fetchGet } from "../../../lib/httpHandler";
import { formatDate } from "../../../utils/FormatDate/formatDate.js";
import "./MedicalShift.css";
import AddShift from "../../../components/Admin/ShiftManagement/AddShift/AddShift";
import { FaCalendarAlt } from "react-icons/fa";
export default function MedicalShift() {
    const [listShift, setListShift] = useState([]);
    const [listShiftShow, setListShiftShow] = useState([]);
    const [listTimeSlot, setListTimeSlot] = useState([]);
    const [filterTimeSlot, setFilterTimeSlot] = useState("DEFAULT");
    const [dataSearch, setDataSearch] = useState("");

    // hàm lấy ngày hiện tại
    const getCurrentDate = () => {
        const date = new Date();
        const year = date.getFullYear();
        const month = date.getMonth() + 1;
        const day = date.getDate();
        return `${year}-${month < 10 ? `0${month}` : month}-${day < 10 ? `0${day}` : day}`;
    };
    const [daySearch, setDaySearch] = useState(getCurrentDate());
    // hàm này dùng để khi load trang lên thì nó sẽ tự động search theo ngày hiện tại

    // Lấy danh sách ca khám 
    useEffect(() => {
        const uri = "/api/admin/quan-li-ca-kham";
        fetchGet(
            uri,
            (sus) => {
                setListShift(sus);

            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, []);
    // Cập nhật danh sách hiển thị khi listShift thay đổi
    useEffect(() => {
        const slot = getSlot();
        setListShiftShow(listShift);
        setListTimeSlot(slot);
        applyFilterAndSearch(dataSearch, filterTimeSlot, getCurrentDate());
    }, [listShift]);

    //hàm chuyển đổi thời gian bắt đầu, kết thúc thành khung giờ
    const convertTimeToSlot = (timeStart, timeEnd) => {
        return `${timeStart.slice(0, 5)} - ${timeEnd.slice(0, 5)}`;
    };
    // hàm lấy các khung giờ 
    const getSlot = () => {
        const slot = listShift.map((item) => {
            return convertTimeToSlot(item.thoiGianBatDau, item.thoiGianKetThuc);
        });
        return slot;
    }
    // Hàm xử lý tìm kiếm
    const handleSearch = (e) => {
        const value = e.target.value;
        setDataSearch(value);
        applyFilterAndSearch(value, filterTimeSlot, daySearch);
    };

    // Hàm tìm kiếm theo ngày
    const handleSearchByDay = (e) => {
        const value = e.target.value;
        setDaySearch(value);
        applyFilterAndSearch(dataSearch, filterTimeSlot, value);
    };

    // Hàm xử lý lọc theo khung giờ 
    const handleFilter = (e) => {
        const value = e.target.value;
        setFilterTimeSlot(value);
        applyFilterAndSearch(dataSearch, value, daySearch);
    };

    // Hàm áp dụng tìm kiếm và lọc
    const applyFilterAndSearch = (searchValue, filterValue, dayValue) => {
        let filteredList = [...listShift];

        // Lọc theo chuyên môn
        if (filterValue !== "DEFAULT" && filterValue !== "Tất cả") {
            filteredList = filteredList.filter(
                (item) => convertTimeToSlot(item.thoiGianBatDau, item.thoiGianKetThuc) === filterValue
            );
        }

        // Tìm kiếm theo họ tên hoặc số điện thoại
        if (searchValue.trim()) {
            const lowercasedSearch = searchValue.toLowerCase();
            filteredList = filteredList.filter(
                (item) =>
                    item.bacSiKham.toLowerCase().includes(lowercasedSearch) ||
                    item.sdt.includes(lowercasedSearch)
            );
        }

        // Tìm kiếm theo ngày 
        if (dayValue.trim()) {
            filteredList = filteredList.filter(
                (item) => formatDate(item.ngayKham) === dayValue
            );
        }
        setListShiftShow(filteredList);
    };
    // console.log("daysearch", daySearch)
    // console.log("listShift", formatDate( listShift.ngayKham))
    return (
        <>
            <div className="Shift_Management">
                <div className="title py-3 fs-5 mb-2">
                    Total number of Medical Shift: {listShiftShow.length}
                </div>
                <div className="row mx-0 my-0">
                    <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
                        <div className="d-flex align-items-center col-10">
                            <div className="contain_Search position-relative me-3">
                                <input
                                    onChange={handleSearch}
                                    value={dataSearch}
                                    className="search rounded-2 px-3 me-1"
                                    placeholder="Enter doctor's name or phone number"
                                />
                                <IoIosSearch className="icon_search translate-middle-y text-secondary" />
                            </div>
                            <div className="form-group d-flex align-items-center position-relative me-3">
                                <select id="khungGio" name="khungGio" className="form-control rounded-3 py-2 TimeSlot" defaultValue={'DEFAULT'} onChange={handleFilter}>
                                    <option value="DEFAULT" hidden disabled>Filter by Time Slot</option>
                                    <option value="Tất cả">Tất cả</option>
                                    {
                                        listTimeSlot && listTimeSlot.length > 0 && listTimeSlot.map((item, index) => (
                                            <option key={index} value={item}>{item}</option>
                                        ))
                                    }
                                </select>
                                <IoIosArrowDown className="position-absolute end-0 me-3" />
                            </div>
                            <div className="form-group d-flex align-items-center">
                                <input type="date" id="ngayKham" name="ngayKham" className="form-control ConsultationDate py-2 rounded-3" value={daySearch} onChange={handleSearchByDay} />
                            </div>
                        </div>
                        <AddShift setListShift={setListShift} listShift={listShift} />
                    </div>
                    <div className="contain_Table mx-0 col-12 bg-white rounded-2">
                        <table className="table table-hover">
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Medical Shift</th>
                                    <th>Time slot</th>
                                    <th>Consultation Date</th>
                                    <th>Disease Group</th>
                                    <th>Doctor</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                {listShiftShow && listShiftShow.length > 0 && listShiftShow.map((item, index) => (
                                    <tr key={item.id}>
                                        <td>{index + 1}</td>
                                        <td>{item.tenCaKham}</td>
                                        <td>{convertTimeToSlot(item.thoiGianBatDau, item.thoiGianKetThuc)}</td>
                                        <td>{formatDate(item.ngayKham)}</td>
                                        <td>{item.tenNhomBenh}</td>
                                        <td>{item.bacSiKham}</td>
                                        <td>
                                            <div className="list_Action">
                                                {/* <FaLock className="icon_Lock icon_action fs-6" /> */}
                                                <IoArrowForwardCircleOutline />

                                                {/* <DetailPatien item={item} setListShift={setListShift} listShift={listShift} />
                                                <DeletePatien item={item} setListShift={setListShift} listShift={listShift} /> */}
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
            </div >
        </>
    );
}