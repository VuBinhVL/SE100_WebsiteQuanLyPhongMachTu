import React, { useState } from "react";
import PropTypes from "prop-types";
import Icon from "../../../assets/icons/SuccessEmoji.png";
import "./YesNoMessageBox.css";
import "../../../styles/index.css";

export default function YesNoMessageBox({ title, onYes = null, onNo = null }) {
  const [isVisible, setIsVisible] = useState(true); // Quản lý trạng thái hiển thị

  if (!isVisible) return null;

  return (
    <div className="yes-no-message-box">
      <div className="message-content">
        <div className="message-icon">
          <img src={Icon} alt="Question" />
        </div>
        <h4 className="message-title">{title}</h4>
        <div className="button-group">
          <button
            className="yes-button"
            onClick={() => {
              setIsVisible(false); // Đóng MessageBox
              if (onYes) onYes(); // Gọi hàm YES nếu được truyền
            }}
          >
            YES
          </button>
          <button
            className="no-button"
            onClick={() => {
              setIsVisible(false); // Đóng MessageBox
              if (onNo) onNo(); // Gọi hàm NO nếu được truyền
            }}
          >
            NO
          </button>
        </div>
      </div>
    </div>
  );
}

YesNoMessageBox.propTypes = {
  title: PropTypes.string.isRequired, // Nội dung thông báo
  onYes: PropTypes.func, // Hàm xử lý khi nhấn YES
  onNo: PropTypes.func, // Hàm xử lý khi nhấn NO
};
