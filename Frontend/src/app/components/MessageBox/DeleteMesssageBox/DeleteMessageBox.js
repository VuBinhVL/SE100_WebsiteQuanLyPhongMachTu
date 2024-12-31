import React, { useState } from "react";
import PropTypes from "prop-types";
import "./DeleteMessageBox.css";
import "../../../styles/index.css";

export default function DeleteMessageBox(props) {
  const { title, onClose, onConfirm } = props;
  return (
    <div className="delete-message-box">
      <div className="message-content">
        <div className="message-icon">
          <img src="https://cdn-icons-png.flaticon.com/512/6897/6897039.png" alt="Delete" className="inner_img" />
        </div>
        <h4 className="message-title">{title}</h4>
        <div className="container_action d-flex justify-content-center">
          <button
            className="cancel-button me-2"
            onClick={onClose}
          >
            Cancel
          </button>
          <button
            className="delete-button"
            onClick={() => {
              onConfirm();
            }}
          >
            OK
          </button>
        </div>
      </div>
    </div>
  );
}

DeleteMessageBox.propTypes = {
  title: PropTypes.string.isRequired, // Nội dung thông báo
};
