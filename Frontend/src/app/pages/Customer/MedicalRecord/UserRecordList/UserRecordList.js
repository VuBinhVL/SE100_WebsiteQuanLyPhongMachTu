import React, { useEffect, useState } from "react";
import MedicalRecordCard from "../../../../components/Customer/MedicalRecord/MedicalRecordCard"; // Import component bạn đã tạo
import anhHoSo from "../../../../assets/icons/medicalrecordicon.png";
import "./UserRecordList.css";
import { useNavigate } from "react-router-dom";
import DiseaseDetail from "../DiseaseDetail/DiseaseDetail"; // Import Component DiseaseDetail
import { fetchGet } from "../../../../lib/httpHandler"; // Import hàm gọi API
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // Import hàm hiển thị thông báo lỗi

export default function UserRecordList() {
  const [recordList, setrecordList] = useState([]); // Khởi tạo là mảng rỗng
  //Gọi API lấy dữ liệu hồ sơ bệnh án của người dùng
  useEffect(() => {
    const uri = "/api/quan-li-ho-so-benh-an";
    fetchGet(
      uri,
      (data) => {
        console.log(data);
        setrecordList(data);
      },
      (error) => {
        showErrorMessageBox(error.message);
      },
      () => {
        showErrorMessageBox("Lỗi kết nối đến máy chủ");
      }
    );
  }, []);

  //Định dạng ngày tạo hồ sơ
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, "0");
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
  };

  // Tính toán các chỉ số phân trang
  const [currentPage, setCurrentPage] = useState(1); // Trang hiện tại
  const recordsPerPage = 10; // Số hồ sơ trên mỗi trang
  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = recordList.slice(
    indexOfFirstRecord,
    indexOfLastRecord
  );
  const totalPages = Math.ceil(recordList.length / recordsPerPage);

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
          {recordList.length > 0 ? (
            recordList.map((record) => (
              <MedicalRecordCard
                key={record.id}
                image={anhHoSo}
                recordId={record.id}
                creationDate={formatDate(record.ngayTao)}
                diseaseType={record.nhomBenh}
              />
            ))
          ) : (
            <p className="no-records">Không có hồ sơ bệnh án</p>
          )}
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
