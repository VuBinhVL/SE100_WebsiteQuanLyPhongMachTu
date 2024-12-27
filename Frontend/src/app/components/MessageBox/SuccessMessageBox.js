import React from "react";
import PropTypes from "prop-types";
import Icon from "../../assets/icons/SuccessEmoji.png";
import "./SuccessMessageBox.css";
import "../../styles/index.css";

export default function SuccessMessageBox({ title, description, onClose }) {
  return (
    <div className="success-message-box">
      <div className="message-content">
        <div className="message-icon">
          <img src={Icon} alt="Success" />
        </div>
        <h2 className="message-title">{title}</h2>
        <p className="message-description">{description}</p>
        <button className="message-button" onClick={onClose}>
          OK
        </button>
      </div>
    </div>
  );
}

SuccessMessageBox.propTypes = {
  title: PropTypes.string.isRequired,
  description: PropTypes.string.isRequired,
  onClose: PropTypes.func.isRequired,
};
