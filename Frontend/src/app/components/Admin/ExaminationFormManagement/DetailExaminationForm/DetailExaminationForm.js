import React, { useEffect, useState } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { IoMdAddCircle } from "react-icons/io";
import "./DetailExaminationForm.css";
import { fetchGet } from "../../../../lib/httpHandler";
import { formatDate } from "../../../../utils/FormatDate/formatDate";
import { formatDateTime } from "../../../../utils/FormatDate/formatDateTime";
import { MdDelete } from "react-icons/md";
import { FaCheck } from "react-icons/fa";
import { MdEdit } from "react-icons/md";

export default function DetailExaminationForm(props) {
    const [inforDetailExamination, setInforDetailExamination] = useState({});
    const [details, setDetails] = useState([]);
    const [showAddModal, setShowAddModal] = useState(false);
    const [dataForm, setDataForm] = useState({ tenBenhLy: '', giaKham: '' });
    const { item } = props;

    const idModal = `idModalDetailExaminationForm${item.id}`;
    const idspecificModal = `#idModalDetailExaminationForm${item.id}`;

    // Lấy thông tin chi tiết phiếu khám bệnh
    useEffect(() => {
        const uri = `/api/admin/quan-li-phieu-kham-benh/detail?id=${item.id}`;
        fetchGet(
            uri,
            async (sus) => {
                await setInforDetailExamination(sus);
                await setDetails(sus.chiTietKhamBenhs || []);
            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, [item.id]);

    const handleOpenAdd = () => {
        setShowAddModal(true);
    };

    const handleCloseAdd = () => {
        setShowAddModal(false);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setDataForm({
            ...dataForm,
            [name]: value
        });
    };

    const handleSubmit = () => {
        const newDetail = { id: Date.now(), ...dataForm };
        setDetails([...details, newDetail]);
        handleCloseAdd();
    };

    return (
        <>
            <a href="#">
                <GrCircleInformation
                    className="icon_information icon_action"
                    data-bs-toggle="modal"
                    data-bs-target={idspecificModal}
                />
            </a>
            <div
                className="detailExaminationForm modal fade"
                id={idModal}
                data-bs-backdrop="static"
                data-bs-keyboard="false"
                tabIndex="-1"
                aria-labelledby="staticBackdropLabel"
                aria-hidden="true"
            >
                <div className="modal-dialog modal-lg">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title fs-4" id="staticBackdropLabel">
                                Detail Examination Form
                            </h5>
                            <button
                                type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                            ></button>
                        </div>
                        <div className="modal-body d-flex justify-content-center flex-column">
                            {/* Thông tin phiếu khám */}
                            <div className="personal_information mb-4 p-3 border rounded bg-light">
                                <h4 className="mb-3">1. Thông tin Bệnh nhân</h4>
                                <div className="record-info d-flex">
                                    <div className="patient-photo text-center me-4">
                                        <img
                                            src={inforDetailExamination.hinhAnhBenhNhan}
                                            alt="Ảnh bệnh nhân"
                                            className="rounded-circle mb-2"
                                        />
                                        <p>Ảnh bệnh nhân</p>
                                    </div>
                                    <div className="patient-details">
                                        <p><b>Họ tên: </b> {inforDetailExamination.hoTenBenhNhan}</p>
                                        <p><b>Giới tính: </b>{inforDetailExamination.gioiTinh}</p>
                                        <p><b>Ngày sinh: </b> {formatDate(inforDetailExamination.ngaySinh)}</p>
                                        <p><b>Địa chỉ: </b> {inforDetailExamination.diaChi}</p>
                                        <p><b>Thời gian khám: </b> {formatDateTime(inforDetailExamination.thoiGianKham)}</p>
                                        <p><b>Bác sĩ khám: </b> {inforDetailExamination.tenBacSiKham}</p>
                                    </div>
                                </div>
                            </div>

                            {/* Chi tiết khám bệnh */}
                            <div className="details-section mb-4 p-3 border rounded bg-light">
                                <h4 className="mb-3 d-flex align-items-center">
                                    2. Chi tiết khám bệnh
                                    <span className="ms-2">
                                        <a href="#" onClick={handleOpenAdd}>
                                            <IoMdAddCircle className="icon_Add" />
                                        </a>
                                    </span>
                                </h4>
                                <table className="table table-hover">
                                    <thead className="table-light">
                                        <tr>
                                            <th>STT</th>
                                            <th>Tên bệnh lý</th>
                                            <th>Giá khám</th>
                                            <th>Thao tác</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            details.map((item, index) => (
                                                <tr key={item.id}>
                                                    <td>{index + 1}</td>
                                                    <td>{item.tenBenhLy}</td>
                                                    <td>{item.giaKham}</td>
                                                    <td>
                                                        <a href="#">
                                                            <MdEdit
                                                                className="icon_edit icon_action" />
                                                        </a>
                                                        <a href="#" >
                                                            <GrCircleInformation className="icon_information icon_action" />
                                                        </a>
                                                        <a href="#" >
                                                            <MdDelete className="icon_delete icon_action" />
                                                        </a>

                                                    </td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>
                                </table>
                            </div>

                            {/* Tổng tiền */}
                            <div className="total-info mb-4 p-3 border rounded bg-light">
                                <h4 className="mb-3">3. Tổng chi phí</h4>
                                <p><b>Tiền xét nghiệm:</b> {inforDetailExamination.tienXetNghiem}</p>
                                <p><b>Tiền chụp chiếu:</b> {inforDetailExamination.tienChupChieu}</p>
                                <p><b>Tiền khám:</b> {inforDetailExamination.tienKham}</p>
                                <p><b>Tổng tiền thuốc:</b> {inforDetailExamination.tienThuoc}</p>
                                <p><b>Tổng:</b> {inforDetailExamination.tienXetNghiem + inforDetailExamination.tienChupChieu
                                    + inforDetailExamination.tienKham + inforDetailExamination.tienThuoc}</p>
                            </div>

                            {/* Footer */}
                            <div className="popup-footer text-end">
                                <button className="btn btn-primary">Xác nhận thanh toán</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {/* Modal thêm chi tiết khám bệnh */}
            <div className={`modal fade ${showAddModal ? 'show' : ''}`} id="addExaminationDetailModal" tabIndex="-1" aria-labelledby="addExaminationDetailModalLabel" aria-hidden={!showAddModal} style={{ display: showAddModal ? 'block' : 'none', zIndex: 1055 }}>
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="addExaminationDetailModalLabel">Thêm Chi Tiết Khám Bệnh</h5>
                            <button type="button" className="btn-close" onClick={handleCloseAdd}></button>
                        </div>
                        <div className="modal-body">
                            <div className="mb-3">
                                <label className="form-label">Tên Bệnh Lý</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    name="tenBenhLy"
                                    value={dataForm.tenBenhLy}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Giá Khám</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    name="giaKham"
                                    value={dataForm.giaKham}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" onClick={handleCloseAdd}>Đóng</button>
                            <button type="button" className="btn btn-primary" onClick={handleSubmit}>Lưu</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}