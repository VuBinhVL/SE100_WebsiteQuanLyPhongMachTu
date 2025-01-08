import React, { useEffect } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { useState } from "react";
import "./DetailAppointment.css";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler.js";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox.js";
import { formatDate } from "../../../../utils/FormatDate/formatDate.js";
import * as bootstrap from 'bootstrap';
export default function DetailAppointment({ appointment }) {
  const [informationAppointment, setInformationAppointment] = useState({});
  // const [dataForm, setDataForm] = useState({});
  console.log(">>>>>>>.check appointment", appointment);
  useEffect(() => {
    const uri = `/api/admin/quan-li-lich-kham/chi-tiet-lich-kham?lichKhamId=${appointment.id}`;
    fetchGet(
      uri,
      (sus) => {
        setInformationAppointment(sus);

      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, [appointment]);

  const convertTimeToSlot = (timeStart, timeEnd) => {
    if (!timeStart || !timeEnd) {
      return "";
    }
    return `${timeStart.slice(0, 5)} - ${timeEnd.slice(0, 5)}`;
  };

  return (
    <>
      {/* <a href="#" data-bs-toggle="modal" data-bs-target="#listappointment">
        <GrCircleInformation className="icon_information icon_action" />
      </a> */}
      <div
        className="detailAppoinment modal fade"
        id={`detailAppointmentModal_${appointment.id}`}
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
              <div className="formOne">
                <h3>1.Thông tin bệnh nhân</h3>
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
                      value={informationAppointment.tenBenhNhan}
                      readOnly
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
                      value={informationAppointment.gioiTinh}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="soDienThoai"
                      className="form-label col-4 custom-bold"
                    >
                      Số điện thoại:
                    </label>

                    <input
                      type="text"
                      id="soDienThoai"
                      name="soDienThoai"
                      className="form-control rounded-3"
                      value={informationAppointment.soDienThoai}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center position-relative">
                    <label
                      htmlFor="diaChi"
                      className="form-label col-4 custom-bold"
                    >
                      Địa chỉ:
                    </label>
                    <input
                      type="text"
                      id="diaChi"
                      name="diaChi"
                      className="form-control rounded-3"
                      value={informationAppointment.diaChi}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="ngaySinhBN"
                      className="form-label col-4 custom-bold"
                    >
                      Ngày sinh:
                    </label>
                    <input
                      name="ngaySinhBN"
                      id="ngaySinhBN"
                      type="date"
                      className="form-control rounded-3"
                      placeholder="Enter your Email"
                      value={formatDate(informationAppointment.ngaySinhBN)}
                      readOnly
                    />
                  </div>
                </form>
              </div>
              <div className="formTwo">
                <h3>2.Thông tin lịch khám</h3>
                <form className="w-100">
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="tenCaKham"
                      className="form-label col-4 custom-bold"
                    >
                      Tên ca khám:
                    </label>
                    <input
                      className="form-control rounded-3"
                      name="tenCaKham"
                      id="tenCaKham"
                      type="text"
                      placeholder="Enter full name"
                      value={informationAppointment.tenCaKham}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center position-relative">
                    <label
                      htmlFor="stt"
                      className="form-label col-4 custom-bold"
                    >
                      Số thứ tự khám:
                    </label>
                    <input
                      type="text"
                      id="stt"
                      name="stt"
                      className="form-control rounded-3"
                      value={informationAppointment.stt}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="khungGio"
                      className="form-label col-4 custom-bold"
                    >
                      Khung giờ:
                    </label>

                    <input
                      type="text"
                      id="khungGio"
                      name="khungGio"
                      className="form-control rounded-3"
                      value={convertTimeToSlot(
                        informationAppointment.thoiGianBatDau,
                        informationAppointment.thoiGianKetThuc
                      )}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center position-relative">
                    <label
                      htmlFor="ngayKham"
                      className="form-label col-4 custom-bold"
                    >
                      Ngày khám:
                    </label>
                    <input
                      type="date"
                      id="ngayKham"
                      name="ngayKham"
                      className="form-control rounded-3"
                      value={formatDate(informationAppointment.ngayKham)}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="tenNhomBenh"
                      className="form-label col-4 custom-bold"
                    >
                      Nhóm bệnh:
                    </label>
                    <input
                      name="tenNhomBenh"
                      id="tenNhomBenh"
                      type="text"
                      className="form-control rounded-3"
                      value={informationAppointment.tenNhomBenh}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="trangThai"
                      className="form-label col-4 custom-bold"
                    >
                      Trạng thái:
                    </label>
                    <input
                      name="trangThai"
                      id="trangThai"
                      type="text"
                      className="form-control rounded-3"
                      placeholder="Enter your address"
                      value={informationAppointment.trangThai}
                      readOnly
                    />
                  </div>
                </form>
              </div>
              <div className="formThree">
                <h3>3.Thông tin bác sĩ khám</h3>
                <form className="w-100">
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="tenBacSi"
                      className="form-label col-4 custom-bold"
                    >
                      Bác sĩ:
                    </label>
                    <input
                      className="form-control rounded-3"
                      name="tenBacSi"
                      id="tenBacSi"
                      type="text"
                      placeholder="Enter full name"
                      value={informationAppointment.tenBacSi}
                      readOnly
                    />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center position-relative">
                    <label
                      htmlFor="tenChuyenMon"
                      className="form-label col-4 custom-bold"
                    >
                      Chuyên khoa:
                    </label>
                    <input
                      type="text"
                      id="tenChuyenMon"
                      name="tenChuyenMon"
                      className="form-control rounded-3"
                      value={informationAppointment.tenChuyenMon}
                      readOnly
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