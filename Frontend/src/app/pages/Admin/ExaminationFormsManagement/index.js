import React, { useEffect, useState } from "react";
import { fetchGet } from "../../../lib/httpHandler";
import DetailExaminationForm from "../../../components/Admin/ExaminationFormManagement/DetailExaminationForm/DetailExaminationForm"
import DeleteExaminationForm from "../../../components/Admin/ExaminationFormManagement/DeleteExaminationForm/DeleteExaminationForm";
import { formatDateTime } from "../../../utils/FormatDate/formatDateTime";
import { IoIosSearch } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
export default function ExaminationForm() {
    const [listExaminationForm, setListExaminationForm] = useState([]);
    const [listExaminationFormShow, setListExaminationFormShow] = useState([]);
    const [listStatus, setListStatus] = useState([]);
    const [filterStatus, setFilterStatus] = useState("DEFAULT");
    const [dataSearch, setDataSearch] = useState("");
    // api lấy danh sách biểu mẫu khám bệnh
    useEffect(() => {
        const uri = "/api/admin/quan-li-phieu-kham-benh";
        fetchGet(
            uri,
            (sus) => {
                setListExaminationForm(sus);
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
    // Cập nhật danh sách hiển thị khi listExaminationForm thay đổi
    useEffect(() => {
        setListExaminationFormShow(listExaminationForm);
        // applyFilterAndSearch(dataSearch, filterTimeSlot, daySearch);
    }, [listExaminationForm]);
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
        let filteredList = [...listExaminationForm];

        // Lọc theo chuyên môn
        if (filterValue !== "DEFAULT" && filterValue !== "Tất cả") {
            filteredList = filteredList.filter(
                (item) => item.tenTrangThaiPKB === filterValue
            );
        }

        // Tìm kiếm theo họ tên hoặc số thứ tự
        if (searchValue.trim()) {
            const lowercasedSearch = searchValue.trim().toLowerCase();

            filteredList = filteredList.filter(
                (item) =>
                    item.tenBacSi.toLowerCase().includes(lowercasedSearch) ||
                    item.tenBenhNhan.toLowerCase().includes(lowercasedSearch)
            );
        }

        setListExaminationFormShow(filteredList);
    };
    console.log(">>>>>>>>>check listExaminationFormShow", listExaminationFormShow)
    return (
        <>
            <>
                <div className="Shift_Management">
                    <div className="title py-3 fs-5 mb-2">
                        Total number of Examination Forms: {listExaminationFormShow.length}
                    </div>
                    <div className="row mx-0 my-0">
                        <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
                            <div className="d-flex align-items-center col-10">
                                <div className="contain_Search position-relative me-3">
                                    <input
                                        onChange={handleSearch}
                                        value={dataSearch}
                                        className="search rounded-2 px-3 me-1"
                                        placeholder="Enter doctor's name or patient's name"
                                    />
                                    <IoIosSearch className="icon_search translate-middle-y text-secondary" />
                                </div>
                                <div className="form-group d-flex align-items-center position-relative me-3">
                                    <select
                                        id="khungGio"
                                        name="khungGio"
                                        className="form-control rounded-3 py-2 TimeSlot"
                                        defaultValue={"DEFAULT"}
                                        onChange={handleFilter}
                                    >
                                        <option value="DEFAULT" hidden disabled>
                                            Filter by status
                                        </option>
                                        <option value="Tất cả">Tất cả</option>
                                        {listStatus && listStatus.length > 0 && listStatus.map((item, index) => (
                                            <option key={item.id} value={item.tenTrangThai}>{item.tenTrangThai}</option>
                                        ))}
                                    </select>
                                    <IoIosArrowDown className="position-absolute end-0 me-3" />
                                </div>

                            </div>
                        </div>
                        <div className="contain_Table mx-0 col-12 bg-white rounded-2">
                            <table className="table table-hover">
                                <thead>
                                    <tr>
                                        <th>STT</th>
                                        <th>Patient's Name</th>
                                        <th>Phone Number</th>
                                        <th>Doctor</th>
                                        <th>Time create</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {listExaminationFormShow &&
                                        listExaminationFormShow.length > 0 &&
                                        listExaminationFormShow.map((item, index) => (
                                            <tr key={item.id}>
                                                <td>{index + 1}</td>
                                                <td>{item.tenBenhNhan}</td>
                                                <td>
                                                    {item.soDienThoai}
                                                </td>
                                                <td>{item.tenBacSi}</td>
                                                <td>{formatDateTime(item.ngayTao)}</td>

                                                <td>
                                                    <div className="list_Action">

                                                        {/* <ListAppointment item={item} /> */}
                                                        {/* <DetailAppointment /> */}
                                                        <DetailExaminationForm item={item} setListExaminationForm={setListExaminationForm} listExaminationForm={listExaminationForm} />
                                                        <DeleteExaminationForm
                                                            item={item}
                                                            setListExaminationForm={setListExaminationForm}
                                                            listExaminationForm={listExaminationForm}
                                                        />
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
            </>
        </>
    );
}
