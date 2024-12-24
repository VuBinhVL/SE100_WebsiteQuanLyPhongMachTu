import React from "react";
import PropTypes from "prop-types";
import "./StaffCard.css";

export default function StaffCard({ avatar, name, role, specialty }) {
  return (
    <div className="staff-card">
      <div className="staff-card-avatar">
        <img src={avatar} alt={name} />
      </div>
      <h3 className="staff-card-name">{name}</h3>
      <p className="staff-card-role">
        <strong>Vai trò:</strong> {role}
      </p>
      <p className="staff-card-specialty">
        <strong>Chuyên môn:</strong> {specialty}
      </p>
    </div>
  );
}

StaffCard.propTypes = {
  avatar: PropTypes.string.isRequired, // URL của ảnh đại diện
  name: PropTypes.string.isRequired, // Tên của nhân viên
  role: PropTypes.string.isRequired, // Vai trò của nhân viên
  specialty: PropTypes.string.isRequired, // Chuyên môn của nhân viên
};
