import React from "react";
import "./MedicalRecordCard.css";
import image from "../../../assets/icons/medicalrecordicon.png";

export default function MedicalRecordCard({
  recordId,
  creationDate,
  onViewDetails,
  diseaseType,
}) {
  return (
    <div className="medical-record-card">
      {/* Hình ảnh đại diện */}
      <img src={image} alt="Medical Record" className="record-image" />

      {/* Thông tin hồ sơ */}
      <div className="record-info">
        <p className="record-id"> {recordId}</p>
        <p className="record-date"> {creationDate}</p>
        <p className="record-disease-type">
          <b>Loại bệnh: </b>
          {diseaseType}
        </p>
      </div>

      {/* Nút xem chi tiết */}
      <button className="view-details-button" onClick={onViewDetails}>
        Xem chi tiết
      </button>
    </div>
  );
}
