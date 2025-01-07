import React, { useEffect } from "react";
import { IoMdAddCircleOutline } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react";
import "./AddShift.css";
import { fetchGet, fetchPost } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
import { formatDate } from "../../../../utils/FormatDate/formatDate";
export default function AddShift(props) {
  const { listShift, setListShift } = props;
  const [specialization, setSpecialization] = useState([]);
  const [dataForm, setDataForm] = useState({});
  //gọi api lấy chuyên môn (nhóm bệnh)
  useEffect(() => {
    const uri = "/api/admin/quan-li-nhom-benh";
    fetchGet(
      uri,
      (sus) => {
        setSpecialization(sus);
      },
      (fail) => {
        // alert(fail.message);
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
  // hàm lấy lại listStaff

  // Hàm lấy danh ca khám
  const fetchShiftList = () => {
    const uri = "/api/admin/quan-li-ca-kham";
    fetchGet(
      uri,
      (data) => {
        setListShift(data); // Cập nhật danh sách ca khám
      },
      (fail) => {
        showErrorMessageBox(fail.message);
      },
      () => {
        alert("An error occurred while fetching staff list.");
      }
    );
  };
  const handleSubmit = (event) => {
    event.preventDefault();
    // loại bỏ khoảng trắng ở đầu và cuối của tên ca khám
    dataForm.tenCaKham = dataForm.tenCaKham.trim();
    const { tenCaKham, khungGio, ngayKham, soLuongBenhNhanToiDa, nhomBenhId } =
      dataForm;

    // Validate dữ liệu trước khi gửi
    if (
      !tenCaKham ||
      !khungGio ||
      !ngayKham ||
      !soLuongBenhNhanToiDa ||
      !nhomBenhId
    ) {
      showErrorMessageBox("Please fill in all the required fields!");
      return;
    }
    // tên ca khám phải là buổi sáng hoặc buổi chiều
    const validShifts = ["buổi sáng", "buổi chiều"];
    if (!validShifts.includes(tenCaKham.trim().toLowerCase())) {
      showErrorMessageBox("The shift name must be 'Buổi sáng' or 'Buổi chiều'");
      return;
    }

    // Kiểm tra ngày khám
    const ngayKhamDate = new Date(ngayKham);
    const currentDate = new Date();
    // Xét giờ của currentDate về 0h và chuyển currentDate thành ngày mai
    currentDate.setHours(0, 0, 0, 0);
    currentDate.setDate(currentDate.getDate() + 1); // Ngày mai
    // Tính ngày tối đa (30 ngày kể từ ngày mai)
    const maxDate = new Date(currentDate);
    maxDate.setDate(currentDate.getDate() + 30);

    // Kiểm tra ngày khám có hợp lệ hay không
    if (ngayKhamDate < currentDate) {
      showErrorMessageBox(
        "The exploration date must start from tomorrow onwards"
      );
      return;
    }

    if (ngayKhamDate > maxDate) {
      showErrorMessageBox(
        "The exploration date must be within 30 days from tomorrow"
      );
      return;
    }

    const newDataForm = { ...dataForm };
    //lấy khung giờ
    const timeSlot = newDataForm.khungGio;
    //lấy giờ bắt đầu, giờ kết thúc
    let startTime = "";
    let endTime = "";
    if (timeSlot === "morning") {
      startTime = "07:00:00";
      endTime = "11:00:00";
    } else {
      startTime = "13:00:00";
      endTime = "17:00:00";
    }
    //đẩy giờ bắt đầu, giờ kết thúc vào newDataForm
    newDataForm.thoiGianBatDau = startTime;
    newDataForm.thoiGianKetThuc = endTime;
    //xóa khung giờ
    delete newDataForm.khungGio;
    // kiểm tra xem ca khám đã tồn tại chưa
    for (const element of listShift) {
      const elementDate = formatDate(element.ngayKham);
      const ngayKhamDate = formatDate(newDataForm.ngayKham);
      // Nếu muốn so cả thời gian bắt đầu, thời gian kết thúc
      // if (elementDate === ngayKhamDate && element.tenCaKham.trim().toLowerCase() === newDataForm.tenCaKham.trim().toLowerCase()
      //     && element.thoiGianBatDau === newDataForm.thoiGianBatDau && element.thoiGianKetThuc === newDataForm.thoiGianKetThuc) {
      //     showErrorMessageBox("The shift already exists on this day");
      //     return;
      // }
      // Nếu chỉ so sánh tên ca khám
      if (
        elementDate === ngayKhamDate &&
        element.tenCaKham.trim().toLowerCase() ===
        newDataForm.tenCaKham.trim().toLowerCase()
      ) {
        showErrorMessageBox("The shift already exists on this day");
        return;
      }
    }

    addShift(newDataForm);
  };

  const addShift = async (newDataForm) => {
    const uri = "/api/admin/quan-li-ca-kham/add";

    // console.log(">>>>>check addd dataForm", newDataForm)
    await fetchPost(
      uri,
      newDataForm,
      (sus) => {
        // // Đóng modal
        // alert(sus.message);
        showSuccessMessageBox(sus.message);
        // Lấy phần tử button cancel
        const btnCancel = document.querySelector(".btn_Cancel");
        btnCancel.click();

        // Clear data
        handleClearData();
        // lấy lại data
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
  const handleClose = () => {
    handleClearData();
  };
  // console.log(">>>>>check addd dataForm", dataForm)
  // console.log(">>>>>check dataForm", dataForm)
  // console.log(">>>>>check listShift", listShift)
  return (
    <>
      {/* <!-- Button trigger modal --> */}
      <button
        type="button"
        className="Add_Shift col-2 rounded-2 d-flex align-items-center justify-content-center"
        data-bs-toggle="modal"
        data-bs-target="#staticBackdrop"
      >
        <span>
          <IoMdAddCircleOutline className="fs-4 me-2" />
        </span>
        Add Shift
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
                Add shift
              </h5>
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
                    placeholder="Enter Medical Shift"
                    onChange={handleChange}
                    required
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center position-relative">
                  <label
                    htmlFor="khungGio"
                    className="form-label col-4 custom-bold"
                  >
                    Time Slot:
                  </label>
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
                  <IoIosArrowDown className="position-absolute end-0 me-3" />
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
                    onChange={handleChange}
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center position-relative">
                  <label
                    htmlFor="nhomBenhId"
                    className="form-label col-4 custom-bold"
                  >
                    Disease Group:
                  </label>
                  <select
                    id="nhomBenhId"
                    name="nhomBenhId"
                    className="form-control rounded-3"
                    onChange={handleChange}
                    defaultValue="DEFAULT"
                  >
                    <option value="DEFAULT" hidden disabled>
                      Enter your specialization
                    </option>
                    {specialization &&
                      specialization.length > 0 &&
                      specialization.map((item) => (
                        <option key={item.id} value={item.id}>
                          {item.tenNhomBenh}
                        </option>
                      ))}
                  </select>
                  <IoIosArrowDown className="position-absolute end-0 me-3" />
                </div>
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="soLuongBenhNhanToiDa"
                    className="form-label col-4 custom-bold"
                  >
                    Number of patients:
                  </label>
                  <input
                    name="soLuongBenhNhanToiDa"
                    id="soLuongBenhNhanToiDa"
                    type="text"
                    className="form-control rounded-3"
                    placeholder="Enter number of patients"
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
                Cancel
              </button>
              <button
                type="button"
                onClick={handleSubmit}
                className="btn btn-primary btn_Accept"
              >
                Accept
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
