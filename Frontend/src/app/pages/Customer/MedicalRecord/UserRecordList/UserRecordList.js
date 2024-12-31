import React, { useState } from "react";
import MedicalRecordCard from "../../../../components/Customer/MedicalRecord/MedicalRecordCard"; // Import component bạn đã tạo
import "./UserRecordList.css";

export default function UserRecordList() {
  // Dữ liệu giả lập danh sách hồ sơ bệnh án
  const records = Array.from({ length: 20 }, (_, index) => ({
    id: `HS00${index + 1}`,
    date: "15/11/2004",
  }));

  const [currentPage, setCurrentPage] = useState(1); // Trang hiện tại
  const recordsPerPage = 10; // Số hồ sơ trên mỗi trang

  // Tính toán các chỉ số phân trang
  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = records.slice(indexOfFirstRecord, indexOfLastRecord);

  const totalPages = Math.ceil(records.length / recordsPerPage);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  return (
    <div className="medical-record-page">
      <div className="medical-record-header">
        <h2 className="medical-record-title">HỒ SƠ BỆNH ÁN</h2>
      </div>

      {/* Danh sách hồ sơ */}
      <div className="record-container">
        {/* Danh sách hồ sơ */}
        <div className="medical-section">
          {currentRecords.map((record) => (
            <MedicalRecordCard
              key={record.id}
              image={record.image}
              recordId={record.id}
              creationDate={record.date}
              onViewDetails={() => alert(`Xem chi tiết hồ sơ: ${record.id}`)}
            />
          ))}
        </div>
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
    </div>
  );
}
