import React, { useEffect } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react";
import { TiEdit } from "react-icons/ti";
import "./DetailShift.css";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
import { formatDate } from "../../../../utils/FormatDate/FormatDate";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";

export default function DetailShift(props) {
  // state quản lý trạng thái có thể edit information
  const [editStatus, setEditStatus] = useState(false);
  // data chi tiết bệnh nhân gốc (được gọi từ api)
  const [informationShift, setInformationShift] = useState({});
  // item truyền từ props qua
  const { listPatien, setListPatien, item } = props;
  const { specialization, setSpecialization } = props;
  // state quản lý data của formform
  const [dataForm, setDataForm] = useState({});
  // console.log(">>>>>>>check item", item)
  useEffect(() => {
    const uri = `/api/admin/quan-li-ca-kham/detail?id=${item.id}`;
    fetchGet(
      uri,
      (sus) => {
        setInformationShift(sus);
        // lấy data gán vào cho newInformation
        const {
          chuyenMon,
          chuyenMonId,
          hoSoBenhAns,
          isLock,
          matKhau,
          suChoPheps,
          tenTaiKhoan,
          vaiTro,
          vaiTroId,
          ...newInformation
        } = sus;
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
  useEffect(() => {
    const uri = "/api/admin/quan-li-nhom-benh";
    fetchGet(
      uri,
      (sus) => {
        setSpecialization(sus);
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
    const {
      chuyenMon,
      chuyenMonId,
      hoSoBenhAns,
      isLock,
      matKhau,
      suChoPheps,
      tenTaiKhoan,
      vaiTro,
      vaiTroId,
      ...newInformation
    } = informationShift;
    setDataForm(newInformation);
  };
  const handleSubmit = (event) => {
    event.preventDefault();
    const { hoTen, gioiTinh, ngaySinh, soDienThoai, email, diaChi, image, id } =
      dataForm;
    // Validate dữ liệu trước khi gửi
    if (
      !hoTen ||
      !gioiTinh ||
      !ngaySinh ||
      !soDienThoai ||
      !email ||
      !diaChi ||
      !image
    ) {
      // alert("Please fill in all the required fields!");
      showErrorMessageBox("Please fill in all the required fields!");
      return;
    }

    if (!email.endsWith("@gmail.com")) {
      // alert("Email must end with @gmail.com");
      showErrorMessageBox("Email must end with @gmail.com");
      return;
    }
    // Kiểm tra số điện thoại
    if (!/^\d+$/.test(soDienThoai)) {
      // alert("Phone number must be a number");
      showErrorMessageBox("Phone number must be a number");
      return;
    }

    // Kiểm tra độ dài số điện thoại
    if (soDienThoai.length < 10 || soDienThoai.length > 11) {
      // alert("Phone number must be between 10 and 11 digits");
      showErrorMessageBox("Phone number must be between 10 and 11 digits");
      return;
    }
    // Kiểm tra ngày sinh
    const ngaySinhDate = new Date(ngaySinh);
    const currentDate = new Date();
    const minDate = new Date("1950-01-01");

    if (ngaySinhDate >= currentDate) {
      // alert("Date of birth must be before today");
      showErrorMessageBox("Date of birth must be before today");
      return;
    }

    if (ngaySinhDate < minDate) {
      // alert("Date of birth must be after 1950");
      showErrorMessageBox("Date of birth must be after 1950");
      return;
    }
    // Kiểm tra tính duy nhất của số điện thoại và email
    for (const element of listPatien) {
      if (element.soDienThoai === soDienThoai && element.id !== id) {
        // alert("Phone number already exists");
        showErrorMessageBox("Phone number already exists");
        return;
      }
      if (element.email === email && element.id !== id) {
        // alert("Email already exists");
        showErrorMessageBox("Email already exists");
        return;
      }
    }

    EditPatient();
  };
  console.log(">>>>>>>>check listPatien", listPatien);
  // Hàm lấy danh sách nhân viên
  const fetchPatientList = () => {
    const uri = "/api/admin/quan-li-benh-nhan";
    fetchGet(
      uri,
      (data) => {
        setListPatien(data); // Cập nhật danh sách nhân viên
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("An error occurred while fetching patient list.");
      }
    );
  };
  const EditPatient = () => {
    const uri = "/api/admin/quan-li-benh-nhan/update";
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
        // cập nhật ui ở trang quản lý nhân viên
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
                {editStatus ? "Edit Patient" : "Detail Patient"}
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
                    value={dataForm.hoTen}
                    onChange={handleChange}
                    readOnly={!editStatus}
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center position-relative">
                  <label
                    htmlFor="khungGio"
                    className="form-label col-4 custom-bold"
                  >
                    Time Slot:
                  </label>
                  {editStatus ? (
                    <select
                      id="khungGio"
                      name="khungGio"
                      className="form-control rounded-3 "
                      defaultValue={"DEFAULT"}
                      onChange={handleChange}
                    >
                      <option value="DEFAULT" hidden disabled>
                        Enter Time Slot
                      </option>
                      <option value="morning">07:00 - 11:00</option>
                      <option value="afternoon">13:00 - 17:00</option>
                    </select>
                  ) : (
                    <input
                      type="text"
                      id="khungGio"
                      name="khungGio"
                      className="form-control rounded-3"
                      value={dataForm.gioiTinh}
                      readOnly
                    />
                  )}
                  <IoIosArrowDown className="position-absolute end-0 me-3" />
                </div>
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="ngayKham"
                    className="form-label col-4 custom-bold"
                  >
                    Consultation Date:
                  </label>
                  {editStatus ? (
                    <input
                      type="date"
                      id="ngayKham"
                      name="ngayKham"
                      className="form-control rounded-3"
                      defaultValue={formatDate(dataForm.ngaySinh)}
                      onChange={handleChange}
                      readOnly={!editStatus}
                    />
                  ) : (
                    <input
                      type="date"
                      id="ngayKham"
                      name="ngayKham"
                      className="form-control rounded-3"
                      value={formatDate(dataForm.ngaySinh)}
                      onChange={handleChange}
                      readOnly={!editStatus}
                    />
                  )}
                </div>
                <div className="form-group mb-3 d-flex align-items-center position-relative">
                  <label
                    htmlFor="tenNhomBenh"
                    className="form-label col-4 custom-bold"
                  >
                    Disease Group:
                  </label>
                  {editStatus ? (
                    <select
                      id="tenNhomBenh"
                      name="tenNhomBenh"
                      className="form-control rounded-3"
                      value={dataForm.chuyenMonId}
                      onChange={handleChange}
                    >
                      {specialization &&
                        specialization.length > 0 &&
                        specialization.map((item) => (
                          <option key={item.id} value={item.id}>
                            {item.tenNhomBenh}
                          </option>
                        ))}
                    </select>
                  ) : (
                    <input
                      type="text"
                      id="tenNhomBenh"
                      name="tenNhomBenh"
                      className="form-control rounded-3"
                      value={
                        specialization.find(
                          (spec) => spec.id == dataForm.tenNhomBenh
                        )?.tenNhomBenh || "ABC"
                      }
                      readOnly={!editStatus}
                    />
                  )}
                  <IoIosArrowDown className="position-absolute end-0 me-3" />
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
                    value={dataForm.email}
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
                    value={dataForm.diaChi}
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
              <div className="contain_Edit d-flex align-items-center mb-3 ms-3">
                <h4 className="title_edit fs-6 mb-0 me-2">
                  Edit patient information
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
