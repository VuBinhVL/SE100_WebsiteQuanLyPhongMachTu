import React, { useEffect, useState } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { IoMdAddCircle } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
import "./DetailExaminationForm.css";
import { fetchGet } from "../../../../lib/httpHandler";
import { formatDate } from "../../../../utils/FormatDate/formatDate";
import { formatDateTime } from "../../../../utils/FormatDate/formatDateTime";
import { MdDelete } from "react-icons/md";
import { FaCheck } from "react-icons/fa";
import { MdEdit } from "react-icons/md";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showYesNoMessageBox } from "../../../MessageBox/YesNoMessageBox/showYesNoMessgeBox";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";

export default function DetailExaminationForm(props) {
  const [inforDetailExamination, setInforDetailExamination] = useState({});
  const [details, setDetails] = useState([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [dataForm, setDataForm] = useState({ tenBenhLy: "", giaKham: "" });
  const { item } = props;
  const [benhLyList, setbenhLyList] = useState([]); // Đảm bảo là mảng
  const [nhomBenhId, setNhomBenhId] = useState(-1);
  const idModal = `idModalDetailExaminationForm${item.id}`;
  const idspecificModal = `#idModalDetailExaminationForm${item.id}`;

  // Lấy thông tin chi tiết phiếu khám bệnh
  useEffect(() => {
    const uri = `/api/admin/quan-li-phieu-kham-benh/detail?id=${item.id}`;
    fetchGet(
      uri,
      (sus) => {
        console.log("Thông tin phiếu khám bệnh:", sus);
        setNhomBenhId(sus.nhomBenhId); // Cập nhật nhomBenhId
        setInforDetailExamination(sus); // Lưu thông tin chi tiết phiếu khám
        setDetails(sus.chiTietKhamBenhs || []); // Lưu chi tiết khám bệnh
      },
      (fail) => {
        console.error(
          "Lỗi khi gọi API chi tiết phiếu khám bệnh:",
          fail.message
        );
      },
      () => {
        console.error(
          "Máy chủ mất kết nối khi gọi API chi tiết phiếu khám bệnh"
        );
      }
    );
  }, [item.id]); // Gọi API khi item.id thay đổi

  //Gọi API lấy bệnh lý
  useEffect(() => {
    if (nhomBenhId !== -1) {
      // Chỉ gọi API nếu nhomBenhId đã được cập nhật
      const uri = `/api/admin/quan-li-benh-ly/benh-ly-theo-nhom-benh?id=${nhomBenhId}`;
      fetchGet(
        uri,
        (res) => {
          console.log("Danh sách bệnh lý:", res);
          setbenhLyList(Array.isArray(res) ? res : []); // Đảm bảo luôn là mảng
        },
        (fail) => {
          console.error("Lỗi khi gọi API danh sách bệnh lý:", fail.message);
        },
        () => {
          console.error("Máy chủ mất kết nối khi gọi API danh sách bệnh lý");
        }
      );
    }
  }, [nhomBenhId]); // Gọi API khi nhomBenhId thay đổi

  //Thêm chi tiết khám bệnh vào danh sách
  const handleThemChiTietKhamBenh = (e) => {
    e.preventDefault(); // Ngăn chặn reload trang

    // Lấy giá trị từ input và select
    const selectedBenhLyId = document.getElementById("benhly").value;
    const giaKhamInput = document.getElementById("hoTen").value;

    // Kiểm tra dữ liệu nhập vào
    if (!selectedBenhLyId || selectedBenhLyId === "DEFAULT") {
      showErrorMessageBox("Vui lòng chọn bệnh lý.");
      return;
    }
    if (!giaKhamInput || isNaN(giaKhamInput) || giaKhamInput <= 0) {
      showErrorMessageBox("Vui lòng nhập giá khám hợp lệ.");
      return;
    }

    // Tìm tên bệnh lý dựa trên ID đã chọn
    const tenBenhLy = benhLyList.find(
      (item) => item.id === parseInt(selectedBenhLyId)
    )?.tenBenhLy;

    if (!tenBenhLy) {
      showErrorMessageBox("Bệnh lý không tồn tại.");
      return;
    }

    // Tạo một đối tượng chi tiết khám mới
    const newDetail = {
      id: Date.now(), // Giả lập ID duy nhất
      tenBenhLy,
      giaKham: parseFloat(giaKhamInput), // Chuyển giá trị thành số
    };

    // Cập nhật danh sách chi tiết khám
    setDetails((prevDetails) => [...prevDetails, newDetail]);

    // Reset form sau khi thêm thành công
    document.getElementById("benhly").value = "DEFAULT";
    document.getElementById("hoTen").value = "";
  };

  // Gọi API Thanh toán
  const handlePaymentConfirmation = async () => {
    const uri =
      "/api/admin/quan-li-phieu-kham-benh/xac-nhan-thanh-toan?id=" + item.id;
    const result = await showYesNoMessageBox("Bạn có chắc muốn thanh toán?");
    if (result) {
      fetchGet(
        uri,
        (res) => showSuccessMessageBox(res.message),
        (fail) => showErrorMessageBox(fail.message),
        () => showErrorMessageBox("Mất kết nối đến máy chủ")
      );
    } else {
    }
  };

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
                Detail Examination Form
              </h5>
              <button
                type="button"
                className="btn-close"
                data-bs-dismiss="modal"
                aria-label="Close"
              ></button>
            </div>
            <div className="modal-body d-flex justify-content-center flex-column">
              {/* Thông tin phiếu khám */}
              <div className="personal_information mb-4 p-3 border rounded bg-light">
                <h4 className="mb-3">1. Thông tin Bệnh nhân</h4>
                <div className="record-info d-flex">
                  <div className="patient-photo text-center me-4">
                    <img
                      src={inforDetailExamination.hinhAnhBenhNhan}
                      alt="Ảnh bệnh nhân"
                      className="rounded-circle mb-2"
                    />
                    <p>Ảnh bệnh nhân</p>
                  </div>
                  <div className="patient-details">
                    <p>
                      <b>Họ tên: </b> {inforDetailExamination.hoTenBenhNhan}
                    </p>
                    <p>
                      <b>Giới tính: </b>
                      {inforDetailExamination.gioiTinh}
                    </p>
                    <p>
                      <b>Ngày sinh: </b>{" "}
                      {formatDate(inforDetailExamination.ngaySinh)}
                    </p>
                    <p>
                      <b>Địa chỉ: </b> {inforDetailExamination.diaChi}
                    </p>
                    <p>
                      <b>Thời gian khám: </b>{" "}
                      {formatDateTime(inforDetailExamination.thoiGianKham)}
                    </p>
                    <p>
                      <b>Bác sĩ khám: </b> {inforDetailExamination.tenBacSiKham}
                    </p>
                  </div>
                </div>
              </div>

              {/* Chi tiết khám bệnh */}
              <div className="details-section mb-4 p-3 border rounded bg-light">
                <h4 className="mb-3 d-flex align-items-center">
                  2. Chi tiết khám bệnh
                  <span className="ms-2"></span>
                </h4>

                {/*  Thêm thuốc*/}
                <form className="me-5 w-75">
                  <div className="form-group mb-3 d-flex align-items-center position-relative">
                    <label
                      htmlFor="tenBenhLy"
                      className="form-label col-4 custom-bold"
                    >
                      Tên bệnh lý:
                    </label>
                    <select
                      id="benhly"
                      name="benhly"
                      className="form-control rounded-3 "
                      defaultValue={"DEFAULT"}
                      //onChange={handleChange}
                    >
                      <option value="DEFAULT" hidden disabled>
                        Chọn bệnh lý
                      </option>
                      {benhLyList.map((loai) => (
                        <option key={loai.id} value={loai.id}>
                          {loai.tenBenhLy}
                        </option>
                      ))}
                    </select>
                    <IoIosArrowDown className="position-absolute end-0 me-3" />
                  </div>
                  <div className="form-group mb-3 d-flex align-items-center">
                    <label
                      htmlFor="hoTen"
                      className="form-label col-4 custom-bold"
                    >
                      Giá khám:
                    </label>
                    <input
                      className="form-control rounded-3"
                      name="hoTen"
                      id="hoTen"
                      type="number"
                      placeholder="Nhập giá khám"
                      //onChange={handleChange}
                      required
                    />
                  </div>
                  <div className="d-flex justify-content-end me-3 mb-3">
                    <button
                      onClick={handleThemChiTietKhamBenh}
                      className="btn btn-primary"
                    >
                      Thêm
                    </button>
                  </div>
                </form>
                <table className="table table-hover">
                  <thead className="table-light">
                    <tr>
                      <th>STT</th>
                      <th>Tên bệnh lý</th>
                      <th>Giá khám</th>
                      <th>Thao tác</th>
                    </tr>
                  </thead>
                  <tbody>
                    {details.map((item, index) => (
                      <tr key={item.id}>
                        <td>{index + 1}</td>
                        <td>{item.tenBenhLy}</td>
                        <td>{item.giaKham}</td>
                        <td>
                          <a href="#">
                            <MdEdit className="icon_edit icon_action" />
                          </a>
                          <a href="#">
                            <GrCircleInformation className="icon_information icon_action" />
                          </a>
                          <a href="#">
                            <MdDelete className="icon_delete icon_action" />
                          </a>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>

              {/* Tổng tiền */}
              <div className="total-info mb-4 p-3 border rounded bg-light">
                <h4 className="mb-3">3. Tổng chi phí</h4>
                <p>
                  <b>Tiền xét nghiệm:</b> {inforDetailExamination.tienXetNghiem}
                </p>
                <p>
                  <b>Tiền chụp chiếu:</b> {inforDetailExamination.tienChupChieu}
                </p>
                <p>
                  <b>Tiền khám:</b> {inforDetailExamination.tienKham}
                </p>
                <p>
                  <b>Tổng tiền thuốc:</b> {inforDetailExamination.tienThuoc}
                </p>
                <p>
                  <b>Tổng:</b>{" "}
                  {inforDetailExamination.tienXetNghiem +
                    inforDetailExamination.tienChupChieu +
                    inforDetailExamination.tienKham +
                    inforDetailExamination.tienThuoc}
                </p>
              </div>

              {/* Footer */}
              <div className="popup-footer text-end">
                <button
                  className="btn btn-primary"
                  onClick={() => handlePaymentConfirmation()}
                >
                  Xác nhận thanh toán
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
