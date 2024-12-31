import React from "react";
import PropTypes from "prop-types";
import "./ExamCard.css";

export default function ExamCard({
  group,
  doctorName,
  time,
  date,
  image,
  onRegister,
}) {
  return (
    <div className="exam-card">
      <div className="exam-card-header">
        <p className="exam-group">Nhóm bệnh: {group}</p>
      </div>
      <div className="exam-card-body">
        <img src={image} alt="Bác sĩ" className="exam-image" />
        <p className="exam-doctor">{doctorName}</p>
        <p className="exam-time">Thời gian khám: {time}</p>
        <p className="exam-date">{date}</p>
      </div>
      <div className="exam-card-footer">
        <button className="exam-register-button" onClick={onRegister}>
          Đăng ký ngay
        </button>
      </div>
    </div>
  );
}

ExamCard.propTypes = {
  group: PropTypes.string.isRequired,
  doctorName: PropTypes.string.isRequired,
  time: PropTypes.string.isRequired,
  date: PropTypes.string.isRequired,
  image: PropTypes.string.isRequired,
  onRegister: PropTypes.func.isRequired,
};
