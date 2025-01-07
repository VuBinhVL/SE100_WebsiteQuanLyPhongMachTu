import React, { useState } from "react";
import PropTypes from "prop-types";
import Icon from "../../../assets/icons/SuccessEmoji.png";
import "./SuccessMessageBox.css";
import "../../../styles/index.css";

export default function SuccessMessageBox({ title }) {
  const [isVisible, setIsVisible] = useState(true); // Quản lý trạng thái hiển thị

  // Nếu component không hiển thị, return null
  if (!isVisible) return null;

  return (
    <div className="success-message-box">
      <div className="message-content">
        <div className="message-icon">
          <img src={Icon} alt="Success" />
        </div>
        <h4 className="message-title">{title}</h4>
        <button
          className="message-button"
          onClick={() => {
            setIsVisible(false); // Đóng MessageBox
          }}
        >
          OK
        </button>
      </div>
    </div>
  );
}

SuccessMessageBox.propTypes = {
  title: PropTypes.string.isRequired, // Nội dung thông báo
};
