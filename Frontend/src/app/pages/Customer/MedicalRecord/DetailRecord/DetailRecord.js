import { React, useEffect, useState } from "react";
import { fetchGet } from "../../../../lib/httpHandler"; // Import hàm gọi API
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox"; // Import hàm hiển thị thông báo lỗi
import MedicalImaging from "../MedicalImaging/MedicalImaging"; // Component Popup ảnh chụp chiếu
import DiseaseDetail from "../DiseaseDetail/DiseaseDetail"; // Component Popup chi tiết bệnh lý
import "./DetailRecord.css";
import { useParams } from "react-router-dom";

export default function DetailRecord() {
  const { id } = useParams(); // Lấy id từ URL
  const [recordDetails, setRecordDetails] = useState([]);

  //Phục vụ cho việc xem chi tiết khám bệnh
  const [selectedDiseaseId, setSelectedDiseaseId] = useState(null);
  const [isPopupVisible, setIsPopupVisible] = useState(false);
  const handleOpenPopup = (diseaseId) => {
    setSelectedDiseaseId(diseaseId);
    setIsPopupVisible(true);
  };
  // Đóng popup
  const handleClosePopup = () => {
    setIsPopupVisible(false);
  };

  //Phục vụ cho việc xem chi tiết chụp chiếu
  const [selectedImagingId, setSelectedImagingId] = useState(null);
  const [isMedicalImagingVisible, setIsMedicalImagingVisible] = useState(false);
  const handleOpenMedicalImaging = (diseaseId) => {
    setSelectedImagingId(diseaseId); // Lưu Id vào state
    setIsMedicalImagingVisible(true); // Hiển thị popup
  };

  // Hàm để đóng popup MedicalImaging
  const handleCloseMedicalImaging = () => {
    setIsMedicalImagingVisible(false);
  };

  //Gọi API để lấy thông tin
  useEffect(() => {
    if (id) {
      const uri = `/api/quan-li-chi-tiet-ho-so-benh-an?HoSoBenhAnID=${id}`;
      fetchGet(
        uri,
        (data) => {
          console.log(data); // Dữ liệu chi tiết của hồ sơ bệnh án
          setRecordDetails(data || []);
        },
        (error) => {
          showErrorMessageBox(error.message);
        },
        () => {
          showErrorMessageBox("Lỗi kết nối đến máy chủ");
        }
      );
    }
  }, [id]);

  //Định dạng ngày khám
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, "0");
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
  };

  //Đinh dạng tiền
  const formatCurrency = (value) => {
    return new Intl.NumberFormat("vi-VN", {
      style: "decimal",
      minimumFractionDigits: 0,
    }).format(value);
  };

  // Tính toán các chỉ số phân trang
  const [currentPage, setCurrentPage] = useState(1); // Trang hiện tại
  const recordsPerPage = 6; // Số bệnh lý trên mỗi trang
  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = recordDetails.slice(
    indexOfFirstRecord,
    indexOfLastRecord
  );
  const totalPages = Math.ceil(recordDetails.length / recordsPerPage);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  return (
    <div className="detail-record-page">
      <div className="detail-record-header">
        <h2 className="detail-record-title">MÃ HỒ SƠ BỆNH ÁN: {id}</h2>
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
                <td>{index + 1 + indexOfFirstRecord}</td>{" "}
                {/* Đảm bảo số thứ tự đúng */}
                <td>{record.hoTenBacSi}</td>
                <td>{record.tenBenhLy}</td>
                <td>{formatDate(record.ngayKham)}</td>
                <td>{formatCurrency(record.tongTien)} đ</td>
                <td className="d-flex gap-2 justify-content-center align-items-center">
                  <button
                    type="button"
                    className="btn btn-success"
                    data-toggle="tooltip"
                    title="Xem chi tiết bệnh lý khám"
                    onClick={() => handleOpenPopup(record.id)}
                  >
                    Chi tiết khám
                  </button>
                  <button
                    className="btn btn-danger"
                    data-toggle="tooltip"
                    title="Xem ảnh chụp chiếu"
                    type="button"
                    onClick={() => handleOpenMedicalImaging(record.id)} // Truyền Id
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
      {isPopupVisible && (
        <DiseaseDetail
          diseaseId={selectedDiseaseId}
          onClose={handleClosePopup}
        />
      )}

      {/* Hiển thị popup của chụp chiếu */}
      {isMedicalImagingVisible && (
        <MedicalImaging
          diseaseId={selectedImagingId}
          onClose={handleCloseMedicalImaging}
        />
      )}
    </div>
  );
}
