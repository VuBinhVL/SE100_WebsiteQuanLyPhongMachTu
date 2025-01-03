import React from "react";
import "./DiseaseInfoPopup.css";

export default function DiseaseInfoPopup({ disease, onClose }) {
  if (!disease) return null; // Không hiển thị nếu không có thông tin bệnh lý

  return (
    <div className="disease-info-popup">
      <div className="popup-header">
        <h3 className="popup-title">{disease.tenBenhLy}</h3>
        <button className="popup-close" onClick={onClose}>
          X
        </button>
      </div>
      <div className="popup-body">
        <img
          src="https://via.placeholder.com/500x300"
          alt={disease.tenBenhLy}
          className="disease-image"
        />
        <h4 className="section-title">Triệu chứng:</h4>
        <p>{disease.trieuTrung}</p>
      </div>
      <div className="popup-footer">
        {disease.isHaveAppointment ? (
          <button className="schedule-button">
            Đang có lịch, đăng ký ngay
          </button>
        ) : (
          <p className="no-appointment-text">Hiện không có lịch hẹn</p>
        )}
      </div>
    </div>
  );
}
