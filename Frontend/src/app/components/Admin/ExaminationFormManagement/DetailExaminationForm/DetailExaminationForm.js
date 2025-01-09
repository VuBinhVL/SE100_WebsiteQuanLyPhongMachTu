import React, { useEffect } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react";
import { TiEdit } from "react-icons/ti";
import "./DetailExaminationForm.css";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
import { formatDate } from "../../../../utils/FormatDate/formatDate";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";

export default function DetailExaminationForm(props) {

    const { item } = props

    const idModal = `idModal${item.id}`;
    const idspecificModal = `#idModal${item.id}`;
    // console.log(">>>>>>>.check DataForm", dataForm)
    // console.log(">>>>>>>.check specialization", specialization)

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
                                Detail
                            </h5>
                            <button
                                type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"

                            ></button>
                        </div>
                        <div className="modal-body d-flex justify-content-center">
                            xxxxxxxxxxxxxxxx
                        </div>

                    </div>
                </div>
            </div>
        </>
    );
}
