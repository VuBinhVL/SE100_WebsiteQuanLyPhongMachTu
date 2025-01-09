import React, { useEffect } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { useState } from "react";
import "./AddExaminationDetailModal.css";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler.js";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox.js";
import { formatDate } from "../../../../utils/FormatDate/formatDate.js";
import * as bootstrap from 'bootstrap';
export default function AddExaminationDetailModal({ appointment }) {
    // const [informationAppointment, setInformationAppointment] = useState({});
    const [dataForm, setDataForm] = useState({});
    const handleChange = (e) => {
        const value = e.target.value;
        const name = e.target.name;
        setDataForm({
            ...dataForm, [name]: value
        })
    }
    console.log(">>>>>>>.check appointment", appointment);
    return (
        <>
            {/* <a href="#" data-bs-toggle="modal" data-bs-target="#listappointment">
        <GrCircleInformation className="icon_information icon_action" />
      </a> */}
            <div
                className="AddExaminationDetailModal modal fade"
                id={`detailAppointmentModal`}
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
                                Detail Appointment
                            </h5>
                            <button
                                type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                            ></button>
                        </div>
                        <div className="modal-body d-flex justify-content-center flex-column align-items-center">
                            <div className="formThree">
                                <form className="w-100">
                                    <div className="form-group mb-3 d-flex align-items-center">
                                        <label
                                            htmlFor="benhLyId"
                                            className="form-label col-4 custom-bold"
                                        >
                                            Tên bệnh lý:
                                        </label>
                                        <input
                                            className="form-control rounded-3"
                                            name="benhLyId"
                                            id="benhLyId"
                                            type="text"
                                            placeholder="Nhập tên bệnh lý"
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3 d-flex align-items-center position-relative">
                                        <label
                                            htmlFor="giaKham"
                                            className="form-label col-4 custom-bold"
                                        >
                                            Giá khám:
                                        </label>
                                        <input
                                            type="text"
                                            id="giaKham"
                                            name="giaKham"
                                            className="form-control rounded-3"
                                            placeholder="Nhập giá khám"
                                            onChange={handleChange}
                                        />
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div >
        </>
    );
}