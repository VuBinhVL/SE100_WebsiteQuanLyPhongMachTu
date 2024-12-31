import React, { useState } from "react";
import PropTypes from "prop-types";
import Icon from "../../../assets/icons/FailEmoji.png";
import "./ErrorMessageBox.css";
import "../../../styles/index.css";

export default function ErrorMessageBox({ title }) {
  const [isVisible, setIsVisible] = useState(true); // Quản lý trạng thái hiển thị

  // Nếu component không hiển thị, return null
  if (!isVisible) return null;

  return (
    <div className="error-message-box">
      <div className="message-content">
        <div className="message-icon">
          <img src={Icon} alt="Error" className="inner-icon" />
        </div>
        <h4 className="message-title">{title}</h4>
        <button
          className="error-button"
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

ErrorMessageBox.propTypes = {
  title: PropTypes.string.isRequired, // Nội dung thông báo
};
