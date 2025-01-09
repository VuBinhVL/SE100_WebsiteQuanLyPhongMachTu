import React, { useEffect, useState } from "react";
import "./DetailPathology.css";
import { fetchGet } from "../../../../lib/httpHandler.js";
import * as bootstrap from 'bootstrap';

export default function DetailPathology(props) {
    const { pathology } = props;
    const [informationPathology, setInformationPathology] = useState({});

    useEffect(() => {
        if (pathology && pathology.id) {
            const uri = `/api/admin/quan-li-chi-tiet-kham-benh/detail?id=${pathology.id}`;
            fetchGet(
                uri,
                (sus) => {
                    setInformationPathology(sus);
                },
                (fail) => {
                    alert(fail.message);
                },
                () => {
                    alert("Có lỗi xảy ra");
                }
            );
        }
    }, [pathology]);

    useEffect(() => {
        if (pathology && pathology.id) {
            const modalElement = document.getElementById(`detailPathologyModal_${pathology.id}`);
            const bootstrapModal = new bootstrap.Modal(modalElement);
            bootstrapModal.show();
        }
    }, [pathology]);

    return (
        <div
            className="detailPathology modal fade"
            id={`detailPathologyModal_${pathology.id}`}
            data-bs-backdrop="static"
            data-bs-keyboard="false"
            tabIndex="-1"
            aria-labelledby="staticBackdropLabel"
            aria-hidden="true"
            style={{ zIndex: 1055 }}
        >
            <div className="modal-dialog modal-lg">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title fs-4" id="staticBackdropLabel">
                            Detail Pathology
                        </h5>
                        <button
                            type="button"
                            className="btn-close"
                            data-bs-dismiss="modal"
                            aria-label="Close"
                        ></button>
                    </div>
                    <div className="modal-body d-flex justify-content-center flex-column align-items-center">
                        <div className="formOne">
                            <h3>1. Thông tin bệnh nhân</h3>
                            <form className="w-100">
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label
                                        htmlFor="tenBenhNhan"
                                        className="form-label col-4 custom-bold"
                                    >
                                        Họ và tên:
                                    </label>
                                    <input
                                        className="form-control rounded-3"
                                        name="tenBenhNhan"
                                        id="tenBenhNhan"
                                        type="text"
                                        placeholder="Enter full name"
                                    />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center position-relative">
                                    <label
                                        htmlFor="gioiTinh"
                                        className="form-label col-4 custom-bold"
                                    >
                                        Giới tính:
                                    </label>
                                    <input
                                        type="text"
                                        id="gioiTinh"
                                        name="gioiTinh"
                                        className="form-control rounded-3"
                                    />
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}