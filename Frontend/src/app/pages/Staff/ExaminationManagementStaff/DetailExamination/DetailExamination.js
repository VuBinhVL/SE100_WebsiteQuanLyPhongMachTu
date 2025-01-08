import React, { useEffect } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { useState } from "react";
import "./DetailExamination.css";
import { fetchGet } from "../../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { formatDate } from "../../../../utils/FormatDate/formatDate";

export default function DetailExamination(props) {
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
    fetchGet(
      uri,
      (sus) => {
        setInformationShift(sus);
        setDataForm(sus);
      },
      (fail) => {
        alert(fail.message);
      },
      () => {
        alert("Có lỗi xảy ra");
      }
    );
  }, [listShift]);

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
        className="detail-examination modal fade"
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
                Thông tin ca khám
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
                    Tên ca khám:
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
                    Khung giờ làm việc:
                  </label>
                  <input
                    type="text"
                    id="khungGio"
                    name="khungGio"
                    className="form-control rounded-3"
                    value={convertTimeToSlot(
                      dataForm.thoiGianBatDau,
                      dataForm.thoiGianKetThuc
                    )}
                    readOnly
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="ngayKham"
                    className="form-label col-4 custom-bold"
                  >
                    Ngày làm việc:
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
                    Chuyên môn:
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
                    Số lượng bệnh nhân:
                  </label>
                  <input
                    name="soLuongBenhNhanToiDa"
                    id="soLuongBenhNhanToiDa"
                    type="text"
                    className="form-control rounded-3"
                    placeholder="Enter your Email"
                    value={dataForm.soLuongBenhNhanToiDa}
                    readOnly={!editStatus}
                  />
                </div>
                <div className="form-group mb-3 d-flex align-items-center">
                  <label
                    htmlFor="bacSiId"
                    className="form-label col-4 custom-bold"
                  >
                    Bác sĩ:
                  </label>
                  <input
                    name="bacSiId"
                    id="bacSiId"
                    type="text"
                    className="form-control rounded-3"
                    value={item.bacSiKham || "Chưa có"}
                    readOnly
                  />
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
