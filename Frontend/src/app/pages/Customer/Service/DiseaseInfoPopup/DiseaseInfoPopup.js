import React, { useEffect, useState } from "react";
import "./DiseaseInfoPopup.css";
import { fetchGet } from "../../../../lib/httpHandler";
import { useNavigate } from "react-router-dom";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";

export default function DiseaseInfoPopup({ diseaseId, onClose }) {
  const [diseaseDetails, setDiseaseDetails] = useState(null);
  const navigate = useNavigate();
  useEffect(() => {
    if (!diseaseId) return; // Không làm gì nếu không có id
    const uri = "/api/quan-li-benh-ly/chi-tiet-benh-ly?benhLyId=" + diseaseId;
    fetchGet(
      uri,
      (result) => {
        setDiseaseDetails(result); // Lưu chi tiết bệnh lý
      },
      (error) => {
        showErrorMessageBox(error.message);
      },
      () => {
        showErrorMessageBox("Không thể kết nối đến server");
      }
    );
  }, [diseaseId]);

  if (!diseaseId) return null;

  return (
    <>
      {/* Lớp overlay */}
      <div className="disease-info-overlay" onClick={onClose}></div>

      {/* Popup */}
      <div className="disease-info-popup">
        <div className="popup-header">
          <h3 className="popup-title">
            {diseaseDetails?.tenBenhLy || "Loading..."}
          </h3>
          <button className="popup-close" onClick={onClose}>
            X
          </button>
        </div>
        <div className="popup-body">
          <img
            src={
              diseaseDetails?.images || "https://via.placeholder.com/500x300"
            }
            alt={diseaseDetails?.tenBenhLy || "Image"}
            className="disease-image"
          />
          <h4 className="section-title">Triệu chứng:</h4>
          <p>
            <b>{diseaseDetails?.trieuTrung || "Đang tải thông tin..."}</b>
          </p>
        </div>
        <div className="popup-footer">
          {diseaseDetails?.isHaveAppointment ? (
            <button
              className="schedule-button"
              onClick={() => navigate("/medical-exam-list")}
            >
              Đang có lịch, đăng ký ngay
            </button>
          ) : (
            <p className="no-appointment-text">Hiện không có lịch khám</p>
          )}
        </div>
      </div>
    </>
  );
}
