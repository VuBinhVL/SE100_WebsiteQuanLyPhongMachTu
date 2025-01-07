import React, { useEffect, useState } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { TiEdit } from "react-icons/ti";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import "./DetailMedicineGroup.css";

export default function DetailMedicineGroup(props) {
  const [editStatus, setEditStatus] = useState(false);
  // data chi tiết bệnh nhân gốc (được gọi từ api)
  const [informationPatient, setInformationPatient] = useState({});
  // item truyền từ props qua
  const { listPatien, setListPatien, item } = props;
  // state quản lý data của formform
  const [dataForm, setDataForm] = useState({
    id: 0, // Lấy id  là 0
  });

  useEffect(() => {
    const uri = `/api/admin/quan-li-loai-thuoc/detail?id=${item.id}`;
    fetchGet(
      uri,
      (sus) => {
        setInformationPatient(sus);
        // lấy data gán vào cho newInformation
        const { tenLoaiThuoc, ...newInformation } = sus;
        setDataForm(newInformation);
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, []);

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
    const { tenLoaiThuoc, ...newInformation } = informationPatient;
    setDataForm(newInformation);
  };
  const handleSubmit = (event) => {
    event.preventDefault();
    const { tenLoaiThuoc } = dataForm;
    // Validate dữ liệu trước khi gửi
    if (!tenLoaiThuoc) {
      showErrorMessageBox("Hãy điền đầy đủ thông tin");
      return;
    }
    EditPatient();
  };

  // Hàm lấy danh sách
  const fetchPatientList = () => {
    const uri = "/api/admin/quan-li-loai-thuoc";
    fetchGet(
      uri,
      (data) => {
        setListPatien(data);
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("Máy chủ mất kết nối");
      }
    );
  };

  const EditPatient = () => {
    const uri = "/api/admin/quan-li-loai-thuoc/edit";
    const updatedDataForm = { ...dataForm }; // Lưu trữ giá trị mới trước khi gọi API
    fetchPut(
      uri,
      dataForm,
      (sus) => {
        // // Đóng modal
        // alert(sus.message);
        showSuccessMessageBox(sus.message);
        // Cập nhật dataForm với giá trị mới
        setDataForm(updatedDataForm);
        // quay lại trang detail information
        handleEditInformation();
        // cập nhật ui ở trang quản lý
        fetchPatientList();
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

  const idModal = `idModal${item.id}`;
  const idspecificModal = `#idModal${item.id}`;
  return (
    <>
      <a>
        <GrCircleInformation
          className="icon_information icon_action"
          data-bs-toggle="modal"
          data-bs-target={idspecificModal}
        />
      </a>
      <div
        className="detailMedicineGroup modal fade"
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
                {editStatus
                  ? "Sửa thông tin loại thuốc"
                  : "Thông tin loại thuốc"}
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
              <form className="me-5 w-75">
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="tenLoaiThuoc"
                    className="form-label col-4 custom-bold"
                  >
                    Tên loại thuốc:
                  </label>
                  <input
                    className="form-control rounded-3"
                    name="tenLoaiThuoc"
                    id="tenLoaiThuoc"
                    type="text"
                    value={dataForm.tenLoaiThuoc || item.tenLoaiThuoc}
                    onChange={handleChange}
                    readOnly={!editStatus}
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
                  Hủy
                </button>
                <button
                  type="button"
                  className="btn-primary btn_Accept"
                  onClick={handleSubmit}
                >
                  Lưu
                </button>
              </div>
            ) : (
              <div className="contain_Edit d-flex align-items-center mb-3 ms-3">
                <h4 className="title_edit fs-6 mb-0 me-2">
                  Chỉnh sửa thông tin
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
