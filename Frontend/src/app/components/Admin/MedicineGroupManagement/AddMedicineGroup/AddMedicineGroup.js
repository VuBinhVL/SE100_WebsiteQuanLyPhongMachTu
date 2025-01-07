import React, { useEffect } from "react";
import { IoMdAddCircleOutline } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react";
import "./AddMedicineGroup.css";
import { fetchGet, fetchPost } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";

export default function AddMedicineGroup(props) {
  const { listPatien, setListPatien } = props;
  const [dataForm, setDataForm] = useState({ id: 0 });

  const handleChange = (e) => {
    const value = e.target.value;
    const name = e.target.name;
    setDataForm({
      ...dataForm,
      [name]: value,
    });
  };
  // Hàm lấy danh sách loại thuốc
  const fetchPatienList = () => {
    const uri = "/api/admin/quan-li-loai-thuoc";
    fetchGet(
      uri,
      (data) => {
        setListPatien(data); // Cập nhật danh sách loại thuốc
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("An error occurred while fetching patient list.");
      }
    );
  };
  const handleSubmit = (event) => {
    event.preventDefault();
    const { tenLoaiThuoc, id } = dataForm;

    // Validate dữ liệu trước khi gửi
    if (!tenLoaiThuoc) {
      showErrorMessageBox("Cần nhập đầy đủ thông tin");
      return;
    }

    addPatien();
  };

  // hàm clear data được fill trong modal
  const handleClearData = () => {
    // Clear data
    setDataForm({});
    const inputs = document.querySelectorAll(
      "#staticBackdrop input, #staticBackdrop select"
    );
    inputs.forEach((input) => {
      input.value = "";
    });
    const selects = document.querySelectorAll("#staticBackdrop select");
    selects.forEach((select) => {
      select.value = "DEFAULT";
    });
  };

  const addPatien = async () => {
    const uri = "/api/admin/quan-li-loai-thuoc/add";
    await fetchPost(
      uri,
      dataForm,
      (sus) => {
        showSuccessMessageBox(sus.message);
        const btnCancel = document.querySelector(".btn_Cancel");
        btnCancel.click();
        // Clear data
        handleClearData();
        // lấy lại data
        fetchPatienList();
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  };

  const handleClose = () => {
    handleClearData();
  };

  return (
    <>
      {/* <!-- Button trigger modal --> */}
      <button
        type="button"
        className="Add_Patien col-2 rounded-2 d-flex align-items-center justify-content-center"
        data-bs-toggle="modal"
        data-bs-target="#staticBackdrop"
      >
        <span>
          <IoMdAddCircleOutline className="fs-4 me-2" />
        </span>
        Thêm loại thuốc
      </button>

      {/* <!-- Modal --> */}
      <div
        className="modal fade"
        id="staticBackdrop"
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
                Thêm loại thuốc
              </h5>
            </div>
            <div className="modal-body d-flex justify-content-center">
              <form className="me-5 w-75">
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="tenLoaiThuoc"
                    className="form-label col-4 custom-bold"
                  >
                    Tên loại thuốc:
                  </label>
                  <input
                    name="tenLoaiThuoc"
                    id="tenLoaiThuoc"
                    type="text"
                    className="form-control rounded-3"
                    placeholder="Nhập tên loại thuốc"
                    onChange={handleChange}
                  />
                </div>
              </form>
            </div>
            <div className="modal-footer">
              <button
                type="button"
                onClick={handleClose}
                className="btn btn-secondary btn_Cancel"
                data-bs-dismiss="modal"
              >
                Hủy
              </button>
              <button
                type="button"
                onClick={handleSubmit}
                className="btn btn-primary btn_Accept"
              >
                Thêm
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
