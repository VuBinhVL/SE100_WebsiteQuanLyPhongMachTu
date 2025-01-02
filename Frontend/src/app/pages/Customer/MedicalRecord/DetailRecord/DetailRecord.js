import { React, useState } from "react";
import MedicalImaging from "../MedicalImaging/MedicalImaging"; // Component Popup ảnh chụp chiếu
import "./DetailRecord.css";

export default function DetailRecord() {
  const [isPopupVisible, setIsPopupVisible] = useState(false); // Quản lý trạng thái popup
  const handleOpenPopup = () => {
    setIsPopupVisible(true); // Hiển thị popup
  };
  // Tính toán các chỉ số phân trang
  const [currentPage, setCurrentPage] = useState(1); // Trang hiện tại
  const recordsPerPage = 2; // Số bệnh lý trên mỗi trang
  const records = [
    {
      id: 1,
      doctorName: "Trần Thanh Trúc",
      disease: "Cao huyết áp",
      date: "20/12/2024",
      total: "190.000 đ",
    },
    {
      id: 2,
      doctorName: "Trần Thanh Trúc",
      disease: "Rối loạn nhịp tim",
      date: "20/12/2024",
      total: "150.000 đ",
    },
  ];
  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = records.slice(indexOfFirstRecord, indexOfLastRecord);
  const totalPages = Math.ceil(records.length / recordsPerPage);
  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  return (
    <div className="detail-record-page">
      <div className="detail-record-header">
        <h2 className="detail-record-title">HỒ SƠ BỆNH ÁN: HS001</h2>
      </div>

      {/* Danh sách các bệnh lý gặp phải */}
      <div className="detail-record-container">
        <table className="detail-record-table">
          <thead>
            <tr>
              <th>STT</th>
              <th>Tên bác sĩ điều trị</th>
              <th>Bệnh lý mắc phải</th>
              <th>Ngày khám</th>
              <th>Tổng tiền</th>
              <th>Thao tác</th>
            </tr>
          </thead>
          <tbody>
            {currentRecords.map((record, index) => (
              <tr key={record.id}>
                <td>{index + 1}</td>
                <td>{record.doctorName}</td>
                <td>{record.disease}</td>
                <td>{record.date}</td>
                <td>{record.total}</td>
                <td className="d-flex gap-2 justify-content-center align-items-center">
                  <button
                    type="button"
                    class="btn btn-success  "
                    data-toggle="tooltip"
                    title="Xem chi tiết bệnh lý khám"
                    onClick={handleOpenPopup} // Mở popup khi nhấn
                  >
                    Chi tiết khám
                  </button>
                  <button
                    class="btn btn-danger "
                    data-toggle="tooltip"
                    title="Xem ảnh chụp chiếu"
                    type="button"
                  >
                    Ảnh chụp chiếu
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Phân trang */}
      <div className="pagination">
        {[...Array(totalPages)].map((_, index) => (
          <button
            key={index + 1}
            className={`pagination-button ${
              currentPage === index + 1 ? "active" : ""
            }`}
            onClick={() => handlePageChange(index + 1)}
          >
            {index + 1}
          </button>
        ))}
      </div>

      {/* Hiển thị popup nếu trạng thái bật */}
      {isPopupVisible && <MedicalImaging />}
    </div>
  );
}
