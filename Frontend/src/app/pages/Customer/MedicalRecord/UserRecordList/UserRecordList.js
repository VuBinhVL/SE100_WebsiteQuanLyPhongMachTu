import React, { useState } from "react";
import MedicalRecordCard from "../../../../components/Customer/MedicalRecord/MedicalRecordCard"; // Import component bạn đã tạo
import "./UserRecordList.css";
import { useNavigate } from "react-router-dom";
import DiseaseDetail from "../DiseaseDetail/DiseaseDetail"; // Import Component DiseaseDetail

export default function UserRecordList() {
  const navigate = useNavigate(); // Hook để điều hướng
  // Dữ liệu giả lập danh sách hồ sơ bệnh án
  const records = Array.from({ length: 1 }, (_, index) => ({
    id: `HS00${index + 1}`,
    date: "15/11/2004",
    diseaseType: "Tim mạch",
  }));
  // Gọi API lấy dữ liệu hồ sơ bệnh án của người dùng
  // useEffect(() => {
  //   const uri = "/api/quan-li-ho-so-benh-an/hien-thi-ho-so-benh-an";
  //   fetchGet(
  //     uri,
  //     (data) => {
  //       console.log(data);
  //       setRecords(data);
  //     },
  //     (error) => {
  //       showErrorMessageBox(error);
  //     },
  //     () => {
  //       showErrorMessageBox("Lỗi kết nối đến máy chủ");
  //     }
  //   );
  // }, []);

  //Định dạng ngày tạo hồ sơ
  // const formatDate = (dateString) => {
  //   const date = new Date(dateString);
  //   const day = date.getDate().toString().padStart(2, "0");
  //   const month = (date.getMonth() + 1).toString().padStart(2, "0");
  //   const year = date.getFullYear();
  //   return `${day}/${month}/${year}`;
  // };

  // Tính toán các chỉ số phân trang
  const [currentPage, setCurrentPage] = useState(1); // Trang hiện tại
  const recordsPerPage = 10; // Số hồ sơ trên mỗi trang
  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = records.slice(indexOfFirstRecord, indexOfLastRecord);

  const totalPages = Math.ceil(records.length / recordsPerPage);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  // const handleViewDetails = (recordId) => {
  //   navigate(`/detail-record/${recordId}`); // Chuyển hướng đến DetailRecord và truyền mã hồ sơ qua URL
  // };

  //Test view
  const [isPopupOpen, setIsPopupOpen] = useState(false); // Trạng thái popup
  const handleViewDetails = () => {
    setIsPopupOpen(true); // Mở popup
  };

  const handleClosePopup = () => {
    setIsPopupOpen(false); // Đóng popup
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
              diseaseType={record.diseaseType}
              onViewDetails={handleViewDetails} // Gọi popup khi nhấn nút
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
      {/* Popup hiển thị chi tiết */}
      {isPopupOpen && (
        <div className="popup-overlay">
          <DiseaseDetail onClose={handleClosePopup} />{" "}
          {/* Hiển thị giao diện mẫu */}
        </div>
      )}
    </div>
  );
}
