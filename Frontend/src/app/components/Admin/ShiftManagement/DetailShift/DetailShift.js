import React, { useEffect } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { useState } from "react";
import { TiEdit } from "react-icons/ti";
import "./DetailShift.css";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
import { formatDate } from "../../../../utils/FormatDate/formatDate";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";

export default function DetailShift(props) {
  // state quản lý trạng thái có thể edit information
  const [editStatus, setEditStatus] = useState(false);
  // data chi tiết bệnh nhân gốc (được gọi từ api)
  const [informationShift, setInformationShift] = useState({});
  // item truyền từ props qua
  const { listShift, setListShift, item } = props;
  // state quản lý data của form
  const [dataForm, setDataForm] = useState({});
  // console.log(">>>>>>>check item", item)
  useEffect(() => {
    const uri = `/api/admin/quan-li-ca-kham/detail?id=${item.id}`;
    // const uri = "/api/admin/quan-li-ca-kham/detail?id=2";
    fetchGet(
      uri,
      (sus) => {
        setInformationShift(sus);
        setDataForm(sus);
        console.log(">>>>>>> Chạy vào detail")
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, [listShift]);
  const handleChange = (e) => {
    const value = e.target.value;
    const name = e.target.name;
    setDataForm({
      ...dataForm,
      [name]: value,
    });
  };

  const handleEditInformation = () => {
    setEditStatus(!editStatus);
  };
  const handleCancel = () => {
    // quay lại trạng thái detail information
    setEditStatus(!editStatus);
    // clear data đã thay đổi
    setDataForm(informationShift);
  };
  const handleSubmit = (event) => {
    event.preventDefault();
    const { soLuongBenhNhanToiDa } = dataForm;
    // Validate dữ liệu trước khi gửi
    if (
      !soLuongBenhNhanToiDa
    ) {
      showErrorMessageBox("Please fill in all the required fields!");
      return;
    }
    if (soLuongBenhNhanToiDa <= 0) {
      showErrorMessageBox("The number of patients must be greater than 0!");
      return;
    }
    if (soLuongBenhNhanToiDa < item.soLuongBenhNhanDaDangKi) {
      showErrorMessageBox("The number of patients must be greater than or equal to the number of registered patients.!");
      return;
    }
    EditShift();
  };
  // Hàm lấy danh sách ca khám
  const fetchShiftList = () => {
    const uri = "/api/admin/quan-li-ca-kham";
    fetchGet(
      uri,
      (data) => {
        setListShift(data); // Cập nhật danh sách nhân viên
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("An error occurred while fetching patient list.");
      }
    );
  };
  const EditShift = () => {
    const uri = "/api/admin/quan-li-ca-kham/edit";
    const newDataForm = dataForm;
    const { bacSi, lichKhams, nhomBenh, ...updatedDataForm } = dataForm;
    fetchPut(
      uri,
      updatedDataForm,
      (sus) => {
        // // Đóng modal
        // alert(sus.message);
        showSuccessMessageBox(sus.message);
        // Cập nhật dataForm với giá trị mới
        setDataForm(newDataForm);
        // quay lại trang detail information
        handleEditInformation();
        // cập nhật ui ở trang ca khám 
        fetchShiftList();
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  };
  const handleClsoe = () => {
    setEditStatus(false);
  };
  const convertTimeToSlot = (timeStart, timeEnd) => {
    if (!timeStart || !timeEnd) {
      return "";
    }
    return `${timeStart.slice(0, 5)} - ${timeEnd.slice(0, 5)}`;
  };
  const idModal = `idModal${item.id}_${item.id}`;
  const idspecificModal = `#idModal${item.id}_${item.id}`;
  console.log(">>>>>>>.check DataForm", dataForm)
  // console.log(">>>>>>>.check informationShift", informationShift)

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
        className="detailShift modal fade"
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
                {editStatus ? "Edit Shift" : "Detail Shift"}
              </h5>
              <button
                type="button"
                className="btn-close"
                data-bs-dismiss="modal"
                aria-label="Close"
                onClick={handleClsoe}
              ></button>
            </div>
            <div className="modal-body d-flex justify-content-center">
              <form className="w-75">
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="tenCaKham"
                    className="form-label col-4 custom-bold"
                  >
                    Medical Shift:
                  </label>
                  <input
                    className="form-control rounded-3"
                    name="tenCaKham"
                    id="tenCaKham"
                    type="text"
                    placeholder="Enter full name"
                    value={dataForm.tenCaKham}
                    readOnly
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center position-relative">
                  <label
                    htmlFor="khungGio"
                    className="form-label col-4 custom-bold"
                  >
                    Time Slot:
                  </label>
                  <input
                    type="text"
                    id="khungGio"
                    name="khungGio"
                    className="form-control rounded-3"
                    value={convertTimeToSlot(dataForm.thoiGianBatDau, dataForm.thoiGianKetThuc)}
                    readOnly
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="ngayKham"
                    className="form-label col-4 custom-bold"
                  >
                    Consultation Date:
                  </label>

                  <input
                    type="date"
                    id="ngayKham"
                    name="ngayKham"
                    className="form-control rounded-3"
                    value={formatDate(dataForm.ngayKham)}
                    readOnly
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center position-relative">
                  <label
                    htmlFor="tenNhomBenh"
                    className="form-label col-4 custom-bold"
                  >
                    Disease Group:
                  </label>
                  <input
                    type="text"
                    id="tenNhomBenh"
                    name="tenNhomBenh"
                    className="form-control rounded-3"
                    value={item.tenNhomBenh}
                    readOnly
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="soLuongBenhNhanToiDa"
                    className="form-label col-4 custom-bold"
                  >
                    number of patients:
                  </label>
                  <input
                    name="soLuongBenhNhanToiDa"
                    id="soLuongBenhNhanToiDa"
                    type="text"
                    className="form-control rounded-3"
                    placeholder="Enter your Email"
                    value={dataForm.soLuongBenhNhanToiDa}
                    onChange={handleChange}
                    readOnly={!editStatus}
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="bacSiId"
                    className="form-label col-4 custom-bold"
                  >
                    Doctor:
                  </label>
                  <input
                    name="bacSiId"
                    id="bacSiId"
                    type="text"
                    className="form-control rounded-3"
                    placeholder="Enter your address"
                    value={item.bacSiKham}
                    readOnly
                  />
                </div>
              </form>
            </div>
            {editStatus ? (
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary btn_Cancel"
                  onClick={handleCancel}
                >
                  Cancel
                </button>
                <button
                  type="button"
                  className="btn-primary btn_Accept"
                  onClick={handleSubmit}
                >
                  Accept
                </button>
              </div>
            ) : (
              <div className="contain_Edit d-flex align-items-center mb-5">
                <h4 className="title_edit fs-6 mb-0 me-2">
                  Edit shift information
                </h4>
                <button
                  className="bg-white border-0 p-0"
                  onClick={handleEditInformation}
                >
                  <TiEdit className="fs-3 icon_edit_information" />
                </button>
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
}
