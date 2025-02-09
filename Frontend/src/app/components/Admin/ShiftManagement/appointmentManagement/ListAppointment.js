import React, { useEffect, useState } from "react";
import { IoArrowForwardCircleOutline } from "react-icons/io5";
import "./ListAppointment.css"
import { GrCircleInformation } from "react-icons/gr";
import { MdAddCircle } from "react-icons/md";
import { fetchGet, fetchPost } from "../../../../lib/httpHandler";
import { IoIosSearch } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";

import DetailAppointment from "../DetailAppointment/DetailAppointment";
import * as bootstrap from 'bootstrap';
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
export default function ListAppointment(props) {
    const { item } = props;
    const idModal = `idModal${item.id}`;
    const idspecificModal = `#idModal${item.id}`;
    const [listAppoinment, setListAppoinment] = useState([]);
    const [listAppoinmentShow, setListAppoinmentShow] = useState([]);
    const [dataSearch, setDataSearch] = useState("");
    const [listStatus, setListStatus] = useState([]);
    const [filterStatus, setFilterStatus] = useState("DEFAULT");
    const [selectedAppointment, setSelectedAppointment] = useState(null);
    // Lấy danh sách lịch khám
    useEffect(() => {
        const uri = `/api/admin/quan-li-lich-kham?CaKhamId=${item.id}`;
        fetchGet(
            uri,
            (sus) => {
                setListAppoinment(sus);

            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, []);
    // Lấy danh sách trạng thái
    useEffect(() => {
        const uri = "/api/admin/quan-li-lich-kham/trang-thai-lich-kham";
        fetchGet(
            uri,
            (sus) => {
                setListStatus(sus);

            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, []);
    // cập nhật danh sách lịch khám hiển thị khi lịch khám thay đổi 
    useEffect(() => {

        setListAppoinmentShow(listAppoinment);
        // setListTimeSlot(slot);
        // applyFilterAndSearch(dataSearch, filterTimeSlot, daySearch);
    }, [listAppoinment]);
    // Hàm xử lý tìm kiếm
    const handleSearch = (e) => {
        const value = e.target.value;
        setDataSearch(value);
        applyFilterAndSearch(value, filterStatus);
    };
    // Hàm xử lý lọc
    const handleFilter = (e) => {
        const value = e.target.value;
        setFilterStatus(value);
        applyFilterAndSearch(dataSearch, value);
    };

    // Hàm áp dụng tìm kiếm và lọc
    const applyFilterAndSearch = (searchValue, filterValue) => {
        let filteredList = [...listAppoinment];

        // Lọc theo chuyên môn
        if (filterValue !== "DEFAULT" && filterValue !== "Tất cả") {
            filteredList = filteredList.filter(
                (item) => item.tenTrangThai === filterValue
            );
        }
        // console.log(">>>>>>>>>>>check searchValue", searchValue);
        // console.log(">>>>>>>>>>>check filteredList", filteredList);
        // Tìm kiếm theo họ tên hoặc số thứ tự
        if (searchValue.trim()) {
            const lowercasedSearch = searchValue.toLowerCase();

            filteredList = filteredList.filter(
                (item) =>
                    item.tenBenhNhan.toLowerCase().includes(lowercasedSearch) ||
                    String(item.stt) === lowercasedSearch.trim()
            );
        }

        setListAppoinmentShow(filteredList);
    };
    const handleOpenDetailAppointment = async (appointment) => {
        await setSelectedAppointment(appointment);
        const detailModal = new bootstrap.Modal(document.getElementById(`detailAppointmentModal_${appointment.id}`));
        detailModal.show();
    };
    const fetchAppointmentList = () => {
        const uri = `/api/admin/quan-li-lich-kham?CaKhamId=${item.id}`;
        fetchGet(
            uri,
            (sus) => {
                setListAppoinment(sus);

            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }
    const AddExamination = (appointment) => {
        const uri = "/api/admin/quan-li-phieu-kham-benh/add";
        const data = {
            LichKhamId: appointment.id
        }
        fetchPost(
            uri,
            data
            ,
            (sus) => {
                showSuccessMessageBox(sus.message);
                // lấy lại data
                fetchAppointmentList();
            },
            (fail) => {
                console.log(">>>>>>>>.check fail", fail)
                showErrorMessageBox(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }
    const handleAddEmxaminationForm = (appointment) => {
        const status = "Đang chờ"
        if (appointment.tenTrangThai.trim().toLowerCase() === status.trim().toLowerCase()) {
            AddExamination(appointment)
        } else {
            showErrorMessageBox("Lịch khám này đã có phiếu khám bệnh")
            return;
        }

    }
    console.log(">>>>>>>>>>>check listAppointment", listAppoinment);
    return (
        <>
            {/* <!-- Button trigger modal --> */}
            <a href="#" className="contain_icon_listPointment" title="List Apppoinment">
                <IoArrowForwardCircleOutline className="icon_listPointment me-3" data-bs-toggle="modal" data-bs-target={idspecificModal} />
            </a>
            {/* <!-- Modal --> */}
            <div className="listPointment modal fade" id={idModal} data-bs-backdrop="static" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog modal-lg">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title fs-4" id="staticBackdropLabel">List Apppoinment</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body d-flex justify-content-center align-items-center flex-column">
                            <div className="d-flex align-items-center col-12 my-2">
                                <div className="contain_Search position-relative me-3">
                                    <input
                                        onChange={handleSearch}
                                        value={dataSearch}
                                        className="search rounded-2 px-3 me-1"
                                        placeholder="Enter appointment number or Patient's name"
                                    />
                                    <IoIosSearch className="icon_search translate-middle-y text-secondary" />
                                </div>
                                <div className="form-group d-flex align-items-center position-relative me-3">
                                    <select id="khungGio" name="khungGio" className="form-control rounded-3 py-2 TimeSlot" defaultValue={'DEFAULT'} onChange={handleFilter}>
                                        <option value="DEFAULT" hidden disabled>Filter by Status</option>
                                        <option value="Tất cả">Tất cả</option>
                                        {
                                            listStatus && listStatus.length > 0 && listStatus.map((item, index) => (
                                                <option key={item.id} value={item.tenTrangThai}>{item.tenTrangThai}</option>
                                            ))
                                        }
                                    </select>
                                    <IoIosArrowDown className="position-absolute end-0 me-3" />

                                </div>
                            </div>
                            <div className="contain_Table mx-0 col-12 bg-white rounded-2">
                                <table className="table table-hover">
                                    <thead>
                                        <tr>
                                            <th>Appointment Number</th>
                                            <th>Patient's name</th>
                                            <th>Status</th>
                                            <th>Note</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {listAppoinmentShow && listAppoinmentShow.length > 0 && listAppoinmentShow.map((item, index) => (
                                            <tr key={item.id}>
                                                <td>{item.stt}</td>
                                                <td>{item.tenBenhNhan}</td>
                                                <td>{item.tenTrangThai}</td>
                                                <td>{"Không có"}</td>
                                                <td>
                                                    <div className="list_Action">
                                                        <a href="#" onClick={() => handleOpenDetailAppointment(item)}>
                                                            <GrCircleInformation className="icon_information icon_action" />
                                                        </a>
                                                        <a href="#" onClick={() => handleAddEmxaminationForm(item)}>
                                                            <MdAddCircle className="icon_information icon_action" />
                                                        </a>
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
                </div>
            </div>
            {/* Modal Detail Appointment */}
            {selectedAppointment && (
                <div>
                    <DetailAppointment appointment={selectedAppointment} />
                </div>
            )}
        </>
    );
}